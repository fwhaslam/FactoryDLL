// Copyright (c) 2026 Frederick William Haslam born 1962 in the USA.
// Licensed under "The MIT License" https://opensource.org/license/mit/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FactoryModel.Models;
using FactoryModel.Models.Constants;

using static FactoryModel.Models.Constants.RuleType;
using static FactoryModel.Models.Constants.SiteTypeInfo;
using FactoryModel.Tools;


namespace FactoryModel.Builder {

    /// <summary>
    /// Used to construct random markets.
    /// </summary>
    public class MarketBuilder {

        public RandTool Rand { get; set; } = new RandTool( Environment.TickCount );

        public int Limit {  get; set; }

        public Market Trade { get; set; }


        /// <summary>
        /// add new chains up to limit, with restrictions on lower tier count.
        /// </summary>
        public void Build() { 
            
            Trade = new Market();

            // build chains based on tier count
            var tierMap = SelectNumberOfTiers();
//Console.WriteLine("TierMap="+tierMap);

            for ( int tier=1;tier<=MAXIMUM_TIER_LIMIT;tier++) {
                if (tierMap.ContainsKey(tier)) {
//Console.WriteLine("TierMap["+tier+"] = " + tierMap[tier]);
                    var count = tierMap[tier];
                    for ( int cx = 0; cx<count; cx++ ) {
                        var chain = InventChain( tier );
                        Trade.AddChain( chain );
                    }
                }
            }
        }


        /// <summary>
        /// Decide on how many chains for each tier.
        /// </summary>
        /// <returns>Map[Tiers,Count]</returns>
        internal Dictionary<int,int> SelectNumberOfTiers() {

            var map = new Dictionary<int,int>();

            var halfLimit = (1+Limit)/2;
            if (halfLimit>MAXIMUM_TIER_LIMIT) halfLimit = MAXIMUM_TIER_LIMIT;

            // ensure we have 1 per tier up to halfLimit
            for ( int tx=1; tx<=halfLimit; tx++ ) {
                map[tx] = 1;
            }
            
            // pick some more at random, creating random mix.
            for ( int cx=halfLimit; cx<Limit; cx++) {
                var newTierSpace = FindChainSpace( map );
                var addTier = newTierSpace[ Rand.Next(newTierSpace.Count) ];
                if (!map.ContainsKey(addTier)) map[addTier] = 0;
                map[addTier]++;
            }

            return map;
        }

        /// <summary>
        /// List of Tiers that may be added to market.
        /// 
        /// No more than half(limit) for a particular tier.
        /// Must have Tier N before Tier N+1.  
        /// Assume Tier 0 exists.
        /// </summary>
        /// <returns></returns>
        internal List<int> FindChainSpace( Dictionary<int,int> tierMap ) {

            var maxTier = 0;

            var space = new List<int>();
            foreach ( var entry in tierMap ) {

                var tier = entry.Key;
                var count = entry.Value;

                if (2*count>Limit) continue;

                space.Add( tier );
                if (tier>maxTier) maxTier = tier;

            }

            // add max+1 tier
            if (maxTier<MAXIMUM_TIER_LIMIT) space.Add( 1+maxTier );
            return space;
        }

//======================================================================================================================

        internal Chain InventChain( int tier ) {

            var work = new Chain();
            var length = PickLength( tier );

            // first rule, Make
            work.Add( InventMakeRule( tier ) );
            Product lastProduct = null;

            // series of transforms
            for (int rx=0;rx<length;rx++) {
                
                lastProduct = work.LastRule.Out.Key(0);
                work.Add( InventEditRule( tier, lastProduct ) );
            }

            // chain endpoint
            lastProduct = work.LastRule.Out.Key(0);
            work.Add( InventVendRule( tier, lastProduct ) );

            return work;
        }


        internal Rule InventMakeRule( int tier ) {

            var input = NextProduct( tier );
            var rule = new Rule() {
                Tier = tier,
                Type = Make
            };
            rule.Out.Increment(  input );

            return rule;
        }

        internal Rule InventVendRule( int tier, Product sell ) {

            var rule = new Rule() {
                Tier = tier,
                Type = Vend
            };
            rule.Ins.Increment( sell );

            return rule;
        }

        internal Rule InventEditRule( int tier, Product input ) {

            var info = TierInfo.TierMap[tier];

            var index = Rand.Next( info.Editors.Count );
            var editType = info.Editors[index];
            var editInfo = EditTypeEnumInfo.EditTypeEnumMap[editType];

            // first input
            var value = input.Value;
            var rule = new Rule() {
                Tier = tier,
                Type = Edit
            };
            rule.Ins.Add(input,1);

            // other inputs
            for (int ix=1;ix<editInfo.Inputs;ix++) {
                var moreIns = PickProduct();
                rule.Ins.Include( moreIns, 1 );
                value += moreIns.Value;
            }

            // add Tier component to value
            value += tier;
            rule.Facility = editInfo.Facilities[0];

            // first Output is always a New Product
            var nextOut = NextProduct(tier);
            value += nextOut.Value;
            rule.Out.Include( nextOut, 1 );

            // other outputs :: 50% new, until old -> then all old
            for (int ix=1;ix<editInfo.Inputs;ix++) {
                var moreOut = PickProduct();
                rule.Out.Include( moreOut, 1 );
                value += moreOut.Value;
            }

            // cleanup
            rule.Out.Key(0).Value = value;
            return rule;
        }


        internal Product NextProduct( int tier ) {
            var num = 1 + Trade.Products.Count;
            var work = new Product() {
                Name = "P"+num,
                Value = tier * (1+tier) / 2     // triangle value
            };
            Trade.Products.Add( work );
            return work;
        }

        internal Product PickProduct() {
            if (Trade.Products.Count<1) return null;
            var pick = Rand.Next( Trade.Products.Count() );
            return Trade.Products[ pick ];
        }

        internal int PickLength( int tier ) {
            return PickLength( tier, (float)Rand.NextDouble() );
        }

        internal static int PickLength( int tier, float value ) {
//Console.WriteLine("PICK LENGTH["+tier+"] ="+value);
            var info = TierInfo.TierMap[tier];
            for (int ix=0; ix<info.ChainLength.Count(); ix++) { 
                if ( value<info.ChainLength[ix] ) return ix;
            }
            return -1;  // we should not reach this line, providing ChainLength[] is correct
        }


    }
}
