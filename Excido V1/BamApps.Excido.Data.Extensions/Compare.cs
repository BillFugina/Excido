using System;
using System.Collections.Generic;
using System.Linq;

namespace BamApps.Excido.Data.Extensions {
    public static class Compare {
        public static IEnumerable<T> Union<T, TKey>(this IEnumerable<T> thisList, IEnumerable<T> otherList, Func<T, TKey> lookup) where TKey : struct {
            return thisList.Union(otherList, new StructEqualityComparer<T, TKey>(lookup));
        }

        public static IEnumerable<T> Distinct<T, TKey>(this IEnumerable<T> list, Func<T, TKey> lookup) where TKey : struct {
            return list.Distinct(new StructEqualityComparer<T, TKey>(lookup));
        }

        class StructEqualityComparer<T, TKey> : IEqualityComparer<T> where TKey : struct {

            readonly Func<T, TKey> lookup;

            public StructEqualityComparer(Func<T, TKey> lookup) {
                this.lookup = lookup;
            }

            public bool Equals(T x, T y) {
                return lookup(x).Equals(lookup(y));
            }

            public int GetHashCode(T obj) {
                return lookup(obj).GetHashCode();
            }
        }

        public static IEnumerable<T> Union<T>(this IEnumerable<T> thisList, IEnumerable<T> otherList, Func<T, T, bool> compare) {
            return thisList.Union(otherList, new EqualityComparer<T>(compare));
        }

        public static IEnumerable<T> Distinct<T>(this IEnumerable<T> thisList, IEnumerable<T> otherList, Func<T, T, bool> compare) {
            return thisList.Distinct(new EqualityComparer<T>(compare));
        }

        public static IEnumerable<T> Distinct<T>(this IEnumerable<T> list, Func<T, T, bool> compare) {
            return list.Distinct(new EqualityComparer<T>(compare));
        }

        class EqualityComparer<T> : IEqualityComparer<T> {

            readonly Func<T, T, bool> compare;

            public EqualityComparer(Func<T, T, bool> compareFunction) {
                this.compare = compareFunction;
            }

            public bool Equals(T x, T y) {
                return compare(x, y);
            }

            public int GetHashCode(T obj) {
                return compare.GetHashCode();
            }
        }

    }
}
