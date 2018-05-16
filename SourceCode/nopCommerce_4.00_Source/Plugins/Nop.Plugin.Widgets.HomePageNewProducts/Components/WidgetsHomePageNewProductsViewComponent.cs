using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Orders;
using Nop.Services.Catalog;
using Nop.Services.Configuration;
using Nop.Services.Logging;
using Nop.Services.Orders;
using Nop.Web.Factories;
using Nop.Web.Framework.Components;
using Nop.Web.Models.Catalog;

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
        private readonly IProductService _productService;
        private readonly IProductModelFactory _productModelFactory;
        public WidgetsGoogleAnalyticsViewComponent(
            IWorkContext workContext,
            IStoreContext storeContext,
            ISettingService settingService,
            IOrderService orderService,
            IProductService productService,
            ILogger logger,
            IProductModelFactory productModelFactory
            )
        {
            this._productModelFactory = productModelFactory;
            this._productService = productService;
            this._workContext = workContext;
            this._storeContext = storeContext;
            this._settingService = settingService;
            this._orderService = orderService;
            this._logger = logger;
        }

        public IViewComponentResult Invoke(string widgetZone, int productThumbPictureSize)
        {

            var numberOfProductsToShow = this._settingService.GetSettingByKey<int>("HomePageNewProductsSettings.NumberOfProductsToShow");

            var products = _productService.SearchProducts(orderBy: ProductSortingEnum.CreatedOn).Take(numberOfProductsToShow);

            var model = new List<ProductOverviewModel>();
            model.AddRange(_productModelFactory.PrepareProductOverviewModels(products, true, true, productThumbPictureSize));

            return View("~/Plugins/Widgets.HomePageNewProducts/Views/PublicInfo.cshtml", model);
        }
        
    }
}