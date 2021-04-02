using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace TaskConsoleApp
{

    public class Content 
    {
        public string Site { get; set; }
        public int Len { get; set; }
    }
    internal class Program
    {
        private async static Task Main(string[] args)
        {
            Console.WriteLine("Main threadı :" + Thread.CurrentThread.ManagedThreadId);// Çalışan threadı görürüz

            List<string> urlList = new List<string>()
            {
                "https://www.google.com",
                "https://www.microsoft.com",
                "https://www.amazon.com",
                "https://www.n11.com",
                "https://www.haberturk.com"
            };

            List<Task<Content>> tastList = new List<Task<Content>>();

            urlList.ToList().ForEach(x =>
            {
                tastList.Add(GetContentAsync(x));
            });

            var firstData = await Task.WhenAny(tastList);//When.Any ise ilk gerçekleşeni dön demek. Bu örnekte listeden bir tane Task<Content> döner


            Console.WriteLine($"{firstData.Result.Site} - {firstData.Result.Len}");// İlk datanın sonucunu görücez
        }

        public async static Task<Content> GetContentAsync(string url)
        {
            Content content = new Content();
            var data = await new HttpClient().GetStringAsync(url);

            content.Site = url;
            content.Len = data.Length;

            Console.WriteLine("GetContentAsync threadı :" + Thread.CurrentThread.ManagedThreadId);// Çalışan threadı görürüz

            return content;

        }
    }
}
