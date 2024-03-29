﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMC.Models
{
    public class PagedList<T>
    {
        public List<T> Content { get; set; }
        public Int32 CurrentPage { get; set; }
        public Int32 PageSize { get; set; }
        public int TotalRecords { get; set; }
    }
}