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
        private readonly IEnumerable<IDeclaredType> _typeParams;

        public SearchGenericImplementationsRequest(IDeclaredElement declaredElement, 
                                                   ITypeElement originType,
                                                   ISearchDomain searchDomain, 
                                                   IEnumerable<IDeclaredType> typeParams) : base(declaredElement, originType, searchDomain)
        {
            _typeParams = typeParams;
        }

        public override ICollection<IOccurence> Search(IProgressIndicator progressIndicator)
        {
            var occurences = base.Search(progressIndicator);
            var genericOccurences = occurences.Where(x => IsEqualGeneric(x.GetDeclaredElement())).ToList();
            return genericOccurences.Any() ? genericOccurences : occurences;
        }

        private bool IsEqualGeneric(IDeclaredElement y)
        {
            if (!_typeParams.Any())
            {
                return true;
            }

            var ySuperTypes = TypeElementUtil.GetAllSuperTypes(DeclaredElementUtil.GetTopLevelTypeElement(y as IClrDeclaredElement));
            var yTypeParams = GetTypeParametersFromTypes(ySuperTypes);

            return yTypeParams.Any(yTypeParam => TypeCollectionsEqual(_typeParams, yTypeParam));
        }

        private static bool TypeCollectionsEqual(IEnumerable<IDeclaredType> typeParams, IEnumerable<IDeclaredType> types)
        {
            var comparer = new MultiSetComparer<IDeclaredType>();
            return comparer.Equals(typeParams, types);
        }

        private static IEnumerable<IEnumerable<IDeclaredType>> GetTypeParametersFromTypes(IEnumerable<IDeclaredType> types)
        {
            return types.Select(x => TypeParameterUtil.GetResolvedTypeParams(x.Resolve()));
        }
    }
}