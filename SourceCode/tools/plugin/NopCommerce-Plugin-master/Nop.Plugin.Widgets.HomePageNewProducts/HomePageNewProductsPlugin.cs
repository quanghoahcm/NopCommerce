using Nop.Core;
using Nop.Core.Plugins;
using Nop.Services.Cms;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using System.Collections.Generic;

namespace Nop.Plugin.Widgets.HomePageNewProductsPlugin
{
    public class HomePageNewProductsPlugin : BasePlugin, IWidgetPlugin
    {

        private readonly IWebHelper _webHelper;
        private readonly ISettingService _settingService;
        private readonly HomePageNewProductsPluginSettings _homePageNewProductsPluginSettings;


        public HomePageNewProductsPlugin(IWebHelper webHelper, ISettingService settingService, HomePageNewProductsPluginSettings homePageNewProductsPluginSettings)
        {
            this._webHelper = webHelper;
            this._settingService = settingService;
            this._homePageNewProductsPluginSettings = homePageNewProductsPluginSettings;
        }


        /// <summary>
        /// Gets widget zones where this widget should be rendered
        /// </summary>
        /// <returns>Widget zones</returns>
        public IList<string> GetWidgetZones()
        {
            return !string.IsNullOrWhiteSpace(_homePageNewProductsPluginSettings.WidgetZone)
                ? new List<string>() { _homePageNewProductsPluginSettings.WidgetZone }
                : new List<string>() { "home_page_top" };
        }


        public override string GetConfigurationPageUrl()
        {
            return _webHelper.GetStoreLocation() + "Admin/WidgetsHomePageNewProductsPlugin/Configure";
        }

        /// <summary>
        /// Gets a view component for displaying plugin in public store
        /// </summary>
        /// <param name="widgetZone">Name of the widget zone</param>
        /// <param name="viewComponentName">View component name</param>

        public void GetPublicViewComponent(string widgetZone, out string viewComponentName)
        {
            viewComponentName = "WidgetsHomePageNewProductsPlugin";
        }

        public override void Install()
        {
            var settings = new HomePageNewProductsPluginSettings()
            {
                NumberOfProductsToShow = 3
            };
            _settingService.SaveSetting(settings);

            this.AddOrUpdatePluginLocaleResource("Plugins.Widgets.HomePageNewProductsPlugin.NumberOfProductsToShow", "NumberOfProducts");
            this.AddOrUpdatePluginLocaleResource("Plugins.Widgets.HomePageNewProductsPlugin.NumberOfProductsToShow.Hint", "Enter number of products");
            this.AddOrUpdatePluginLocaleResource("Plugins.Widgets.HomePageNewProductsPlugin.WidgetZone", "WidgetZone");
            this.AddOrUpdatePluginLocaleResource("Plugins.Widgets.HomePageNewProductsPlugin.WidgetZone.Hints", "Chose Widget zone");

            base.Install();
        }

        public override void Uninstall()
        {
            _settingService.DeleteSetting<HomePageNewProductsPluginSettings>();

            this.DeletePluginLocaleResource("Plugins.Widgets.HomePageNewProductsPlugin.NumberOfProductsToShow");
            this.DeletePluginLocaleResource("Plugins.Widgets.HomePageNewProductsPlugin.NumberOfProductsToShow.Hint");
            this.DeletePluginLocaleResource("Plugins.Widgets.HomePageNewProductsPlugin.WidgetZone");
            this.DeletePluginLocaleResource("Plugins.Widgets.HomePageNewProductsPlugin.WidgetZone.Hint");

            base.Uninstall();
        }


    }
}
