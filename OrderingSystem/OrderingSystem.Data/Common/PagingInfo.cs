﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingSystem.Data.Common
{
    public class PagingInfo
    {
        public int TotalItems { get; set; }

        public int CurrentPage { get; set; }

        public int ItemsPerPage { get; set; }

        public int TotalPages
        {
            get => (int)Math.Ceiling((decimal)(TotalItems / ItemsPerPage));
        }
    }
}
