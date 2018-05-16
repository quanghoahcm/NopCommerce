using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Core.Caching;
using Nop.Services.Configuration;
using Nop.Services.Media;
using Nop.Web.Framework.Components;
using Nop.Plugin.Widgets.News.Service;

namespace Nop.Plugin.Widgets.News.Components
{
    [ViewComponent(Name = "WidgetsNews")]
    public class WidgetsNewsViewComponent : NopViewComponent
    {
        private readonly IStoreContext _storeContext;
        private readonly IStaticCacheManager _cacheManager;
        private readonly ISettingService _settingService;
        private readonly IPictureService _pictureService;
        private readonly IWidgetNewsService _widgetNewsService;
        public WidgetsNewsViewComponent(IStoreContext storeContext, 
            IStaticCacheManager cacheManager, 
            ISettingService settingService, 
            IPictureService pictureService,
             IWidgetNewsService widgetNewsService)
        {
            this._storeContext = storeContext;
            this._cacheManager = cacheManager;
            this._settingService = settingService;
            this._pictureService = pictureService;
            this._widgetNewsService = widgetNewsService;
        }

        public IViewComponentResult Invoke(string widgetZone, object additionalData)
        {
        
            return View("~/Plugins/Widgets.News/Views/PublicInfo.cshtml", _widgetNewsService.GetModel());
        }             
    }
}
