using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TaskCansellationApp
{
    public partial class Form1 : Form
    {
        CancellationTokenSource tokenSource = new CancellationTokenSource();
        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)//Başlat Click
        {
            Task<HttpResponseMessage> myResponseTask;
            try
            {


                myResponseTask = new HttpClient().GetAsync("https://localhost:44303/api/Home", tokenSource.Token);//Task<HttpResponseMessage> döner
                //async metodun overloadlarında CancellationToken yoksa token alamayız bu işlemde işlemle beraber token alırız çünkü var bu tokenla beraber istediğimiz yerde iptal edebiliriz.
                await myResponseTask;

                var content = await myResponseTask.Result.Content.ReadAsStringAsync();

                richTextBox1.Text = content;
            }
            catch (TaskCanceledException ex)//Tokenla iptal ettiğimizde TaskCanceledException tipinde hata fırlatır onu böyle yakalarız
            {

                MessageBox.Show(ex.Message);
            }


            //myResponseTask.Status
            //myResponseTask.IsCanceled
            //myResponseTask.IsCompleted
            //Gibi sonuc değerlerine göre işlemler yapabiliriz.

        }

        private void button2_Click(object sender, EventArgs e)//Durdur Click
        {
            tokenSource.Cancel();
        }
    }
}
