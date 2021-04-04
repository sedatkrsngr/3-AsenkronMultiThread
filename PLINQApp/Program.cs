using System;
using System.Collections;
using System.Linq;

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
        }
    }
}
