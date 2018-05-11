using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Core.Domain.Orders;
using Nop.Services.Configuration;
using Nop.Services.Logging;
using Nop.Services.Orders;
using Nop.Web.Framework.Components;

namespace Nop.Plugin.Widgets.GoogleAnalytics.Components
{
    [ViewComponent(Name = "WidgetsHomePageNewProducts")]
    public class WidgetsGoogleAnalyticsViewComponent : NopViewComponent
    {
        private readonly IWorkContext _workContext;
        private readonly IStoreContext _storeContext;
        private readonly ISettingService _settingService;
        private readonly IOrderService _orderService;
        private readonly ILogger _logger;

        public WidgetsGoogleAnalyticsViewComponent(
            IWorkContext workContext,
            IStoreContext storeContext,
            ISettingService settingService,
            IOrderService orderService,
            ILogger logger)
        {
            this._workContext = workContext;
            this._storeContext = storeContext;
            this._settingService = settingService;
            this._orderService = orderService;
            this._logger = logger;
        }

        public IViewComponentResult Invoke(string widgetZone, object additionalData)
        {
            return View("~/Plugins/Widgets.HomePageNewProducts/Views/PublicInfo.cshtml");
        }
        
    }
}