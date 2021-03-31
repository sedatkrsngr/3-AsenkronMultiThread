using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TaskFormApp
{
    public partial class Form1 : Form
    {
        public int counter { get; set; } = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private async void btnread_Click(object sender, EventArgs e)
        {
            string data = string.Empty;
            // string data = ReadFile(); senkron



              Task<string> okuma =  ReadFileAsync(); // asenkton kullanınca metotda async yok sonradan ekledik await kullanana kadar thread kullanılmıyor şuan sadece atama yaptık Task söz vermek gibi düşünebiliriz. Yani o veriyi mutlaka sana döndürecek biz sadece awaitle çağırırız Eğer arada başka işler yaptıracaksak await ile çağırmayı daha sonra yaparız diğer işlemleri yaptıktan sonra await ile ilgili atamaları gerçekleştiririz
            
            richTextBox2.Text = await new HttpClient().GetStringAsync("https://www.google.com");//Burada bir işlem yapsın diye bir threadı meşgul ettik
            
            data = await okuma;
            
            richTextBox1.Text = data;
            
        }

        private void btnCounter_Click(object sender, EventArgs e)//bunu koymamızın sebebi işlem olurken sayaç işlemini gerçekleştirmeize izin veriyor mu kontrol etmek için  senkronda izin vermiyor işlem bitene kadar ardından eğer işlem zamanında dokunduysak senkron bitince sayactaki bastığımız kadar attırımı yapar ama asenkronda işlem sürse dahi biz yan tarafta arttırma işlemini devam ettiriyoruz
        {
            textBoxCounter.Text = counter++.ToString();

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private string ReadFile()//senkron kodumuz
        {
            string data = string.Empty;
            using (StreamReader reader = new StreamReader("dosya.txt"))//kalıtım aldığı sınıf IDisposable İnterfacesinden kalıtım aldığı için using kullanabilriz. İşlem bitince bellekten atılır dosya.text bin/debug içerisine eklendi
            {
                Thread.Sleep(5000);// İşlem yapılınca 5  sn bekletelim ki işlemimiz 5 sn sürüyormuş gibi olsun
                data = reader.ReadToEnd();
            }
            return data;
        }
        private async Task<string> ReadFileAsync()//asenkron kodumuz
        {
            string data = string.Empty;
            using (StreamReader reader = new StreamReader("dosya.txt"))//kalıtım aldığı sınıf IDisposable İnterfacesinden kalıtım aldığı için using kullanabilriz. İşlem bitince bellekten atılır dosya.text bin/debug içerisine eklendi
            {
 
               Task<string> myTask =  reader.ReadToEndAsync();//şuan thread meşgul değil await kullanıldığı an bir alt satıra geçmez

                await Task.Delay(5000);// İşlem yapılınca 5  sn bekle
                data = await myTask;//wait burada işlem tamamlanana kadar bekle demek alt satıra geçme demek
            }
            return  data;
        }
    }
}
