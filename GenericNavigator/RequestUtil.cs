using System;
using System.Collections.Generic;
using JetBrains.Application.DataContext;
using JetBrains.ReSharper.Feature.Services.Navigation.ContextNavigation;
using JetBrains.ReSharper.Feature.Services.Navigation.Requests;
using JetBrains.ReSharper.Psi;

namespace GenericNavigator {
    static internal class RequestUtil {

        public static SearchImplementationsRequest CreateRequest(IDataContext dataContext, 
            DeclaredElementTypeUsageInfo element, 
            DeclaredElementTypeUsageInfo initialTarget) {

            var originTypeElement = TypeParameterUtil.GetOriginTypeElement(dataContext, initialTarget);
            var searchDomain = SearchDomainContextUtil.GetSearchDomainContext(dataContext)
                                                      .GetDefaultDomain().SearchDomain;
            var typeParams = TypeParameterUtil.GetTypeParametersFromContext(dataContext);

            return new SearchGenericImplementationsRequest(element, originTypeElement, searchDomain, typeParams);
        }

        internal static SearchDeclaredElementUsagesRequest CreateRequest(IDataContext context, 
            ICollection<DeclaredElementInstance> elements, 
            ICollection<DeclaredElementInstance> initialTargets)
        {
            var searchDomain = SearchDomainContextUtil.GetSearchDomainContext(context)
                                                      .GetDefaultDomain().SearchDomain;
            var typeParams = TypeParameterUtil.GetTypeParametersFromContext(context);

            return new SearchGenericUsagesRequest(elements, initialTargets, searchDomain, typeParams);
        }
    }
}