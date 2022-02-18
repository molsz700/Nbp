using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nbp.Models
{
    public class SingleCurrencyResponse
    {
        public string Table { get; set; }
        public string Currency { get; set; }
        public string Code { get; set; }
        public List<Rates> Rates { get; set; }
    }
}
