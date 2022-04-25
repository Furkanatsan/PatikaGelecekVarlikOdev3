﻿using Northwind.Entity.Base;
using System;
using System.Collections.Generic;

#nullable disable

namespace Northwind.Entity.Models
{
    public partial class OrderSubtotal:EntityBase
    {
        public int OrderId { get; set; }
        public decimal? Subtotal { get; set; }
    }
}
