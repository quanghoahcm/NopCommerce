using System.Collections.Generic;
using System.IO;
using Nop.Core;
using Nop.Core.Plugins;
using Nop.Services.Cms;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Media;

namespace Nop.Plugin.Widgets.QuickView
{
    /// <summary>
    /// PLugin
    /// </summary>
    public class QuickViewPlugin : BasePlugin, IWidgetPlugin
    {
        private readonly IPictureService _pictureService;
        private readonly ISettingService _settingService;
        private readonly IWebHelper _webHelper;

        public QuickViewPlugin(IPictureService pictureService,
            ISettingService settingService, IWebHelper webHelper)
        {
            this._pictureService = pictureService;
            this._settingService = settingService;
            this._webHelper = webHelper;
        }

        /// <summary>
        /// Gets widget zones where this widget should be rendered
        /// </summary>
        /// <returns>Widget zones</returns>
        public IList<string> GetWidgetZones()
        {
            return new List<string> { "home_page_top" };
        }

        /// <summary>
        /// Gets a configuration page URL
        /// </summary>
        public override string GetConfigurationPageUrl()
        {
            return _webHelper.GetStoreLocation() + "Admin/WidgetsQuickView/Configure";
        }

        /// <summary>
        /// Gets a view component for displaying plugin in public store
        /// </summary>
        /// <param name="widgetZone">Name of the widget zone</param>
        /// <param name="viewComponentName">View component name</param>
        public void GetPublicViewComponent(string widgetZone, out string viewComponentName)
        {
            viewComponentName = "WidgetsQuickView";
        }

        /// <summary>
        /// Install plugin
        /// </summary>
        public override void Install()
        {
            //pictures
            var sampleImagesPath = CommonHelper.MapPath("~/Plugins/Widgets.QuickView/Content/quickview/sample-images/");
            
            //settings
            var settings = new QuickViewSettings
            {
                Picture1Id = _pictureService.InsertPicture(File.ReadAllBytes(sampleImagesPath + "banner1.jpg"), MimeTypes.ImagePJpeg, "banner_1").Id,
                Text1 = "",
                Link1 = _webHelper.GetStoreLocation(false),
                Picture2Id = _pictureService.InsertPicture(File.ReadAllBytes(sampleImagesPath + "banner2.jpg"), MimeTypes.ImagePJpeg, "banner_2").Id,
                Text2 = "",
                Link2 = _webHelper.GetStoreLocation(false),
                //Picture3Id = _pictureService.InsertPicture(File.ReadAllBytes(sampleImagesPath + "banner3.jpg"), MimeTypes.ImagePJpeg, "banner_3").Id,
                //Text3 = "",
                //Link3 = _webHelper.GetStoreLocation(false),
            };
            _settingService.SaveSetting(settings);


            this.AddOrUpdatePluginLocaleResource("Plugins.Widgets.QuickView.Picture1", "Picture 1");
            this.AddOrUpdatePluginLocaleResource("Plugins.Widgets.QuickView.Picture2", "Picture 2");
            this.AddOrUpdatePluginLocaleResource("Plugins.Widgets.QuickView.Picture3", "Picture 3");
            this.AddOrUpdatePluginLocaleResource("Plugins.Widgets.QuickView.Picture4", "Picture 4");
            this.AddOrUpdatePluginLocaleResource("Plugins.Widgets.QuickView.Picture5", "Picture 5");
            this.AddOrUpdatePluginLocaleResource("Plugins.Widgets.QuickView.Picture", "Picture");
            this.AddOrUpdatePluginLocaleResource("Plugins.Widgets.QuickView.Picture.Hint", "Upload picture.");
            this.AddOrUpdatePluginLocaleResource("Plugins.Widgets.QuickView.Text", "Comment");
            this.AddOrUpdatePluginLocaleResource("Plugins.Widgets.QuickView.Text.Hint", "Enter comment for picture. Leave empty if you don't want to display any text.");
            this.AddOrUpdatePluginLocaleResource("Plugins.Widgets.QuickView.Link", "URL");
            this.AddOrUpdatePluginLocaleResource("Plugins.Widgets.QuickView.Link.Hint", "Enter URL. Leave empty if you don't want this picture to be clickable.");

            base.Install();
        }

        /// <summary>
        /// Uninstall plugin
        /// </summary>
        public override void Uninstall()
        {
            //settings
            _settingService.DeleteSetting<QuickViewSettings>();

            //locales
            this.DeletePluginLocaleResource("Plugins.Widgets.QuickView.Picture1");
            this.DeletePluginLocaleResource("Plugins.Widgets.QuickView.Picture2");
            this.DeletePluginLocaleResource("Plugins.Widgets.QuickView.Picture3");
            this.DeletePluginLocaleResource("Plugins.Widgets.QuickView.Picture4");
            this.DeletePluginLocaleResource("Plugins.Widgets.QuickView.Picture5");
            this.DeletePluginLocaleResource("Plugins.Widgets.QuickView.Picture");
            this.DeletePluginLocaleResource("Plugins.Widgets.QuickView.Picture.Hint");
            this.DeletePluginLocaleResource("Plugins.Widgets.QuickView.Text");
            this.DeletePluginLocaleResource("Plugins.Widgets.QuickView.Text.Hint");
            this.DeletePluginLocaleResource("Plugins.Widgets.QuickView.Link");
            this.DeletePluginLocaleResource("Plugins.Widgets.QuickView.Link.Hint");

            base.Uninstall();
        }
    }
}