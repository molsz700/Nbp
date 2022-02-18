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

namespace Nbp.Services
{
    public class CurrencyService : ICurrencyService
    {
        private readonly AppDbContext _context;

        public CurrencyService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<CurrencyDto>> GetAllAsync()
        {
            var result = await GetResponseFromNbpAsync("http://api.nbp.pl/api/exchangerates/tables/A/");
            var items = JsonConvert.DeserializeObject<List<FullResponse>>(result).FirstOrDefault();
            
            foreach(var item in items.Rates)
            {
                await AddOrUpdateDatabaseAsync(item);
            }            

            return items.Rates;
        }

        public async Task<CurrencyDto> GetCurrencyByCodeAsync(string code)
        {
            var result = await GetResponseFromNbpAsync($"http://api.nbp.pl/api/exchangerates/rates/A/{code}/");
            var item = JsonConvert.DeserializeObject<SingleCurrencyResponse>(result);

            var currencyDto = SingleCurrency2CurrencyDto(item);

            await AddOrUpdateDatabaseAsync(currencyDto);

            return currencyDto;
        }

        private async Task<string> GetResponseFromNbpAsync(string url)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");

            return await client.GetStringAsync(url);
        }

        private async Task AddOrUpdateDatabaseAsync(CurrencyDto item)
        {
            var currency = await _context.Currencies.FirstOrDefaultAsync(x => x.Code == item.Code);
            if (currency != null)
            {
                currency.Value = item.Mid;
                currency.LastUpdate = DateTime.Now;
            }
            else
            {
                await _context.Currencies.AddAsync(new CurrencyLine()
                {
                    Code = item.Code,
                    Currency = item.Currency,
                    Value = item.Mid,
                    LastUpdate = DateTime.Now
                });
            }

            await _context.SaveChangesAsync();
        }

        private CurrencyDto SingleCurrency2CurrencyDto(SingleCurrencyResponse rates)
        {
            return new CurrencyDto()
            {
                Currency = rates.Currency,
                Code = rates.Code,
                Mid = rates.Rates.FirstOrDefault().Mid
            };
        }
    }
}
