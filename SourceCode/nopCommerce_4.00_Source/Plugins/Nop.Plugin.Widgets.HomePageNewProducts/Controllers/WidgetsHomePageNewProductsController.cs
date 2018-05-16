using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Core;
using Nop.Plugin.Widgets.HomePageNewProducts;
using Nop.Plugin.Widgets.HomePageNewProducts.Models;
using Nop.Services.Catalog;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Security;
using Nop.Services.Stores;
using Nop.Web.Factories;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;

namespace Nop.Plugin.Widgets.HomePageNewProducts.Controllers
{
    [Area(AreaNames.Admin)]
    public class WidgetsHomePageNewProductsController : BasePluginController
    {
        private readonly IWorkContext _workContext;
        private readonly IStoreService _storeService;
        private readonly ISettingService _settingService;
        private readonly ILocalizationService _localizationService;
        private readonly IPermissionService _permissionService;
        private readonly IProductService _productService;
        private readonly IProductModelFactory _productModelFactory;

        public WidgetsHomePageNewProductsController(IWorkContext workContext,
            IStoreContext storeContext,
            IStoreService storeService,
            ISettingService settingService,
            ILocalizationService localizationService,
            IPermissionService permissionService,
            IProductService productService,
            IProductModelFactory productModelFactory)
        {
            this._workContext = workContext;
            this._storeService = storeService;
            this._settingService = settingService;
            this._localizationService = localizationService;
            this._permissionService = permissionService;
            this._productService = productService;
            this._productModelFactory = productModelFactory;
        }
        public IActionResult Configure()
        {
            //if (!_permissionService.Authorize(StandardPermissionProvider.ManageWidgets))
            //    return AccessDeniedView();

            //load settings for a chosen store scope
            var storeScope = this.GetActiveStoreScopeConfiguration(_storeService, _workContext);
            var homePagePluginSettings = _settingService.LoadSetting<HomePageNewProductsSettings>(storeScope);
            var model = new ConfigurationModel
            {
                ZoneId = homePagePluginSettings.WidgetZone,
                NumberOfProducts = homePagePluginSettings.NumberOfProductsToShow,
                ActiveStoreScopeConfiguration = storeScope
            };

            model.AvailableZones.Add(new SelectListItem() { Text = "Home Page Top", Value = "home_page_top" });
            model.AvailableZones.Add(new SelectListItem() { Text = "Home Page Before Categories", Value = "home_page_before_categories" });
            model.AvailableZones.Add(new SelectListItem() { Text = "Home Page Before Products", Value = "home_page_before_products" });
            model.AvailableZones.Add(new SelectListItem() { Text = "Home Page Before Best Sellers", Value = "home_page_before_best_sellers" });
            model.AvailableZones.Add(new SelectListItem() { Text = "Home Page Before News", Value = "home_page_before_news" });
            model.AvailableZones.Add(new SelectListItem() { Text = "Home Page Bottom", Value = "home_page_bottom" });
            model.AvailableZones.Add(new SelectListItem() { Text = "Home Page Before Poll", Value = "home_page_bottom" });
            
            model.ActiveStoreScopeConfiguration = storeScope;
            if (storeScope > 0)
            {
                model.NumberOfProducts_OverrideForStore = _settingService.SettingExists(homePagePluginSettings, x => x.NumberOfProductsToShow, storeScope);
                model.ZoneId_OverrideForStore = _settingService.SettingExists(homePagePluginSettings, x => x.WidgetZone, storeScope);
            }

            return View("~/Plugins/Widgets.HomePageNewProducts/Views/Configure.cshtml", model);
        }



        [HttpPost]
        public IActionResult Configure(ConfigurationModel model)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageWidgets))
                return AccessDeniedView();
            if (!ModelState.IsValid)
            {
                return Configure();
            }
            //load settings for a chosen store scope
            var storeScope = this.GetActiveStoreScopeConfiguration(_storeService, _workContext);
            var homePagePluginSettings = _settingService.LoadSetting<HomePageNewProductsSettings>(storeScope);
            homePagePluginSettings.WidgetZone = model.ZoneId;
            homePagePluginSettings.NumberOfProductsToShow = model.NumberOfProducts;
            /* We do not clear cache after each setting update.
             * This behavior can increase performance because cached settings will not be cleared 
             * and loaded from database after each update */
            _settingService.SaveSettingOverridablePerStore(homePagePluginSettings, x => x.WidgetZone, model.ZoneId_OverrideForStore, storeScope, false);
            _settingService.SaveSettingOverridablePerStore(homePagePluginSettings, x => x.NumberOfProductsToShow, model.NumberOfProducts_OverrideForStore, storeScope, false);
            //now clear settings cache
            _settingService.ClearCache();

            SuccessNotification(_localizationService.GetResource("Admin.Plugins.Saved"));

            return Configure();
        }


    }
}
