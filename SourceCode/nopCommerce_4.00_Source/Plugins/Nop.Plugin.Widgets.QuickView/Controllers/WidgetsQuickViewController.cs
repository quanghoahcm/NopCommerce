using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Core.Caching;
using Nop.Plugin.Widgets.QuickView.Models;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Media;
using Nop.Services.Security;
using Nop.Services.Stores;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;

namespace Nop.Plugin.Widgets.QuickView.Controllers
{
    [Area(AreaNames.Admin)]
    public class WidgetsQuickViewController : BasePluginController
    {
        private readonly IWorkContext _workContext;
        private readonly IStoreService _storeService;
        private readonly IPermissionService _permissionService;
        private readonly IPictureService _pictureService;
        private readonly ISettingService _settingService;
        private readonly ILocalizationService _localizationService;

        public WidgetsQuickViewController(IWorkContext workContext,
            IStoreService storeService,
            IPermissionService permissionService, 
            IPictureService pictureService,
            ISettingService settingService,
            ICacheManager cacheManager,
            ILocalizationService localizationService)
        {
            this._workContext = workContext;
            this._storeService = storeService;
            this._permissionService = permissionService;
            this._pictureService = pictureService;
            this._settingService = settingService;
            this._localizationService = localizationService;
        }

        public IActionResult Configure()
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageWidgets))
                return AccessDeniedView();

            //load settings for a chosen store scope
            var storeScope = this.GetActiveStoreScopeConfiguration(_storeService, _workContext);
            var quickViewSettings = _settingService.LoadSetting<QuickViewSettings>(storeScope);
            var model = new ConfigurationModel
            {
                Picture1Id = quickViewSettings.Picture1Id,
                Text1 = quickViewSettings.Text1,
                Link1 = quickViewSettings.Link1,
                Picture2Id = quickViewSettings.Picture2Id,
                Text2 = quickViewSettings.Text2,
                Link2 = quickViewSettings.Link2,
                Picture3Id = quickViewSettings.Picture3Id,
                Text3 = quickViewSettings.Text3,
                Link3 = quickViewSettings.Link3,
                Picture4Id = quickViewSettings.Picture4Id,
                Text4 = quickViewSettings.Text4,
                Link4 = quickViewSettings.Link4,
                Picture5Id = quickViewSettings.Picture5Id,
                Text5 = quickViewSettings.Text5,
                Link5 = quickViewSettings.Link5,
                ActiveStoreScopeConfiguration = storeScope
            };

            if (storeScope > 0)
            {
                model.Picture1Id_OverrideForStore = _settingService.SettingExists(quickViewSettings, x => x.Picture1Id, storeScope);
                model.Text1_OverrideForStore = _settingService.SettingExists(quickViewSettings, x => x.Text1, storeScope);
                model.Link1_OverrideForStore = _settingService.SettingExists(quickViewSettings, x => x.Link1, storeScope);
                model.Picture2Id_OverrideForStore = _settingService.SettingExists(quickViewSettings, x => x.Picture2Id, storeScope);
                model.Text2_OverrideForStore = _settingService.SettingExists(quickViewSettings, x => x.Text2, storeScope);
                model.Link2_OverrideForStore = _settingService.SettingExists(quickViewSettings, x => x.Link2, storeScope);
                model.Picture3Id_OverrideForStore = _settingService.SettingExists(quickViewSettings, x => x.Picture3Id, storeScope);
                model.Text3_OverrideForStore = _settingService.SettingExists(quickViewSettings, x => x.Text3, storeScope);
                model.Link3_OverrideForStore = _settingService.SettingExists(quickViewSettings, x => x.Link3, storeScope);
                model.Picture4Id_OverrideForStore = _settingService.SettingExists(quickViewSettings, x => x.Picture4Id, storeScope);
                model.Text4_OverrideForStore = _settingService.SettingExists(quickViewSettings, x => x.Text4, storeScope);
                model.Link4_OverrideForStore = _settingService.SettingExists(quickViewSettings, x => x.Link4, storeScope);
                model.Picture5Id_OverrideForStore = _settingService.SettingExists(quickViewSettings, x => x.Picture5Id, storeScope);
                model.Text5_OverrideForStore = _settingService.SettingExists(quickViewSettings, x => x.Text5, storeScope);
                model.Link5_OverrideForStore = _settingService.SettingExists(quickViewSettings, x => x.Link5, storeScope);
            }

            return View("~/Plugins/Widgets.QuickView/Views/Configure.cshtml", model);
        }

        [HttpPost]
        public IActionResult Configure(ConfigurationModel model)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageWidgets))
                return AccessDeniedView();

            //load settings for a chosen store scope
            var storeScope = this.GetActiveStoreScopeConfiguration(_storeService, _workContext);
            var quickViewSettings = _settingService.LoadSetting<QuickViewSettings>(storeScope);

            //get previous picture identifiers
            var previousPictureIds = new[] 
            {
                quickViewSettings.Picture1Id,
                quickViewSettings.Picture2Id,
                quickViewSettings.Picture3Id,
                quickViewSettings.Picture4Id,
                quickViewSettings.Picture5Id
            };

            quickViewSettings.Picture1Id = model.Picture1Id;
            quickViewSettings.Text1 = model.Text1;
            quickViewSettings.Link1 = model.Link1;
            quickViewSettings.Picture2Id = model.Picture2Id;
            quickViewSettings.Text2 = model.Text2;
            quickViewSettings.Link2 = model.Link2;
            quickViewSettings.Picture3Id = model.Picture3Id;
            quickViewSettings.Text3 = model.Text3;
            quickViewSettings.Link3 = model.Link3;
            quickViewSettings.Picture4Id = model.Picture4Id;
            quickViewSettings.Text4 = model.Text4;
            quickViewSettings.Link4 = model.Link4;
            quickViewSettings.Picture5Id = model.Picture5Id;
            quickViewSettings.Text5 = model.Text5;
            quickViewSettings.Link5 = model.Link5;

            /* We do not clear cache after each setting update.
             * This behavior can increase performance because cached settings will not be cleared 
             * and loaded from database after each update */
            _settingService.SaveSettingOverridablePerStore(quickViewSettings, x => x.Picture1Id, model.Picture1Id_OverrideForStore, storeScope, false);
            _settingService.SaveSettingOverridablePerStore(quickViewSettings, x => x.Text1, model.Text1_OverrideForStore, storeScope, false);
            _settingService.SaveSettingOverridablePerStore(quickViewSettings, x => x.Link1, model.Link1_OverrideForStore, storeScope, false);
            _settingService.SaveSettingOverridablePerStore(quickViewSettings, x => x.Picture2Id, model.Picture2Id_OverrideForStore, storeScope, false);
            _settingService.SaveSettingOverridablePerStore(quickViewSettings, x => x.Text2, model.Text2_OverrideForStore, storeScope, false);
            _settingService.SaveSettingOverridablePerStore(quickViewSettings, x => x.Link2, model.Link2_OverrideForStore, storeScope, false);
            _settingService.SaveSettingOverridablePerStore(quickViewSettings, x => x.Picture3Id, model.Picture3Id_OverrideForStore, storeScope, false);
            _settingService.SaveSettingOverridablePerStore(quickViewSettings, x => x.Text3, model.Text3_OverrideForStore, storeScope, false);
            _settingService.SaveSettingOverridablePerStore(quickViewSettings, x => x.Link3, model.Link3_OverrideForStore, storeScope, false);
            _settingService.SaveSettingOverridablePerStore(quickViewSettings, x => x.Picture4Id, model.Picture4Id_OverrideForStore, storeScope, false);
            _settingService.SaveSettingOverridablePerStore(quickViewSettings, x => x.Text4, model.Text4_OverrideForStore, storeScope, false);
            _settingService.SaveSettingOverridablePerStore(quickViewSettings, x => x.Link4, model.Link4_OverrideForStore, storeScope, false);
            _settingService.SaveSettingOverridablePerStore(quickViewSettings, x => x.Picture5Id, model.Picture5Id_OverrideForStore, storeScope, false);
            _settingService.SaveSettingOverridablePerStore(quickViewSettings, x => x.Text5, model.Text5_OverrideForStore, storeScope, false);
            _settingService.SaveSettingOverridablePerStore(quickViewSettings, x => x.Link5, model.Link5_OverrideForStore, storeScope, false);
            
            //now clear settings cache
            _settingService.ClearCache();
            
            //get current picture identifiers
            var currentPictureIds = new[]
            {
                quickViewSettings.Picture1Id,
                quickViewSettings.Picture2Id,
                quickViewSettings.Picture3Id,
                quickViewSettings.Picture4Id,
                quickViewSettings.Picture5Id
            };

            //delete an old picture (if deleted or updated)
            foreach (var pictureId in previousPictureIds.Except(currentPictureIds))
            { 
                var previousPicture = _pictureService.GetPictureById(pictureId);
                if (previousPicture != null)
                    _pictureService.DeletePicture(previousPicture);
            }

            SuccessNotification(_localizationService.GetResource("Admin.Plugins.Saved"));
            return Configure();
        }
    }
}