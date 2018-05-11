using Nop.Core.Configuration;

namespace Nop.Plugin.Widgets.HomePageNewProductsPlugin
{
    public class HomePageNewProductsPluginSettings : ISettings
    {
        public string WidgetZone { get; set; }
        public int NumberOfProductsToShow { get; set; }
    }
}
