using JetBrains.Application.ComponentModel;
using JetBrains.Application.DataContext;
using JetBrains.ReSharper.Feature.Services.Navigation.ContextNavigation;
using JetBrains.ReSharper.Feature.Services.Navigation.Requests;
using JetBrains.ReSharper.Psi.CSharp;

namespace TeaPot {
    [ShellFeaturePart]
    public class GenericImplementationContextSearch : ImplementationContextSearch {

        protected override bool IsAvailableInternal(IDataContext dataContext) {
            return true;
        }

        public override bool IsContextApplicable(IDataContext dataContext) {
            return ContextNavigationUtil.CheckDefaultApplicability<CSharpLanguage>(dataContext);
        }

        protected override SearchImplementationsRequest CreateSearchRequest(IDataContext dataContext,
                                                                            DeclaredElementTypeUsageInfo element,
                                                                            DeclaredElementTypeUsageInfo initialTarget) {
            return RequestUtil.CreateRequest(dataContext, element, initialTarget);
        }
    }
}