using Microsoft.AspNetCore.Routing;
using Nop.Core;
using Nop.Core.Plugins;
using Nop.Services.Cms;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using System.Collections.Generic;

namespace Nop.Plugin.Widgets.HomePageNewProducts
{
    public class HomePageNewProductsPlugin : BasePlugin, IWidgetPlugin
    {
        private readonly ISettingService _settingService;
        private readonly HomePageNewProductsSettings _homePageNewProductsSettings;
        private readonly IWebHelper _webHelper;  

        public HomePageNewProductsPlugin(IWebHelper webHelper, ISettingService settingService, HomePageNewProductsSettings homePageNewProductsSettings)
        {
            this._settingService = settingService;
            this._homePageNewProductsSettings = homePageNewProductsSettings;
            this._webHelper = webHelper;
        }


        /// <summary>
        /// Gets widget zones where this widget should be rendered
        /// </summary>
        /// <returns>Widget zones</returns>
        public IList<string> GetWidgetZones()
        {
            return !string.IsNullOrWhiteSpace(_homePageNewProductsSettings.WidgetZone)
                ? new List<string>() { _homePageNewProductsSettings.WidgetZone }
                : new List<string>() { "home_page_bottom" };
         }


        /// <summary>
        /// Gets a configuration page URL
        /// </summary>
        public override string GetConfigurationPageUrl()
        {
            return _webHelper.GetStoreLocation() + "Admin/WidgetsHomePageNewProducts/Configure";
        }
        public void GetDisplayWidgetRoute(string widgetZone, out string actionName, out string controllerName, out RouteValueDictionary routeValues)
        {
            actionName = "PublicInfo";
            controllerName = "WidgetsHomePageNewProducts";
            routeValues = new RouteValueDictionary
            {
                {"Namespaces", "Nop.Plugin.Widgets.HomePageNewProducts.Controllers"},
                {"area", null},
                {"widgetZone", widgetZone}
            };
        }
        /// <summary>
        /// Gets a view component for displaying plugin in public store
        /// </summary>
        /// <param name="widgetZone">Name of the widget zone</param>
        /// <param name="viewComponentName">View component name</param>

        public void GetPublicViewComponent(string widgetZone, out string viewComponentName)
        {
            viewComponentName = "WidgetsHomePageNewProducts";
        }

        public override void Install()
        {
            var settings = new HomePageNewProductsSettings()
            {
                NumberOfProductsToShow = 3,
                WidgetZone = "home_page_bottom"
            };
            _settingService.SaveSetting(settings);

            this.AddOrUpdatePluginLocaleResource("Plugins.Widgets.HomePageNewProducts.NumberOfProductsToShow", "NumberOfProducts");
            this.AddOrUpdatePluginLocaleResource("Plugins.Widgets.HomePageNewProducts.NumberOfProductsToShow.Hint", "Enter number of products");
            this.AddOrUpdatePluginLocaleResource("Plugins.Widgets.HomePageNewProducts.WidgetZone", "WidgetZone");
            this.AddOrUpdatePluginLocaleResource("Plugins.Widgets.HomePageNewProducts.WidgetZone.Hints", "Chose Widget zone");

            base.Install();
        }

        public override void Uninstall()
        {
            _settingService.DeleteSetting<HomePageNewProductsSettings>();

            this.DeletePluginLocaleResource("Plugins.Widgets.HomePageNewProducts.NumberOfProductsToShow");
            this.DeletePluginLocaleResource("Plugins.Widgets.HomePageNewProducts.NumberOfProductsToShow.Hint");
            this.DeletePluginLocaleResource("Plugins.Widgets.HomePageNewProducts.WidgetZone");
            this.DeletePluginLocaleResource("Plugins.Widgets.HomePageNewProducts.WidgetZone.Hint");

            base.Uninstall();
        }


    }
}
