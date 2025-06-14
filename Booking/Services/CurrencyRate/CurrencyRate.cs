﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Services.CurrencyRate
{
    public class CurrencyRate
    {
        public String FullName { get; set; } = null!;
        public String ShortName { get; set; } = null!;
        public double RateBuy { get; set; }
        public double RateSell { get; set; }
        public DateOnly Date { get; set; }
    }
}
