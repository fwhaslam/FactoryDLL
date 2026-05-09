// Copyright (c) 2026 Frederick William Haslam born 1962 in the USA.
// Licensed under "The MIT License" https://opensource.org/license/mit/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryModel.Tools {

    public class IndexedDictionary<K,V> : IDictionary<K,V> {

        internal IndexedSet<K> KeyOrder = new IndexedSet<K>();
        internal Dictionary<K,V> Store = new Dictionary<K,V>();

        public K Key(int index) {
            return KeyOrder[index];
        }
        
        public V Value(int index) {
            return this[KeyOrder[index] ];
        }

        public V this[K key] { 
            get => Store[key]; 
            set {
                if (!Store.ContainsKey(key)) KeyOrder.Add(key);
                Store[key] = value; 
            }
        }

        public ICollection<K> Keys => Store.Keys;

        public ICollection<V> Values => Store.Values;

        public int Count => Store.Count;

        public bool IsReadOnly => throw new NotImplementedException();

        public void Add(K key, V value) {
            if (!Store.ContainsKey(key)) KeyOrder.Add(key);
            Store.Add(key,value);
        }

        public void Add(KeyValuePair<K, V> item) {
            throw new NotImplementedException();
        }

        public void Clear() {
            Store.Clear();
        }

        public bool Contains(KeyValuePair<K, V> item) {
            throw new NotImplementedException();
        }

        public bool ContainsKey(K key) {
            return Store.ContainsKey(key);
        }

        public void CopyTo(KeyValuePair<K, V>[] array, int arrayIndex) {
            throw new NotImplementedException();
        }

        public IEnumerator<KeyValuePair<K, V>> GetEnumerator() {
            return Store.GetEnumerator();
        }

        public bool Remove(K key) {
            var found = Store.Remove(key);
            if (found) KeyOrder.Remove(key);
            return found;
        }

        public bool Remove(KeyValuePair<K, V> item) {
            throw new NotImplementedException();
        }

        public bool TryGetValue(K key, out V value) {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }
    }
}
