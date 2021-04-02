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

    public class Status
    {
        public DateTime date { get; set; }
        public int threadId { get; set; }
    }

    internal class Program
    {
        private async static Task Main(string[] args)
        {
            var myTask = Task.Factory.StartNew((nesne) => //StartNew de run metodu gibi ayrı thread kullanılmasını sağlar farkı ise task çalışırken içerisine objemizi dönebiliyoruz ve Task içirisinde çalıştırabiliyoruz.
            {
                Console.WriteLine("My Task Çalıştı");
                var status = nesne as Status;
                status.threadId = Thread.CurrentThread.ManagedThreadId;//datetime yakalanmış nesneye burda Taskın içinde kullanıılan ıd atıyoruz

            }, new Status()//Taska girecek nesnenin bir değerini atıyoruz
            {
                date = DateTime.Now
            });

            await myTask;

            Status s = myTask.AsyncState as Status;// Bu metotla içerisindeki nesneyi yakalarız

            Console.WriteLine(s.date);
            Console.WriteLine(s.threadId);
        }

        
    }
}
