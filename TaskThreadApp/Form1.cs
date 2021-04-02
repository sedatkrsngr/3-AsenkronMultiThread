using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TaskThreadApp
{
    public partial class Form1 : Form
    {
        public static int Counter { get; set; } = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private async void btnStart_Click(object sender, EventArgs e)
        {

            var a = Go(progressBar1);
            var b = Go(progressBar2);

            await Task.WhenAll(a, b);//ikisi aynı anda başlasın
        }


        public async Task Go(ProgressBar bar)
        {
            
            await Task.Run(() =>//Task.Run O anki işlem için ayrı bir thread kullanılması gerektiğini bilgisayara söyledik eğer kullanmasaydık ilk işlem bitmeden 2. cisi başlamayacak ve ekran kitlenecek
            {

                Enumerable.Range(1, 100).ToList().ForEach(x =>
                {
                    Thread.Sleep(100);
                    bar.Invoke((MethodInvoker)delegate { bar.Value = x; });//buraları öğrenmeye gerek yok sadece bu işlem için kullandık.

                });
            });

        }

        private void btnCounter_Click(object sender, EventArgs e)
        {
            btnCounter.Text = Counter++.ToString();
        }
    }
}
