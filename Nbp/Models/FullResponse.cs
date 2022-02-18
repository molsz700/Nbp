using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nbp.Models
{
    public class FullResponse
    {
        public string Table { get; set; }
        public string No { get; set; }
        public string EffectiveDate { get; set; }
        public List<CurrencyDto> Rates { get; set; }

    }
}
