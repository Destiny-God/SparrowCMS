﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SparrowCMS.Labels.Shared;
using SparrowCMS.Common;
using SparrowCMS.Attributes;
using SparrowCMS.Managers;

namespace SparrowCMS.Labels.Page
{
    public class List : ListLabelBase
    {
        public int PageSize { get; set; }

        public string SiteId { get; set; }

        public int Page { get; set; }

        private static readonly SiteManager SiteManager = new SiteManager();
        private static readonly PageManager PageManager = new PageManager();

        public override IEnumerable<Document> GetDatas()
        {
            var site = SiteManager.GetSite(SiteId);
            var query = PageManager.GetPages(site).AsEnumerable();

            if (!string.IsNullOrEmpty(PaginationId))
            {
                RecordCount = query.Count();
            }

            if (PageSize > 0)
            {
                if (Page == 0) Page = 1;
                query = query.Skip((Page - 1) * PageSize).Take(PageSize);
            }

            return query.Select(e => e.ToDocument());

        }
    }
}
