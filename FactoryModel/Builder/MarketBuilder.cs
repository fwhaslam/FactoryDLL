// Copyright (c) 2026 Frederick William Haslam born 1962 in the USA.
// Licensed under "The MIT License" https://opensource.org/license/mit/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FactoryModel.Models;
using FactoryModel.Models.Constants;
using static FactoryModel.Models.RuleType;

namespace FactoryModel.Builder {

    /// <summary>
    /// Used to construct random markets.
    /// </summary>
    public class MarketBuilder {

        public Random Rand { get; set; } = new Random( Environment.TickCount );

        public int Size {  get; set; }

        public Market Market { get; set; }

        public void Build() { 
            
            Market = new Market();

            Market.AddChain( InventChain( 1 ) );
            Market.AddChain( InventChain( 2 ) );
            Market.AddChain( InventChain( 3 ) );
            Market.AddChain( InventChain( 4 ) );
            Market.AddChain( InventChain( 5 ) );
            Market.AddChain( InventChain( 6 ) );

        }

        internal Product NextProduct( int tier ) {
            var num = 1 + Market.Products.Count;
            var work = new Product() {
                Name = "P"+num,
                Value = tier * (1+tier) / 2     // triangle value
            };
            Market.Products.Add( work );
            return work;
        }

        internal Product PickProduct() {
            if (Market.Products.Count<1) return null;
            var pick = Rand.Next( Market.Products.Count() );
            return Market.Products[ pick ];
        }

        internal int PickLength( int tier ) {
            return PickLength( tier, (float)Rand.NextDouble() );
        }

        internal static int PickLength( int tier, float value ) {
            var info = TierInfo.TierMap[tier];
            for (int ix=0; ix<info.ChainLength.Count(); ix++) { 
                if ( value<info.ChainLength[ix] ) return ix;
            }
            return -1;  // we should not reach this line, providing ChainLength[] is correct
        }

        internal Chain InventChain( int tier ) {

            var work = new Chain();
            var length = PickLength( tier );

            // first rule, Make
            work.Add( InventMakeRule( tier ) );
            Product lastProduct = null;

            // series of transforms
            for (int rx=0;rx<length;rx++) {
                
                lastProduct = work.Last.Out.Key(0);
                work.Add( InventEditRule( tier, lastProduct ) );
            }

            // chain endpoint
            lastProduct = work.Last.Out.Key(0);
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


    }
}
