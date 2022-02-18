using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nbp.DB;
using Nbp.Interfaces;
using Nbp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Nbp.Controllers
{
    public class CurrenciesController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ICurrencyService _currencyService;

        public CurrenciesController(AppDbContext context, ICurrencyService currencyService)
        {
            _context = context;
            _currencyService = currencyService;
        }

        [HttpGet]
        public async Task<List<CurrencyDto>> All()
        {
            var currencies = await _currencyService.GetAllAsync();

            return currencies;
        }

        [HttpGet]
        [Route("[controller]/[action]/{code?}")]
        public async Task<CurrencyDto> Code([FromRoute] string code)
        {
            var currency = await _currencyService.GetCurrencyByCodeAsync(code);

            return currency;
        }
    }
}
