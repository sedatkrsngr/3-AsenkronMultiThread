using System;
using System.IO;
using System.Threading.Tasks;

namespace FromResultApp
{
    internal  class Program//Dosya.txt ekledim ve properties copyAlvays dedim
    {
        public static string CacheData { get; set; }
        private async static Task Main(string[] args)
        {

            CacheData = await GetDateAsync();

            Console.WriteLine(CacheData);
        }


        public static Task<string> GetDateAsync()
        {


            if (string.IsNullOrEmpty(CacheData))
            {
                return File.ReadAllTextAsync("Dosya.txt");
            }
            else
            {
                return Task.FromResult(CacheData);//Parametre olarak obje olarak alır ve bunu geriye Task olarak döner. Bir metoddan daha önce çalışmış olan static bir nesneyi kullanmak istiyorsak bunu kullanırız Genelde Cachelerde kullanılır
            }
        }

    }
}
