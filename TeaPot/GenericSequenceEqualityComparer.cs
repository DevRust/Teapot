using System.Collections.Generic;
using System.Linq;
using JetBrains.ReSharper.Psi;

namespace GenericNavigator {
    public class GenericSequenceEqualityComparer : IEqualityComparer<IEnumerable<IDeclaredType>> {

        public bool Equals(IEnumerable<IDeclaredType> x, IEnumerable<IDeclaredType> y) {
            var mappedSet = x.Zip(y, (a, b) =>
                                     ((a.IsOpenType || b.IsOpenType)
                                          ? EqualityMode.OpenGeneric
                                          : a.Equals(b)
                                                ? EqualityMode.MatchSuccess
                                                : EqualityMode.MatchFail)).ToList();

            return (mappedSet.Any(result => result == EqualityMode.MatchSuccess) &&
                    mappedSet.All(result => result != EqualityMode.MatchFail)) ||
                   mappedSet.All(result => result == EqualityMode.OpenGeneric);
        }

        public int GetHashCode(IEnumerable<IDeclaredType> obj) {
            return obj.GetHashCode();
        }

        private enum EqualityMode {

            MatchSuccess,
            MatchFail,
            OpenGeneric,

        }

    }
}