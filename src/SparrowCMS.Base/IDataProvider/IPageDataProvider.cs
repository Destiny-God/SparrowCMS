﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SparrowCMS.Base.IDataProvider
{
    public interface IPageDataProvider
    {
        IEnumerable<Page> GetPages();
    }
}
