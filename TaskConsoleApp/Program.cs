using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace TaskConsoleApp
{
    class Program
    {
       private async static  Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var myTask = new HttpClient().GetStringAsync("https://www.google.com").ContinueWith(calis);//static sınıftan statşc sınıf çağırılabilir

            Console.WriteLine("arada yapılacak işler");

            await myTask;

        }

        public  static void calis(Task<string> data)
        {
            Console.WriteLine(data.Result.Length);//Result Task<string> sonuclarını almak için kullanılır.
        }
    }
}
