using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

using CRMCore.Enums;

namespace CRMCore.Helpers
{
    public static class CurrencyConverter
    {
        static private DateTime LastUpdate;

        static Dictionary<CurrencyType, decimal> currencyValue = new Dictionary<CurrencyType, decimal>
       {
           {CurrencyType.Rub, 1 },
           {CurrencyType.Dollat, 0 },
           {CurrencyType.Euro, 0 },
       };

        private static void GetCurrentCurrency()
        {
            if ((DateTime.Now - LastUpdate).Hours > 1)
            {
                string url = "https://www.cbr-xml-daily.ru/daily_json.js";
                using (var webClient = new WebClient())
                {
                    string responce = webClient.DownloadString(url);
                    dynamic jSon = JObject.Parse(responce);
                    LastUpdate = DateTime.Now;
                    currencyValue[CurrencyType.Dollat] = Convert.ToDecimal(jSon.Valute.USD.Value);
                    currencyValue[CurrencyType.Euro] = Convert.ToDecimal(jSon.Valute.EUR.Value);
                }
            }
        }

        /// <summary>
        /// Метод конвертирования валют
        /// </summary>
        /// <param name="fromСurrency"></param>
        /// <param name="intoCurrency"></param>
        /// <returns></returns>
        public static decimal ConvertValute(CurrencyType fromСurrency, CurrencyType intoCurrency)
        {
            GetCurrentCurrency();
            decimal result = currencyValue[fromСurrency] / currencyValue[intoCurrency];

            return result;

        }
    }
}
