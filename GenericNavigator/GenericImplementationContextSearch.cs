using System.Linq;
using JetBrains.Application.DataContext;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp;
using JetBrains.ReSharper.Psi.ExtensionsAPI.Resolve;
using JetBrains.ReSharper.Psi.Resolve;
using DataConstants = JetBrains.ReSharper.Psi.Services.DataConstants;
using JetBrains.Application.ComponentModel;
using JetBrains.ReSharper.Feature.Services.Navigation.ContextNavigation;
using JetBrains.ReSharper.Feature.Services.Navigation.Requests;
using JetBrains.Application.BuildScript.Application.Zones;

namespace GenericNavigator {

    [ZoneMarker]
    public class ZoneMarker
    {
    }

    [ShellFeaturePart]
    public class GenericImplementationContextSearch : ImplementationContextSearch {

        public override bool IsAvailable(IDataContext dataContext) {
            return true;
        }

        public override bool IsContextApplicable(IDataContext dataContext) {
            return ContextNavigationUtil.CheckDefaultApplicability<CSharpLanguage>(dataContext);
        }

        protected override SearchImplementationsRequest CreateSearchRequest(IDataContext dataContext,
                                                                            IDeclaredElement declaredElement,
                                                                            IDeclaredElement initialTarget) {
            var originTypeElement = GetOriginTypeElement(dataContext, initialTarget);
            var searchDomain = SearchDomainContextUtil.GetSearchDomainContext(dataContext)
                                                      .GetDefaultDomain().SearchDomain;
            var typeParams = TypeParameterUtil.GetTypeParametersFromContext(dataContext);

            return new SearchGenericImplementationsRequest(declaredElement, originTypeElement, searchDomain, typeParams);
        }

        private static ITypeElement GetOriginTypeElement(IDataContext dataContext, IDeclaredElement initialTarget) {
            var data = dataContext.GetData(DataConstants.REFERENCES);
            if (data == null) {
                return null;
            }

            foreach (var current in data.OfType<IReferenceWithQualifier>()) {
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