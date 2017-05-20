using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.Net.Http;

namespace WebCrawler
{
    class Program
    {
        static void Main(string[] args)
        {
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load("http://www.automobile.tn/neuf/bmw.3/");

            var divs = doc.DocumentNode.Descendants("div")
                .Where(node => node.GetAttributeValue("class", "")
                .Equals("article_new_car article_last_modele")).ToList();

            foreach (var div in divs)
            {
                var car = new car
                {
                    Model = div?.Descendants("h2").FirstOrDefault().InnerText,
                    Price = div?.Descendants("div").FirstOrDefault().InnerText
                };

                //var model = div?.Descendants("h2").FirstOrDefault().InnerText;
                //var price = div?.Descendants("div").FirstOrDefault().InnerText;
            }

        }
    }

    public class car
    {
        public string Model { get; set; }
        public string Price { get; set; }

    }
}
public class scrap
{
  
    private static async Task startCrowlerAsync()
    {
        var url = "http://www.automobile.tn/neuf/bmw.3/";

        var httpClient = new  HttpClient();
        var html = await httpClient.GetStringAsync(url);

        var htmlDocument = new HtmlDocument();
        htmlDocument.LoadHtml(html);

        var divs = htmlDocument.DocumentNode.Descendants("div")
            .Where(node => node.GetAttributeValue("class", "")
            .Equals("article_new_car article_last_modele")).ToList();

        var cars = new List<car>();
        //parse
        foreach (var div in divs)
        {
            var car = new car
            {
                Model = div?.Descendants("h2").FirstOrDefault().InnerText,
                Price = div?.Descendants("div").FirstOrDefault().InnerText
            };
            cars.Add(car);
            Console.WriteLine(car.Model);
        }
    }
}

