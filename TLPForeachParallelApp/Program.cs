

using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace TLPForeachParallelApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch stopwatch = new Stopwatch();//işlemlerde süreleri görmek için kullanabilirsin. Genel Kültür olarak kalsın

            stopwatch.Start();
            string picsPath = @"C:\Users\limit\Desktop\Resimler"; // \ işaretinde hata vermesin diye başına @ işareti konuldu Burada masaüstümüzde bulunan resim klasörünün yolunu verdik

            var files = Directory.GetFiles(picsPath); //string[] dizisi alır

            Parallel.ForEach(files,(resimNesnesi) => {//Bununla beraber bir işlem için çoklu thread kullanarak özellikle uzun işlemlerde süreyi kısaltabiliriz kısa işlemler de senkrona göre daha yavaş olabilir. Bu yüzden kullandığımız yerleri iyi seçmeliyiz. Mesela bu işlem de multithread senkrondan daha yavaş çalışır Ama daha büyük işlemlerde daha hızlı olur. Bildiğimiz foreachtan farkı yok

                Console.WriteLine("Thread No"+Thread.CurrentThread.ManagedThreadId);
                Image ımage = new Bitmap(resimNesnesi);//Bitmap imagedan kalıtım alır ondan nesne oluşturduk genel kültür olarak aklında kalsın

                var kucukResim = ımage.GetThumbnailImage(50, 50, () => false, IntPtr.Zero);

                kucukResim.Save(Path.Combine(picsPath, "KucukResim", Path.GetFileName(resimNesnesi)));

            });

   
            stopwatch.Stop();

            Console.WriteLine("İşlem Bitti ve süresi : "+stopwatch.ElapsedMilliseconds);


            ///////////////////////////////////////////// 2. Örnek RaceCondition olmasını engelleme  ////////////////////////////////////////////////////////////

            long FilesByte = 0;

            string picsPath2 = @"C:\Users\limit\Desktop\Resimler";

            var files2 = Directory.GetFiles(picsPath2);

            Parallel.ForEach(files2, (resimNesnesi2) => {

                Console.WriteLine("Thread No" + Thread.CurrentThread.ManagedThreadId);

                FileInfo f = new FileInfo(resimNesnesi2);


                Interlocked.Add(ref FilesByte, f.Length);//bu Class metodunu kullanmazsak birden fazla thread aynı işlemi yabilecek ihtimali bu işlem için gereksiz bir kullanım olacak Buna RaceCondition denir. Burada resimlerin ana dosyasının dosya boyutunu yanlış hesaplayacak. Ama bu class bir thread işlemini yapmadan diğer threada izin vermez belki lock yaratır ama tutarsızlığı engeller ve düzgün bir arttırım olur azaltma işlemi için  Interlocked.Decrement değiştirmek için ise Interlocked.Exchange metodu güvenlidir.


            });


            //////////////////////////////////////////////3.Örnek  RaceCondition Örnek ////////////////////////////////////////////////////////////////////

            int deger = 0;

            Parallel.ForEach(Enumerable.Range(1,100000).ToList(),(sayi)=> {

                deger = sayi;
            
            });

            Console.WriteLine(deger);//burada 100000 değeri en son olacağı için onu vermesi gerekiyor ama bazen RaceCondition durumu oluşur ve farklı bir sayı ekrana verebilir


            ////////////////////////////////////////////4.Örnek  Parallel.For örnek/////////////////////////////////////////////////////////////////////////////////////


            long totalByte = 0;


            string picsPath3 = @"C:\Users\limit\Desktop\Resimler";

            var files3 = Directory.GetFiles(picsPath3);

            Parallel.For(0,files3.Length, (index) => {


               var file= new FileInfo(files3[index]);

                Interlocked.Add(ref totalByte, file.Length);//Race Condition olmasın diye bunu kullanırız


            });


            ////////////////////////////////////////////5.Örnek  Parallel.Foreach Daha Performanslı örnek/////////////////////////////////////////////////////////////////////////////////////


            long Total = 0;



            Parallel.ForEach(Enumerable.Range(1, 100000).ToList(),()=>0, (gelenSayi,dongu, threadIslemToplamı) => {//Foreach ile kullanımı her bir thread kendi atandığı sayıları topladıktan sonra Interlocked eklensin

                threadIslemToplamı += gelenSayi;

                return threadIslemToplamı;

            },(threadIslemToplamıSonucu) =>Interlocked.Add(ref Total, threadIslemToplamıSonucu));

            ////////////////////////////////////////////6.Örnek  Parallel.For Daha Performanslı örnek/////////////////////////////////////////////////////////////////////////////////////
            

            Parallel.For(0,100, () => 0, (gelenSayi, dongu, threadIslemToplamı) => {//For ile kullanımı her bir thread kendi atandığı sayıları topladıktan sonra Interlocked eklensin

                threadIslemToplamı += gelenSayi;

                return threadIslemToplamı;

            }, (threadIslemToplamıSonucu) => Interlocked.Add(ref Total, threadIslemToplamıSonucu));


        }
    }
}
