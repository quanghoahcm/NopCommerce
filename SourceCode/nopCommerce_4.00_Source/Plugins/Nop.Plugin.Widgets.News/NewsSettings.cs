using Nop.Core.Configuration;

namespace Nop.Plugin.Widgets.News
{
    public class NewsSettings : ISettings
    {
        public string WidgetZone { get; set; }
        public int QtdNewsPosts { get; set; }
    }
}