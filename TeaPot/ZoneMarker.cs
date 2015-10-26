using JetBrains.Application.BuildScript.Application.Zones;
using JetBrains.ReSharper.Feature.Services.Navigation;

namespace TeaPot {
    [ZoneMarker]
    public class ZoneMarker : IRequire<NavigationZone>
    {
    }
}