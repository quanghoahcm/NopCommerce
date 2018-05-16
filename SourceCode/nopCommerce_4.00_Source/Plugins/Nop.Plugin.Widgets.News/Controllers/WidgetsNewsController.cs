using Nop.Plugin.Widgets.News.Models;
using Nop.Services.Configuration;
using Nop.Plugin.Widgets.News.Service;
using Microsoft.AspNetCore.Mvc;
using Nop.Web.Framework;
using Nop.Web.Controllers;

namespace Nop.Plugin.Widgets.News.Controllers
{
    [Area(AreaNames.Admin)]
    public class WidgetsNewsController : BasePublicController
    {
        private readonly ISettingService _settingService;
        private readonly NewsSettings _NewsSettings;
        private readonly IWidgetNewsService _widgetNewsService;

        public WidgetsNewsController(ISettingService settingService,
            NewsSettings NewsSettings, IWidgetNewsService widgetNewsService)
        {
            this._settingService = settingService;
            this._NewsSettings = NewsSettings;
            this._widgetNewsService = widgetNewsService;
        }

        public IActionResult Configure()
        {
            var model = new ConfigurationModel()
            {
                WidgetZone = _NewsSettings.WidgetZone,
                QtdNewsPosts = _NewsSettings.QtdNewsPosts
            };
            return View("~/Plugins/Widgets.News/Views/Configure.cshtml", model);
        }

        [HttpPost]       
        public IActionResult Configure(ConfigurationModel model)
        {
            if (!ModelState.IsValid)
            {
                return Configure();
            }
            _NewsSettings.QtdNewsPosts = model.QtdNewsPosts;
            _NewsSettings.WidgetZone = model.WidgetZone;
            _settingService.SaveSetting(_NewsSettings);
            return Configure();
        }

        //public IActionResult PublicInfo(string widgetZone, object additionalData = null)
        //{
        //    return View("~/Plugins/Widgets.News/Views/PublicInfo.cshtml", _widgetNewsService.GetModel());
        //}
    }
}