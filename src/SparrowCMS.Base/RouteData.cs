﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SparrowCMS.Core
{
    public class RouteData
    {
        private readonly Dictionary<string, string> _data = new Dictionary<string, string>();

        public void Add(string name, string value)
        {
            if (_data.ContainsKey(name))
            {
                _data[name] = value;
            }
            else
            {
                _data.Add(name, value);
            }
        }

        public string this[string name]
        {
            get
            {
                return _data.ContainsKey(name) ? _data[name] : string.Empty;
            }
        }
    }
}
