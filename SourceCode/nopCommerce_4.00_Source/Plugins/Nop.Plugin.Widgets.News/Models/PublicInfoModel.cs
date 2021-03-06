﻿using Nop.Web.Framework.Mvc;
using Nop.Web.Framework.Mvc.Models;
using System;
using System.Collections.Generic;

namespace Nop.Plugin.Widgets.News.Models
{
    public class PublicInfoModel : BaseNopModel, ICloneable
    {
        public PublicInfoModel()
        {
            NewsItems = new List<NewsItemModel>();
        }

        public IList<NewsItemModel> NewsItems { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}