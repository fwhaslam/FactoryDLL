// Copyright (c) 2026 Frederick William Haslam born 1962 in the USA.
// Licensed under "The MIT License" https://opensource.org/license/mit/

using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace FactoryModel.Tools {

    /// <summary>
    /// Key is any System.Object, and is used to build dictionaries.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface KeyedElementIf<T> {
		T Key { get; }
	}

    /// <summary>
    /// Name is a string used as a Key, and is used to build dictionaries.
    /// </summary>
    public interface NamedElementIf {
		string Name { get; }
	}

    /// <summary>
    /// Fields are a Map of string keys to 'field' objects which define the attributes of some entity.
    /// </summary>
	public interface FieldedElementIf {
        Dictionary<string,object> Fields { get; }
	}

    /// <summary>
    /// Traits are a Map of string keys which indicate specific traits held by some entity.
    /// </summary>
	public interface TraitedElementIf {
        SortedSet<string> Traits { get; }
	}

	public class SystemTools {

		public static T ParseEnum<T>(string what) {
			try {
				return (T)Enum.Parse(typeof(T), what);
			}
			catch (Exception ex) {
				throw new SystemException("Failed to parse [" + what + "]", ex);
			}
		}


		public static T ParseEnum<T>(string what, T defaultValue ) {
			try {
				if (what==null || what.Length==0) return defaultValue;
				return (T)Enum.Parse(typeof(T), what);
			}
			catch (Exception ex) {
				throw new SystemException("Failed to parse [" + what + "]", ex);
			}
		}

		public static Nullable<T> ParseEnumNullable<T>(string what) where T : struct, Enum {
			T result;
			if (Enum.TryParse<T>( what, out result )) return result;
			return null;
		}

		public static string AsString(object what) {
			if (what == null) return null;
			return what.ToString();
		}

		public static bool IsStringEmpty( string what ) {
			if (what==null) return true;
			if (what.Length==0) return true;
			return false;
		}

		public static int AsInt(object what) {
			try {
				//if (what==null) return 0;
				//if (what.GetType() == typeof(int)) return (int)what;
				return Convert.ToInt32(what); 
			}
			catch (Exception ex) {
				throw new SystemException("Unable to parse as int [" + what + "]", ex);
			}
		}

		/// <summary>
		/// Convert object to float, throw informative exception when we fail.
		/// </summary>
		/// <param name="what"></param>
		/// <returns></returns>
		public static float AsFloat(object what) {
			try {
				//if (what==null) return 0f;
				//if (what.GetType() == typeof(float)) return (float)what;
				return Convert.ToSingle(what); 
			}
			catch (Exception ex) {
				throw new SystemException("Unable to parse as float [" + what + "]", ex);
			}
		}

		/// <summary>
		/// Summarize number to short string, dropping zeros when unnecessary.
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public static string ToNumberPrecis( float value ) {

			value = (float)Math.Round( value * 100f, 2 ) / 100f;
			var precis = value.ToString("0.00").ToCharArray().ToList();

			precis.Reverse();
			if (precis[0] == '0') {
				precis.RemoveAt(0);		// '0'
				if (precis[0] == '0') {
					precis.RemoveAt(1);	// '.'
					precis.RemoveAt(0);	// '0'
				}
			}
			precis.Reverse();
			return String.Join( "", precis );
		}

		/// <summary>
		/// Null safe string join.
		/// </summary>
		/// <param name="splice"></param>
		/// <param name="ary"></param>
		/// <returns></returns>
		public static string AsJoinedString( string splice, IEnumerator<string> ary ) {
			if (ary==null) return null;
			return String.Join( splice, ary );
		}
		public static string AsJoinedString( string splice, params string[] ary ) {
			if (ary==null) return null;
			return String.Join( splice, ary );
		}

//======================================================================================================================
//	Determine object type for manipulating Fields
//======================================================================================================================

		public static bool IsNumber(object obj) {
            switch (Type.GetTypeCode(obj.GetType())) {
                case TypeCode.Byte:
                case TypeCode.SByte:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.Decimal:
                case TypeCode.Double:
                case TypeCode.Single:
                    return true;
                default:
                    return false;
            }
        }

		public static bool IsString(object obj) {
            switch (Type.GetTypeCode(obj.GetType())) {
                case TypeCode.String:
                    return true;
                default:
                    return false;
            }
        }

		public static bool IsBool(object obj) {
            switch (Type.GetTypeCode(obj.GetType())) {
                case TypeCode.Boolean:
                    return true;
                default:
                    return false;
            }
        }

//======================================================================================================================

		public static bool IsEmpty( string value ) {
			return ( value==null || value.Length<1 );
		}

		public static bool IsEmpty( ICollection list ) {
			if (list==null) return true;
			if (list.Count==0) return true;
			return false;
		}

		public static bool HasTrait( TraitedElementIf work, string traitKey ) {
			return work.Traits.Contains( traitKey );
		}

//======================================================================================================================

		/// <summary>
		///  Create dictionary, and throw detailed exception for conflicts.
		/// </summary>
		/// <typeparam name="V"></typeparam>
		/// <param name="list"></param>
		/// <returns></returns>
		public static Dictionary<string, V> AsDictionary<V>(ICollection<V> list) where V : NamedElementIf {

			var map = new Dictionary<string, V>();
			foreach (var entry in list) {
				if (map.ContainsKey(entry.Name))
					throw new SystemException("ToDictionary has duplicate name [" + entry.Name + "]");
				map[entry.Name] = entry;
			}
			return map;
		}

		/// <summary>
		///  Create dictionary, and throw detailed exception for conflicts.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="list"></param>
		/// <returns></returns>
        public static IDictionary<K, V> AsKeyedDictionary<K,V>(ICollection<V> list) where V : KeyedElementIf<K> {

			var map = new SortedDictionary<K, V>();
			foreach (var entry in list) {
				if (map.ContainsKey(entry.Key))
					throw new SystemException("ToKeyedDictionary has duplicate key [" + entry.Key + "]");
				map[entry.Key] = entry;
			}
			return map;
		}

		public static void MergeDictionary<V>( Dictionary<string,V> map, List<V> merge ) where V : NamedElementIf {
			foreach (var entry in merge) {
				if (map.ContainsKey(entry.Name))
					throw new SystemException("MergeDictionary(list) has duplicate name [" + entry.Name + "]");
				map[entry.Name] = entry;
			}
		}

		public static void MergeKeyedDictionary<K,V>( IDictionary<K,V> map, List<V> merge ) where V : KeyedElementIf<K> {
			foreach (var entry in merge) {
				if (map.ContainsKey(entry.Key))
					throw new SystemException("MergeKeyedDictionary(list) has duplicate key [" + entry.Key + "]");
				map[entry.Key] = entry;
			}
		}

		public static void MergeDictionary<K,v>( Dictionary<K,v> map, Dictionary<K,v> merge ) {
			foreach ( var entry in merge ) {
				if (map.ContainsKey(entry.Key))
					throw new SystemException("MergeDictionary has duplicate key [" + entry.Key + "]");
				map[entry.Key] = entry.Value;
			}
		}

//======================================================================================================================
//	Generic Getter + Setter intended for fields set by Starland Drivers
//======================================================================================================================

		public static T GetField<T>( FieldedElementIf box, string key, T alt ) {
			if (!box.Fields.ContainsKey(key)) return alt;
			return (T)box.Fields[key];
		}

		public static void SetField<T>( FieldedElementIf box, string key, T value ) {
			box.Fields[key] = value;
		}

//======================================================================================================================
//	Customizing Getter + Setter intended for fields read from YAML Model
//======================================================================================================================

		public static object GetField( FieldedElementIf box, string fieldKey ) {
			if (!box.Fields.ContainsKey(fieldKey)) return null;
			return box.Fields[fieldKey];
		}
		
		public static void SetField( FieldedElementIf box, string fieldKey, object value ) {
			box.Fields[fieldKey] = value;
		}

//======================================================================================================================

		public static string GetStringField( FieldedElementIf box, string fieldKey ) {
			if (!box.Fields.ContainsKey(fieldKey)) return null;
			return box.Fields[fieldKey].ToString();
		}

		public static int GetIntField( FieldedElementIf box, string fieldKey ) {
			try {
				if (!box.Fields.ContainsKey(fieldKey)) return 0;
				var what = box.Fields[fieldKey];
				//if (what==null) return 0;
				return Convert.ToInt32(what); 
			}
			catch (Exception ex) {
				throw new SystemException("Unable to apply GetIntField for (" + fieldKey + ")", ex);
			}
		}


		public static float GetFloatField( FieldedElementIf box, string fieldKey ) {
			try {
				if (!box.Fields.ContainsKey(fieldKey)) return 0f;
				var what = box.Fields[fieldKey];
				//if (what==null) return 0f;
				return Convert.ToSingle(what); 
			}
			catch (Exception ex) {
				throw new SystemException("Unable to apply GetFloatField for (" + fieldKey + ")", ex);
			}
		}

		public static string[] GetStringArrayField( FieldedElementIf from, string key ) {
			
			var items = GetField(from,key);
			if (items==null) return null;

			var itemType = items.GetType();
			if (typeof(string[]).IsAssignableFrom(itemType)) {
				return (string[])items;
			}

			// reshape field as array, then return
			if ( typeof(IEnumerable).IsAssignableFrom(itemType)) {
				string[] ary = ((IEnumerable)items).Cast<object>().Select( (i) => Convert.ToString(i) ).ToArray();
				SetField( from, key, ary );
				return ary;
			}

			throw new SystemException("Could not parse element as string[] = "+items.ToString());
		}
		
		//public static void SetIntField( FieldedElementIf box, string key, int value ) {
		//	box.Fields[key] = value;
		//}
		//public static void SetIntField( FieldedElementIf box, FieldInfo info, int value ) {
		//	box.Fields[info.Name] = value;
		//}

		//public static void SetFloatField( FieldedElementIf box, string key, float value ) {
		//	box.Fields[key] = value;
		//}
		//public static void SetFloatField( FieldedElementIf box, FieldInfo info, float value ) {
		//	box.Fields[info.Name] = value;
		//}

	}
}
