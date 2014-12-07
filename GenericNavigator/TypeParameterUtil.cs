using System.Collections.Generic;
using System.Linq;
using JetBrains.Application.DataContext;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Resolve;
using DataConstants = JetBrains.ReSharper.Psi.Services.DataConstants;

namespace GenericNavigator
{
    public class TypeParameterUtil
    {
        public static IEnumerable<IDeclaredType> GetResolvedTypeParams(IResolveResult resolution)
        {
            var sub = resolution.Substitution;
            var domainList = sub.Domain.ToList();
            var typeParams = domainList.Select(x => sub.Apply(x).GetScalarType());

            return typeParams;
        }

        public static IEnumerable<IDeclaredType> GetTypeParametersFromContext(IDataContext context)
        {
            var reference = context.GetData(DataConstants.REFERENCE);
            if (reference == null) return null;
            if (reference.CurrentResolveResult == null) return null;
            var typeParams = GetResolvedTypeParams(reference.CurrentResolveResult.Result);

            return typeParams;
        }
    }
}