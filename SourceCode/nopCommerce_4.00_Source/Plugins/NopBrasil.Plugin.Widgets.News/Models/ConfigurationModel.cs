using Nop.Web.Framework;
using Nop.Web.Framework.Mvc;
using Nop.Web.Framework.Mvc.ModelBinding;
using Nop.Web.Framework.Mvc.Models;

namespace NopBrasil.Plugin.Widgets.News.Models
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