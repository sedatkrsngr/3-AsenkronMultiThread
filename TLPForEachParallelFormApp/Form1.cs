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

namespace TLPForEachParallelFormApp
{
    public partial class Form1 : Form
    {
        public int Counter { get; set; } = 0;
        CancellationTokenSource CancellationToken = new CancellationTokenSource();
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)//Getir ve Başlat
        {
            List<string> urls = new List<string>() {
            "https://www.google.com",
            "https://www.microsoft.com",
            "https://www.amazon.com",
            "https://www.amazon.com",
            "https://www.google.com",
            "https://www.amazon.com",
            "https://www.google.com",
             "https://www.amazon.com",
            "https://www.amazon.com",
            "https://www.google.com",
            "https://www.amazon.com",
            "https://www.google.com"

            };


            HttpClient client = new HttpClient();

            ParallelOptions parallelOptions = new ParallelOptions();
            parallelOptions.CancellationToken = CancellationToken.Token;


            Task.Run(() =>
            {//içerideki işlemler farklı bir threadda çalışsın

                try
                {

                    Parallel.ForEach<string>(urls, parallelOptions, (gelenUrl) =>//parallelOptions kullanım sebebi Global CancellationToken erişemediğimiz durumda parallelOptions.CancellationToken.ThrowIfCancellationRequested ile erişmek için 
                    {
                        string content = client.GetStringAsync(gelenUrl).Result;//gelen hataları handle edbilmek için Result kullandık

                        string data = $"{gelenUrl}: {content.Length}";//url ve değer sayısını gösterelim listboxta


                        CancellationToken.Token.ThrowIfCancellationRequested();//işlemi iptal ettiğimizde hataya düşsün diye ekledik

                        listBox1.Invoke((MethodInvoker)delegate//farklı bir threadda ekleme işlemini yapsın diye kullandık
                        {
                            listBox1.Items.Add(data);
                        });

                    });
                }
                catch (OperationCanceledException ex)
                {
                    MessageBox.Show("İşlem İptal Edildi"+ex.Message);
                }

            });

        }

        private void button2_Click(object sender, EventArgs e)//arttır
        {
            button2.Text = Counter++.ToString();
        }

        private void button3_Click(object sender, EventArgs e)//İptal
        {
            CancellationToken.Cancel();

        }
    }
}
