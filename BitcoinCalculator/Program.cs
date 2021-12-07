using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;

namespace BitcoinCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            BitcoinRate currentbitcoin = getrates();
            //Console.WriteLine($"Current Rate; {currentbitcoin.bpi.EUR.code} {currentbitcoin.bpi.EUR.rate_float}");
            Console.WriteLine("Sisetage enda bitcoini kogus:");
            float userinput = float.Parse(Console.ReadLine());
            Console.WriteLine("Mis valuutas soovite näha EUR/USD/GBP");
            string valuuta = Console.ReadLine();
            float currentrate = 0;
            if (valuuta == "EUR")
            {
                currentrate = currentbitcoin.bpi.EUR.rate_float;
            }
            else if (valuuta == "USD")
            {
                currentrate = currentbitcoin.bpi.USD.rate_float;
            }
            else if (valuuta == "GBP")
            {
                currentrate = currentbitcoin.bpi.GBP.rate_float;
            }
            else
            {
                Console.WriteLine("Minul ei ole sellist valuutat");
            }

            float result = currentrate * userinput;
            Console.WriteLine($"Your bitcoins are worth {result} {valuuta}");
        }
        public static BitcoinRate getrates()
        {
            string url = "https://api.coindesk.com/v1/bpi/currentprice.json";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";

            var webresponse = request.GetResponse();
            var webStream = webresponse.GetResponseStream();

            BitcoinRate bitcoindata;

            using(var responsereader = new StreamReader(webStream))
            {
                var response = responsereader.ReadToEnd();
                bitcoindata = JsonConvert.DeserializeObject<BitcoinRate>(response);
            }

            return bitcoindata;        
        }

    }
}
