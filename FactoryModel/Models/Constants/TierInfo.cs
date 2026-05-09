// Copyright (c) 2026 Frederick William Haslam born 1962 in the USA.
// Licensed under "The MIT License" https://opensource.org/license/mit/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FactoryModel.Tools;

using static FactoryModel.Models.Constants.MakeTypeEnum;
using static FactoryModel.Models.Constants.VendTypeEnum;
using static FactoryModel.Models.Constants.EditTypeEnum;
using static FactoryModel.Models.Constants.FacilityTypeEnum;

using static FactoryModel.Tools.SystemTools;
using FactoryModel.Models.Constants;

namespace FactoryModel.Models.Constants {

    public enum MakeTypeEnum {
        SimpleMake, // Picker
        FairMake,   // Cutter
        GoodMake,   // Grabber
        GreatMake,  // Ripper
    }

    public enum VendTypeEnum {
        SimpleVend, // Beggar
        FairVend,   // Hawker
        GoodVend,   // Pedlar
        GreatVend,  // Vendor
    }

    /// <summary>
    /// Transformer Inputs versus Outputs.
    /// </summary>
    public enum EditTypeEnum {
        OneVOne,
        TwoVOne,
        TwoVTwo,
        OneVTwo,

        ThreeVOne,
        ThreeVTwo,
        ThreeVThree,

        FiveVOne,
        FiveVThree,
        EightVOne,
        EightVThree
    }

    public class EditTypeEnumInfo : KeyedElementIf<EditTypeEnum> {

        public EditTypeEnum Type {  get; set; }

        public EditTypeEnum Key {  get {  return Type; } }

        public List<FacilityTypeEnum> Facilities { get; set; } = new List<FacilityTypeEnum>();

        public int Inputs { get; set; } = 1;

        public int Outputs { get; set; } = 1;

        // readonly constanats
        public static readonly List<EditTypeEnumInfo> EditTypeEnumList = new List<EditTypeEnumInfo> {
            new EditTypeEnumInfo() {
                Type = OneVOne,
                Facilities = new List<FacilityTypeEnum>(){ EditOneVOne }
            },
            new EditTypeEnumInfo() {
                Type = OneVTwo,
                Outputs = 2,
                Facilities = new List<FacilityTypeEnum>(){ EditOneVTwo }
            },
            new EditTypeEnumInfo() {
                Type = TwoVOne,
                Inputs = 2,
                Facilities = new List<FacilityTypeEnum>(){ EditTwoVOne }
            },
            new EditTypeEnumInfo() {
                Type = TwoVTwo,
                Inputs = 2,
                Facilities = new List<FacilityTypeEnum>(){ EditTwoVTwo }

            },
            new EditTypeEnumInfo() {
                Type = ThreeVOne,
                Inputs = 3,
                Facilities = new List<FacilityTypeEnum>(){ EditThreeVOne }
            },
            new EditTypeEnumInfo() {
                Type = ThreeVTwo,
                Inputs = 3,
                Outputs = 2,
                Facilities = new List<FacilityTypeEnum>(){ EditThreeVTwo }
            },
            new EditTypeEnumInfo() {
                Type = ThreeVThree,
                Inputs = 3,
                Outputs = 3,
                Facilities = new List<FacilityTypeEnum>(){ EditThreeVThree }
            },
            new EditTypeEnumInfo() {
                Type = FiveVOne,
                Inputs = 5,
                Facilities = new List<FacilityTypeEnum>(){ EditFiveVOne }
            },
            new EditTypeEnumInfo() {
                Type = FiveVThree,
                Inputs = 5,
                Outputs = 3,
                Facilities = new List<FacilityTypeEnum>(){ EditFiveVThree }
            },
            new EditTypeEnumInfo() {
                Type = EightVOne,
                Inputs = 8,
                Facilities = new List<FacilityTypeEnum>(){ EditEightVOne }
            },
            new EditTypeEnumInfo() {
                Type = EightVThree,
                Inputs = 8,
                Outputs = 3,
                Facilities = new List<FacilityTypeEnum>(){ EditEightVThree }
            }
        };

        public static readonly IDictionary<EditTypeEnum,EditTypeEnumInfo> EditTypeEnumMap =
           AsKeyedDictionary<EditTypeEnum, EditTypeEnumInfo>(EditTypeEnumList);
    }


    public enum FilterTypeEnum {
        SelectFilter,
        PriorityMerge,
        PrioritySplit
    };

    public class TierInfo : KeyedElementIf<int> {

        // Tier 1: facilities (10c), simple make (x1), 1v1 transform, simple vend (x1), 

