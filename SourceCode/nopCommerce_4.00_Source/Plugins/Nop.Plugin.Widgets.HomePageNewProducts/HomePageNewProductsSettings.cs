using Nop.Core.Configuration;

namespace Nop.Plugin.Widgets.HomePageNewProducts
{
    public class HomePageNewProductsSettings : ISettings
    {
        public string WidgetZone { get; set; }
        public int NumberOfProductsToShow { get; set; }
    }
}
