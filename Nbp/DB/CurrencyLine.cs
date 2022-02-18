using System;
using System.ComponentModel.DataAnnotations;

namespace Nbp.DB
{
    public class CurrencyLine
    {
        [Key]
        public string Code { get; set; }

        public string Currency { get; set; }
        
        public double Value { get; set; }

        public DateTime LastUpdate { get; set; }

    }
}