using System.Collections.Generic;
using Microsoft.AspNetCore.Routing;
using Nop.Core;
using Nop.Core.Plugins;
using Nop.Services.Cms;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Media;

namespace Nop.Plugin.Widgets.News
{
    /// <summary>
    /// PLugin
    /// </summary>
    public class NewsPlugin : BasePlugin, IWidgetPlugin
    {
        private readonly ISettingService _settingService;
        private readonly NewsSettings _NewsSettings;
        private readonly IWebHelper _webHelper;
        public NewsPlugin(IPictureService pictureService,
            ISettingService settingService, NewsSettings NewsSettings, IWebHelper webHelper)
        {
            this._settingService = settingService;
            this._NewsSettings = NewsSettings;
            this._webHelper = webHelper;
        }

        public IList<string> GetWidgetZones()
        {
            return !string.IsNullOrWhiteSpace(_NewsSettings.WidgetZone)
                ? new List<string> { _NewsSettings.WidgetZone }
                : new List<string> { "home_page_before_news" };
        }
       
    
        /// <summary>
        /// Gets a configuration page URL
        /// </summary>
        public override string GetConfigurationPageUrl()
        {
            return _webHelper.GetStoreLocation() + "Admin/WidgetsNews/Configure";
        }
        public void GetDisplayWidgetRoute(string widgetZone, out string actionName, out string controllerName, out RouteValueDictionary routeValues)
        {
            actionName = "PublicInfo";
            controllerName = "WidgetsNews";
            routeValues = new RouteValueDictionary
            {
                {"Namespaces", "Nop.Plugin.Widgets.News.Controllers"},
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
				viewComponentName = "WidgetsNews";
			}
        public override void Install()
        {
            var settings = new NewsSettings
            {
                QtdNewsPosts = 3,
                WidgetZone = "home_page_before_news"
            };
            _settingService.SaveSetting(settings);

            this.AddOrUpdatePluginLocaleResource("Plugins.Widgets.News.Fields.WidgetZone", "WidgetZone name");
            this.AddOrUpdatePluginLocaleResource("Plugins.Widgets.News.Fields.WidgetZone.Hint", "Enter the WidgetZone name that will display the HTML code.");
            this.AddOrUpdatePluginLocaleResource("Plugins.Widgets.News.Fields.QtdNewsPosts", "Number of News items");
            this.AddOrUpdatePluginLocaleResource("Plugins.Widgets.News.Fields.QtdNewsPosts.Hint", "Enter the number of News items that will be displayed in view.");

            base.Install();
        }

        public override void Uninstall()
        {
            //settings
            _settingService.DeleteSetting<NewsSettings>();

            //locales
            this.DeletePluginLocaleResource("Plugins.Widgets.News.Fields.WidgetZone");
            this.DeletePluginLocaleResource("Plugins.Widgets.News.Fields.WidgetZone.Hint");
            this.DeletePluginLocaleResource("Plugins.Widgets.News.Fields.QtdNewsPosts");
            this.DeletePluginLocaleResource("Plugins.Widgets.News.Fields.QtdNewsPosts.Hint");

            base.Uninstall();
        }
    }
}