        // Tier 2: facilities (30c), 2v1 transform

        // Tier 3: facilities (100c), fair make (x2), 2v2 transform, select filter

        // Tier 4: facilities (300c), fair vend (x2c), 1v2 transform, priority merge

        // Tier 5: facilities (1000c), good make (x3), 3v1 transform, priority split

        // Tier 6: facilities (3000c), good vend (x3c), 3v2 transform

        public int Tier {  get; set; } = 1;

        public int Key {  get {  return Tier; } }

        public int FacilityCost { get; set; } = 10;

        public List<MakeTypeEnum> Makers { get; set; } = new List<MakeTypeEnum>();

        public List<VendTypeEnum> Venders { get; set; } = new List<VendTypeEnum>();

        public List<EditTypeEnum> Editors { get; set; } = new List<EditTypeEnum>();

        public List<FilterTypeEnum> Filters { get; set; } = new List<FilterTypeEnum>();

        /// <summary>
        /// Chain length thresholds, using Random.NextSingle()=float[0,1)
        /// </summary>
        public List<float> ChainLength { get; set; } = new List<float>();

//======================================================================================================================

        // readonly constants
        internal static List<T> AsEnumList<T>( params int[] indices ) {

            var list = new List<T>();
            foreach ( var index in indices ) {
                list.Add(  (T)(object)index );
            }
            return list;
        }


        public static List<TierInfo> TierList = new List<TierInfo>() {

            new TierInfo() {
                Tier = 1,
                FacilityCost = 10,
                Makers = AsEnumList<MakeTypeEnum>( 0 ),
                Venders = AsEnumList<VendTypeEnum>( 0 ),
                Editors = AsEnumList<EditTypeEnum>( 0 ),
                ChainLength = new List<float>(){ 0.5f, 0.75f, 0.95f, 1f }
            },
            new TierInfo() {
                Tier = 2,
                FacilityCost = 30,
                Makers = AsEnumList<MakeTypeEnum>( 0 ),
                Venders = AsEnumList<VendTypeEnum>( 0 ),
                Editors = AsEnumList<EditTypeEnum>( 0, 1 ),
                ChainLength = new List<float>(){ 0.1f, 0.45f, 0.8f, 0.95f, 1f }

            },
            new TierInfo() {
                Tier = 3,
                FacilityCost = 100,
                Makers = AsEnumList<MakeTypeEnum>( 0, 1 ),
                Venders = AsEnumList<VendTypeEnum>( 0 ),
                Editors = AsEnumList<EditTypeEnum>( 0, 1 ),
                Filters = AsEnumList<FilterTypeEnum>( 0 ),
                ChainLength = new List<float>(){ 0f, 0.25f, 0.5f, 0.85f, 0.95f, 1f }

            },
            new TierInfo() {
                Tier = 4,
                FacilityCost = 300,
                Makers = AsEnumList<MakeTypeEnum>( 0, 1 ),
                Venders = AsEnumList<VendTypeEnum>( 0, 1 ),
                Editors = AsEnumList<EditTypeEnum>( 0, 1, 2 ),
                Filters = AsEnumList<FilterTypeEnum>( 0, 1 ),
                ChainLength = new List<float>(){ 0f, 0.25f, 0.5f, 0.75f, 0.9f, 1f }

            },
            new TierInfo() {
                Tier = 5,
                FacilityCost = 1000,
                Makers = AsEnumList<MakeTypeEnum>( 0, 1, 2 ),
                Venders = AsEnumList<VendTypeEnum>( 0, 1 ),
                Editors = AsEnumList<EditTypeEnum>( 0, 1, 2, 3 ),
                Filters = AsEnumList<FilterTypeEnum>( 0, 1, 2 ),
                ChainLength = new List<float>(){ 0f, 0.25f, 0.45f, 0.75f, 0.85f, 0.95f, 1f }

            },
            new TierInfo() {
                Tier = 6,
                FacilityCost = 1000,
                Makers = AsEnumList<MakeTypeEnum>( 0, 1, 2 ),
                Venders = AsEnumList<VendTypeEnum>( 0, 1, 2 ),
                Editors = AsEnumList<EditTypeEnum>( 0, 1, 2, 3, 4 ),
                Filters = AsEnumList<FilterTypeEnum>( 0, 1, 2 ),
                ChainLength = new List<float>(){ 0f, 0.25f, 0.4f, 0.70f, 0.85f, 0.9f, 0.95f, 1f }

            }
        };

        public static readonly IDictionary<int,TierInfo> TierMap =
           AsKeyedDictionary<int, TierInfo>(TierList);

    }
}
