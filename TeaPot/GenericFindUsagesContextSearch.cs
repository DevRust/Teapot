using System.Collections.Generic;
using JetBrains.Application.ComponentModel;
using JetBrains.Application.DataContext;
using JetBrains.Application.Settings;
using JetBrains.DataFlow;
using JetBrains.ReSharper.Feature.Services.Navigation.ContextNavigation;
using JetBrains.ReSharper.Feature.Services.Navigation.Requests;
using JetBrains.ReSharper.Features.Navigation.Features.FindUsages;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp;
using JetBrains.ReSharper.Psi.Search;

namespace TeaPot {
    [ShellFeaturePart]
    public class GenericFindUsagesContextSearch : FindUsagesContextSearch {

        protected override bool IsAvailableInternal(IDataContext dataContext) {
            return true;
        }

        public override bool IsContextApplicable(IDataContext dataContext) {
            return ContextNavigationUtil.CheckDefaultApplicability<CSharpLanguage>(dataContext);
        }
        
        protected override SearchDeclaredElementUsagesRequest CreateSearchRequest(IDataContext context, 
            ICollection<DeclaredElementInstance> elements,
            ICollection<DeclaredElementInstance> initialTargets, 
            ISearchDomain searchDomain) {
            return RequestUtil.CreateRequest(context, elements, initialTargets);
        }

        public GenericFindUsagesContextSearch(Lifetime lifetime, ISettingsStore settingsStore) : base(lifetime, settingsStore) {}
    }
}