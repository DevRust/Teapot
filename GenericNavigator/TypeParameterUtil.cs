using System.Collections.Generic;
using System.Linq;
using JetBrains.Application.DataContext;
using JetBrains.ReSharper.Feature.Services.Navigation.ContextNavigation;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.ExtensionsAPI.Resolve;
using JetBrains.ReSharper.Psi.Resolve;
using DataConstants = JetBrains.ReSharper.Psi.Services.DataConstants;

namespace GenericNavigator {
    public static class TypeParameterUtil {

        public static IEnumerable<IDeclaredType> GetResolvedTypeParams(IResolveResult resolution) {
            var sub = resolution.Substitution;
            var domainList = sub.Domain.ToList().OrderBy(x => x.Index);
            var typeParams = domainList.Select(x => sub.Apply(x).GetScalarType());

            return typeParams;
        }

        public static IEnumerable<IDeclaredType> GetTypeParametersFromContext(IDataContext context) {
            var reference = context.GetData(DataConstants.REFERENCE);
            if (reference == null || reference.CurrentResolveResult == null) {
                return null;
            }

            var typeParams = GetResolvedTypeParams(reference.CurrentResolveResult.Result);

            return typeParams;
        }

        public static ITypeElement GetOriginTypeElement(IDataContext dataContext,
                                                        DeclaredElementTypeUsageInfo initialTarget) {
            var data = dataContext.GetData(DataConstants.REFERENCES);
            if (data == null) {
                return null;
            }

            foreach (var current in data.OfType<IQualifiableReference>()) {
                if (!Equals(current.Resolve().DeclaredElement, initialTarget)) {
                    continue;
                }

                var qualifierWithTypeElement = current.GetQualifier() as IQualifierWithTypeElement;

                if (qualifierWithTypeElement == null) {
                    continue;
                }

                var qualifierTypeElement = qualifierWithTypeElement.GetQualifierTypeElement();
                if (qualifierTypeElement != null) {
                    return qualifierTypeElement;
                }
            }
            return null;
        }

    }
}