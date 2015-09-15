using System.Collections.Generic;
using System.Linq;
using JetBrains.Application.Progress;
using JetBrains.ReSharper.Feature.Services.Navigation.ContextNavigation;
using JetBrains.ReSharper.Feature.Services.Occurences;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Search;
using JetBrains.ReSharper.Psi.Util;
using DeclaredElementUtil = JetBrains.ReSharper.Feature.Services.ExternalSources.Utils.DeclaredElementUtil;
using JetBrains.ReSharper.Feature.Services.Navigation.Requests;

namespace GenericNavigator {
    public class SearchGenericImplementationsRequest : SearchImplementationsRequest {

        private readonly IEnumerable<IDeclaredType> _originTypeParams;

        public SearchGenericImplementationsRequest(DeclaredElementTypeUsageInfo declaredElement,
                                                   ITypeElement originType,
                                                   ISearchDomain searchDomain,
                                                   IEnumerable<IDeclaredType> originTypeParams)
            : base(declaredElement, originType, searchDomain) {
            _originTypeParams = originTypeParams;
        }

        public override ICollection<IOccurence> Search(IProgressIndicator progressIndicator) {
            var occurences = base.Search(progressIndicator);

            if (_originTypeParams == null || !_originTypeParams.Any()) {
                return occurences;
            }

            var genericOccurences = occurences.Where(x => IsEqualGeneric(x.GetDeclaredElement())).ToList();
            return genericOccurences;
        }

        private bool IsEqualGeneric(IDeclaredElement element) {
            var topLevelTypeElement = DeclaredElementUtil.GetTopLevelTypeElement(element as IClrDeclaredElement);
            var elementSuperTypes = TypeElementUtil.GetAllSuperTypesReversed(topLevelTypeElement);
            var elementSuperTypeParams = GetTypeParametersFromTypes(elementSuperTypes).Where(x => x.Any());

            return new GenericSequenceEqualityComparer().Equals(elementSuperTypeParams.First(), _originTypeParams);
        }

        private static IEnumerable<IEnumerable<IDeclaredType>> GetTypeParametersFromTypes(
            IEnumerable<IDeclaredType> types) {
            return types.Select(x => TypeParameterUtil.GetResolvedTypeParams(x.Resolve()));
        }

    }
}