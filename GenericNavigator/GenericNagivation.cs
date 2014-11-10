using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Application.DataContext;
using JetBrains.ReSharper.Feature.Services;
using JetBrains.ReSharper.Feature.Services.ContextNavigation;
using JetBrains.ReSharper.Feature.Services.Navigation.Search;
using JetBrains.ReSharper.Feature.Services.Navigation.Search.SearchRequests;
using JetBrains.ReSharper.Feature.Services.Occurences;
using JetBrains.ReSharper.Features.Common.Occurences.ExecutionHosting;
using JetBrains.ReSharper.Features.Finding.FindImplementations;
using JetBrains.ReSharper.Features.Finding.GoToImplementation;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Resolve;
using JetBrains.ReSharper.Psi.Util;
using DataConstants = JetBrains.ReSharper.Psi.Services.DataConstants;
using DeclaredElementUtil = JetBrains.ReSharper.Feature.Services.ExternalSources.Utils.DeclaredElementUtil;

namespace GenericNavigator
{
    [ContextNavigationProvider]
    public class GenericReferenceProvider : GotoImplementationProvider
    {
        public GenericReferenceProvider(IFeaturePartsContainer manager) : base(manager)
        {
        }

        protected override void ShowResults(IDataContext context, INavigationExecutionHost host, SearchImplementationsRequest searchRequest,
            ICollection<IOccurence> occurences, Func<SearchImplementationsDescriptor> descriptorBuilder)
        {
            var actualOccurences = occurences;

            var typeParams = GetTypeParametersFromContext(context);

            if (typeParams != null && typeParams.Any())
            {
                var typeParamList = typeParams.ToList();
                var occurenceMap = GetSuperClassesOfOccurences(occurences);
                var types = GetTypeParametersFromOccurenceMap(occurenceMap);

                actualOccurences = GetActualOccurences(typeParamList, types).ToList();
            }

            base.ShowResults(context, host, searchRequest, actualOccurences, descriptorBuilder);
        }

        private IEnumerable<IOccurence> GetActualOccurences(IEnumerable<IDeclaredType> typeParams, IDictionary<IOccurence, IEnumerable<IEnumerable<IDeclaredType>>> typeMap)
        {
            return typeMap
                .Where(kvp => kvp.Value
                    .Any(x => TypeCollectionsEqual(typeParams, x)))
                .Select(x => x.Key);
        }

        private bool TypeCollectionsEqual(IEnumerable<IDeclaredType> typeParams, IEnumerable<IDeclaredType> types)
        {
            var comparer = new MultiSetComparer<IDeclaredType>();

            return comparer.Equals(typeParams, types);
        }

        private Dictionary<IOccurence, IDeclaredType[]> GetSuperClassesOfOccurences(IEnumerable<IOccurence> occurences)
        {
            var superTypeCollections =
                occurences.ToDictionary(x => x,
                    x =>
                        DeclaredElementUtil.GetTopLevelTypeElement((x.GetDeclaredElement() as IClrDeclaredElement))
                            .GetAllSuperTypes());
            return superTypeCollections;
        }

        private IDictionary<IOccurence, IEnumerable<IEnumerable<IDeclaredType>>> GetTypeParametersFromOccurenceMap(
            Dictionary<IOccurence, IDeclaredType[]> occurenceMap)
        {
            return occurenceMap
                .ToDictionary(item => item.Key, item => item.Value
                    .Select(x => GetResolvedTypeParams(x.Resolve())));
        }

        private IEnumerable<IDeclaredType> GetTypeParametersFromContext(IDataContext context)
        {
            var reference = context.GetData(DataConstants.REFERENCE);
            if (reference == null) return null;
            if (reference.CurrentResolveResult == null) return null;

            var typeParams = GetResolvedTypeParams(reference.CurrentResolveResult.Result);

            return typeParams;
        }

        private IEnumerable<IDeclaredType> GetResolvedTypeParams(IResolveResult resolution)
        {
            var sub = resolution.Substitution;
            var domainList = sub.Domain.ToList();
            var typeParams = domainList.Select(x => sub.Apply(x).GetScalarType());

            return typeParams;
        }
    }
}
