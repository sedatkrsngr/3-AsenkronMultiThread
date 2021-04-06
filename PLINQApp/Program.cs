using System;
using System.Collections;
using System.Linq;
using System.Threading;

namespace PLINQApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //Genel kültür IEnumerable -  IQueryable farkı IQueryable veritabanına şartı direkt sorgu olarak atarken IEnumerable önce tüm veriyi çeker sonra şartlara göre işlem yapar

            var array = Enumerable.Range(1, 100).ToList();

           var newarray = array.AsParallel().Where(x => x % 2 == 0);//Asparallel kullanılarak daha fazla threadın üzerinde çalışmasını sağlarız. 

            newarray.ToList().ForEach(x => {//IQueryable sorgu işini bitirdikten snra tolist ile çekilir ve  IEnumerable olur ve işlem yapılır Vetabanından IQueryable ile çekilir

                Console.WriteLine(x);
            
            });


            //////////////////////////////////////////////////2.Örnek ForAll() //////////////////////////////////////////////////////////////////////////////////


            newarray.ForAll(x => {//ParallelQuery geri dönüş tipindeki newarray için foreach yerine ForAll daha performanslıdır ve dönerken de birden fazla thread çalışır
                //  newarray.ToList().ForEach() kısmına geince artık tek thread çalışır bu da iyi değil
                Console.WriteLine(x);

            });


            //Parallel sorgu kullanacağımız zaman süre ölçümü yaparak senkron mu asenkron mu kullanacaksak buna karar vermek gerekir
            //Parallel işlemlerde işlemler çok fazla veya çok ağır işlemse kullanmak daha uygundur

            //////////////////////////////////////////////////3.Örnek LINQ işlemlerinde veritabında işlem yapalım() //////////////////////////////////////////////////////////////////////////////////

            var context = 10;//contexti örnek bir veritabanı bağlantısı olarak düşünelim

            //var product = context.Product.AsParallel().where(p => p.ListPrice > 10M);//geriye Geriye IQueryable döndüğümüz için şuan veritabanında bir istek çalışmaz eğer toList() yapsaydık işlem veritabanına gönderilecekti.

            //product.ForAll(x => //Bu işlemi yaparken veritabanından istek yapar ve birden fazla thread çalışır
            //{ 
            //    Console.WriteLine(x.name);
            //});


            //////////////////////////////////////////////////4.Örnek LINQ işlemlerinde kaç işlemcide çalışacağımızı belirlemek //////////////////////////////////////////////////////////////////////////////////


            //var product = context.Product.AsParallel().WithDegreeOfParallelism(2).where(p => p.ListPrice > 10M);//2 işlemcide çalışır


            //////////////////////////////////////////////////5.Örnek LINQ işlemlerinde %100 PArallel çalıştırma //////////////////////////////////////////////////////////////////////////////////


            //var product = context.Product.AsParallel().WithExecutionMode(ParallelExecutionMode.ForceParallelism).where(p => p.ListPrice > 10M);//default ve ForceParallelism var daima parallel için ForceParallelism seçtik

            /////////////6.Örnek LINQ işlemlerinde AsOrdered ile parallel çalışan arrayi önceki sıralamasıyla gelmesini sağlarız ama maliyetli /////////////////////////////////////////////////////


            // var product = context.Product.AsParallel().AsOrdered().where(p => p.ListPrice > 10M);//default ve ForceParallelism var daima parallel için ForceParallelism seçtik

            /////////////6.Örnek LINQ işlemlerinde Error Handle /////////////////////////////////////////////////////


            // var product = context.Product.AsParallel().AsOrdered().where(p => p.ListPrice > 10M);//default ve ForceParallelism var daima parallel için ForceParallelism seçtik

            //try
            //{
            //    product.ForAll(x => //Bu işlemi yaparken veritabanından istek yapar ve birden fazla thread çalışır
            //    {
            //        Console.WriteLine(x.name);
            //    });
            //}
            //catch (AggregateException ex)
            //{
            //    ex.InnerExceptions.ToList().ForEach(x=> {

            //        Console.WriteLine(ex.Message);
            //    });

            //}

            /////////////.Örnek LINQ işlemlerinde Cansellation işlemleri/////////////////////////////////////////////////////


            //CancellationTokenSource token;

            //try
            //{
            //    var product = context.Product.AsParallel().WithCansellation(token.Token).where(p => p.ListPrice > 10M);// diyelim ki bu işlemi iptal ediyoruz bir tuşla. o zaman.    token.Cancel(); çalışır ve hataya düşer

            //    product.ToList().ForEach(x => {

            //  token.Token.ThrowIfCansellationRequested();//foreach içinde hata yakalanmasını istiyorsak burada koyarızç bunu burda kullanmazsak yine durduğumuz hata fırlatacaktır
            //        Console.WriteLine(x);


            //    });

            //}
            //catch (OperationCanceledException ex)
            //{

            //    Console.WriteLine("İşlem İptal Edildi");
            //}
            //catch (Exception ex2)
            //{
            //    Console.WriteLine("Genel bir hata oluştu");
            //}



        }
    }
}
