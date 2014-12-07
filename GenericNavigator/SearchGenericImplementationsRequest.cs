using System.Collections.Generic;
using System.Linq;
using JetBrains.Application.Progress;
using JetBrains.ReSharper.Feature.Services.ExternalSources.Utils;
using JetBrains.ReSharper.Feature.Services.Navigation.Search;
using JetBrains.ReSharper.Feature.Services.Navigation.Search.SearchRequests;
using JetBrains.ReSharper.Feature.Services.Occurences;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Search;
using TypeElementUtil = JetBrains.ReSharper.Psi.Util.TypeElementUtil;

namespace GenericNavigator
{
    public class SearchGenericImplementationsRequest : SearchImplementationsRequest
    {
        private readonly IEnumerable<IDeclaredType> _originTypeParams;

        public SearchGenericImplementationsRequest(IDeclaredElement declaredElement, 
                                                   ITypeElement originType,
                                                   ISearchDomain searchDomain, 
                                                   IEnumerable<IDeclaredType> originTypeParams) : base(declaredElement, originType, searchDomain)
        {
            _originTypeParams = originTypeParams;
        }

        public override ICollection<IOccurence> Search(IProgressIndicator progressIndicator)
        {
            var occurences = base.Search(progressIndicator);

            if (!_originTypeParams.Any())
            {
                return occurences;
            }

            var genericOccurences = occurences.Where(x => IsEqualGeneric(x.GetDeclaredElement())).ToList();
            return genericOccurences.Any() ? genericOccurences : occurences;
        }

        private bool IsEqualGeneric(IDeclaredElement element)
        {
            var elementSuperTypes = TypeElementUtil.GetAllSuperTypes(DeclaredElementUtil.GetTopLevelTypeElement(element as IClrDeclaredElement));
            var elementSuperTypeParams = GetTypeParametersFromTypes(elementSuperTypes);

            //return elementSuperTypeParams.Any(elementTypeParams => TypeCollectionsEqual(_originTypeParams, elementTypeParams));
            return elementSuperTypeParams.Any(elementTypeParams => _originTypeParams.SequenceEqual(elementTypeParams.Reverse()));
        }

        private static bool TypeCollectionsEqual(IEnumerable<IDeclaredType> left, IEnumerable<IDeclaredType> right)
        {
            var comparer = new MultiSetComparer<IDeclaredType>();
            return comparer.Equals(left, right);
        }

        private static IEnumerable<IEnumerable<IDeclaredType>> GetTypeParametersFromTypes(IEnumerable<IDeclaredType> types)
        {
            return types.Select(x => TypeParameterUtil.GetResolvedTypeParams(x.Resolve()));
        }
    }
}