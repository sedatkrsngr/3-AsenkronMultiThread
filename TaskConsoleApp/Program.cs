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

            Console.WriteLine("WaitAll önce");
            Task.WaitAll(tastList.ToArray());//WaitAll ise WhenAll dan farkı işlem anında kullanıldığı threadı bloklama yapar ve başka işlem yapmasını engeller senkrona benzer
            bool result = Task.WaitAll(tastList.ToArray(),3000);// belirtilen milisaniye içerisinde veriyi getiriyorsa true getirmiyorsa false yapar
            Console.WriteLine("WaitAll Sonra");

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
