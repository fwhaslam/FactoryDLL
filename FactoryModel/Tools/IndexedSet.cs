// Copyright (c) 2026 Frederick William Haslam born 1962 in the USA.
// Licensed under "The MIT License" https://opensource.org/license/mit/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryModel.Tools {

    /// <summary>
    /// Readable via index, but not writable.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class IndexedSet<T> : ISet<T> {

        internal IList<T> Store { get; set; } = new List<T>(); 

        /// <summary>
        /// Index access.
        /// </summary>
        /// <param name="ix"></param>
        /// <returns></returns>
        public T this[int ix] {  get { return Store[ix]; } }

        public int Count => Store.Count;

        public bool IsReadOnly => false;

        public bool Add(T item) {
            if (Store.Contains(item)) return false;
            Store.Add( item );
            return true;
        }

        public void Clear() {
            Store.Clear();
        }

        public bool Contains(T item) {
            return Store.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex) {
            throw new NotImplementedException();
        }

        public void ExceptWith(IEnumerable<T> other) {
            foreach( var item in other ) Remove(item);
        }

        public IEnumerator<T> GetEnumerator() {
            return Store.GetEnumerator();
        }

        public void IntersectWith(IEnumerable<T> other) {
            var work = new List<T>();
            foreach ( var item in other ) {
                if (Store.Contains(item)) work.Add(item);
            }
            Store = work;
        }

        public bool IsProperSubsetOf(IEnumerable<T> other) {
           throw new NotImplementedException();
        }

        public bool IsProperSupersetOf(IEnumerable<T> other) {
           throw new NotImplementedException();
        }

        public bool IsSubsetOf(IEnumerable<T> other) {
            foreach ( var item in other ) {
                if (!Store.Contains(item)) return false;
            }
            return true;
        }

        public bool IsSupersetOf(IEnumerable<T> other) {
            foreach ( var item in Store ) {
                if (!other.Contains(item)) return false;
            }
            return true;
        }

        public bool Overlaps(IEnumerable<T> other) {
            foreach ( var item in other ) {
                if (Store.Contains(item)) return true;
            }
            return false;
        }

        public bool Remove(T item) {
            if (!Store.Contains(item)) return false;
            Store.Remove(item);
            return true;
        }

        public bool SetEquals(IEnumerable<T> other) {
            var otherCount = 0;
            foreach ( var item in other ) {
                if (!Store.Contains(item)) return false;
                otherCount++;
            }
            return ( Count==otherCount );
        }

        public void SymmetricExceptWith(IEnumerable<T> other) {
            var work = new List<T>();
            foreach ( var item in other ) {
                if ( !Store.Contains(item)) work.Add(item);
            }
            foreach ( var item in Store ) {
                if ( !other.Contains(item)) work.Add(item);
            }
            Store = work;
        }

        public void UnionWith(IEnumerable<T> other) {
            foreach ( var item in other ) {
                Add( item );
            }
        }

        void ICollection<T>.Add(T item) {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }
    }
}
