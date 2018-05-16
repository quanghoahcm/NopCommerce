using Nop.Web.Framework.Mvc.ModelBinding;
using Nop.Web.Framework.Mvc.Models;

namespace Nop.Plugin.Widgets.News.Models
{
    public class ConfigurationModel : BaseNopModel
    {
        public int ActiveStoreScopeConfiguration { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.News.Fields.WidgetZone")]
        [AllowHtml]
        public string WidgetZone { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.News.Fields.QtdNewsPosts")]
        [AllowHtml]
        public int QtdNewsPosts { get; set; }
    }
}