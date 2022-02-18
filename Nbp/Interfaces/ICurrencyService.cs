using Nbp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nbp.Interfaces
{
    public interface ICurrencyService
    {
        Task<List<CurrencyDto>> GetAllAsync();
        Task<CurrencyDto> GetCurrencyByCodeAsync(string code);
    }
}
