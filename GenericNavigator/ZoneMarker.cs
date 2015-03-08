using JetBrains.Application.BuildScript.Application.Zones;
using JetBrains.ReSharper.Feature.Services.Navigation;

namespace GenericNavigator {
    [ZoneMarker]
    public class ZoneMarker : IRequire<NavigationZone>
    {
    }
}