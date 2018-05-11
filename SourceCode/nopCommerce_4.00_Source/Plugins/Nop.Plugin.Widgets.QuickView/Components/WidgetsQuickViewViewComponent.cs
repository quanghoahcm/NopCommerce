using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Core.Caching;
using Nop.Plugin.Widgets.QuickView;
using Nop.Plugin.Widgets.QuickView.Infrastructure.Cache;
using Nop.Plugin.Widgets.QuickView.Models;
using Nop.Services.Configuration;
using Nop.Services.Media;
using Nop.Web.Framework.Components;

namespace Nop.Plugin.Widgets.QuickView.Components
{
    [ViewComponent(Name = "WidgetsQuickView")]
    public class WidgetsQuickViewViewComponent : NopViewComponent
    {
        private readonly IStoreContext _storeContext;
        private readonly IStaticCacheManager _cacheManager;
        private readonly ISettingService _settingService;
        private readonly IPictureService _pictureService;

        public WidgetsQuickViewViewComponent(IStoreContext storeContext, 
            IStaticCacheManager cacheManager, 
            ISettingService settingService, 
            IPictureService pictureService)
        {
            this._storeContext = storeContext;
            this._cacheManager = cacheManager;
            this._settingService = settingService;
            this._pictureService = pictureService;
        }

        public IViewComponentResult Invoke(string widgetZone, object additionalData)
        {
            var quickViewSettings = _settingService.LoadSetting<QuickViewSettings>(_storeContext.CurrentStore.Id);

            var model = new PublicInfoModel
            {
                Picture1Url = GetPictureUrl(quickViewSettings.Picture1Id),
                Text1 = quickViewSettings.Text1,
                Link1 = quickViewSettings.Link1,

                Picture2Url = GetPictureUrl(quickViewSettings.Picture2Id),
                Text2 = quickViewSettings.Text2,
                Link2 = quickViewSettings.Link2,

                Picture3Url = GetPictureUrl(quickViewSettings.Picture3Id),
                Text3 = quickViewSettings.Text3,
                Link3 = quickViewSettings.Link3,

                Picture4Url = GetPictureUrl(quickViewSettings.Picture4Id),
                Text4 = quickViewSettings.Text4,
                Link4 = quickViewSettings.Link4,

                Picture5Url = GetPictureUrl(quickViewSettings.Picture5Id),
                Text5 = quickViewSettings.Text5,
                Link5 = quickViewSettings.Link5
            };

            if (string.IsNullOrEmpty(model.Picture1Url) && string.IsNullOrEmpty(model.Picture2Url) &&
                string.IsNullOrEmpty(model.Picture3Url) && string.IsNullOrEmpty(model.Picture4Url) &&
                string.IsNullOrEmpty(model.Picture5Url))
                //no pictures uploaded
                return Content("");

            return View("~/Plugins/Widgets.QuickView/Views/PublicInfo.cshtml", model);
        }

        protected string GetPictureUrl(int pictureId)
        {
            var cacheKey = string.Format(ModelCacheEventConsumer.PICTURE_URL_MODEL_KEY, pictureId);
            return _cacheManager.Get(cacheKey, () =>
            {
                //little hack here. nulls aren't cacheable so set it to ""
                var url = _pictureService.GetPictureUrl(pictureId, showDefaultPicture: false) ?? "";
                return url;
            });
        }
    }
}
