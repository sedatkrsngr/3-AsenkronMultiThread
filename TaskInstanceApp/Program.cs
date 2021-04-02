using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace TaskInstanceApp
{
    
    class Program
    {
        public static int cacheData { get; set; } = 150;
        async static Task Main(string[] args)
        {
            var task = new HttpClient().GetStringAsync("https://www.google.com");


            await task;

            //return task.Result; // bu şekilde kullanımda REsult threadı kitleyemez
            //ya da  şu şekilde 

            //var task = new HttpClient().GetStringAsync("https://www.google.com").ContinueWith((data)=> {

            //    data.Result// bu şekilde içerde kullandığımızda soprun olmaz
            
            //});



            Console.WriteLine(GetDate());

            GetCacheData();

        }
        public static string GetDate()
        {
            var Task = new HttpClient().GetStringAsync("https://www.google.com");

            return Task.Result;//Result propertysini kullanırken çok dikkatli olmalıyız çünkü ana threadı kitler
        }


        public static ValueTask<int> GetCacheData()//Hemen cevap döneceğini bildiğimiz işlemlerde ValueTask daha masrafsız çok ta önemli değil
        {

            return new ValueTask<int>(cacheData);//Result propertysini kullanırken çok dikkatli olmalıyız çünkü ana threadı kitler
        }
    }
}
