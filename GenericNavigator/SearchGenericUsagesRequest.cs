using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Application.Progress;
using JetBrains.ReSharper.Feature.Services.Navigation;
using JetBrains.ReSharper.Feature.Services.Navigation.Requests;
using JetBrains.ReSharper.Feature.Services.Occurences;
using JetBrains.ReSharper.Feature.Services.Occurences.OccurenceInformation;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Modules;
using JetBrains.ReSharper.Psi.Search;
using JetBrains.ReSharper.Psi.Util;
using DeclaredElementUtil = JetBrains.ReSharper.Feature.Services.ExternalSources.Utils.DeclaredElementUtil;

namespace GenericNavigator {
    public class SearchGenericUsagesRequest : SearchDeclaredElementUsagesRequest
    {
        private readonly IEnumerable<IDeclaredType> _originTypeParams;
        
        public SearchGenericUsagesRequest(ICollection<DeclaredElementInstance> elements,
                                          ICollection<DeclaredElementInstance> initialTargets,
                                          ISearchDomain searchDomain,
                                          IEnumerable<IDeclaredType> originTypeParams)
            : base(elements, initialTargets, SearchPattern.FIND_USAGES, searchDomain) {
            _originTypeParams = originTypeParams;
        }

        public override ICollection<IOccurence> Search(IProgressIndicator progressIndicator)
        {
            var occurences = base.Search(progressIndicator);

            if (_originTypeParams == null || !_originTypeParams.Any())
            {
                return occurences;
            }

            var genericOccurences = occurences.Where(IsEqualGeneric).ToList();
            return genericOccurences;
        }

        public override bool IsAsyncSupported
        {
            get { return false; }
        }

        private bool IsEqualGeneric(IOccurence occurence) {
            var reference = ((ReferenceOccurence)occurence).PrimaryReference;
            var elementTypeParams = TypeParameterUtil.GetResolvedTypeParams(reference.CurrentResolveResult.Result);

            return new GenericSequenceEqualityComparer().Equals(elementTypeParams, _originTypeParams);
        }
    }
}