﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace SparrowCMS.Core.Models
{
    public class Page
    {
        private bool _initialized;

        public void Init(Context context)
        {
            if (_initialized)
            {
                return;
            }
            //find all label's template content
            Template.Init();
            RouteData = UrlRoute.GetRouteData(context.HttpContext);
            ViewData = new Document();
            _initialized = true;
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public Role Role { get; set; }

        public string Keywords { get; set; }

        public string Description { get; set; }

        public string UrlPattern { get; set; }

        public Template Template { get; set; }

        public OutputCache OutputCache { get; set; }

        public UrlRoute UrlRoute { get; set; }

        public RouteData RouteData { get; set; }

        public Document ViewData { get; set; }

        public string GetReplacedContext()
        {
            return Template.GetReplacedContent();
        }
    }
}
