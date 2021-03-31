
namespace TaskFormApp
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnread = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.btnCounter = new System.Windows.Forms.Button();
            this.textBoxCounter = new System.Windows.Forms.TextBox();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // btnread
            // 
            this.btnread.Location = new System.Drawing.Point(28, 21);
            this.btnread.Name = "btnread";
            this.btnread.Size = new System.Drawing.Size(75, 23);
            this.btnread.TabIndex = 0;
            this.btnread.Text = "Dosya Oku";
            this.btnread.UseVisualStyleBackColor = true;
            this.btnread.Click += new System.EventHandler(this.btnread_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(28, 91);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(180, 96);
            this.richTextBox1.TabIndex = 1;
            this.richTextBox1.Text = "";
            // 
            // btnCounter
            // 
            this.btnCounter.Location = new System.Drawing.Point(346, 21);
            this.btnCounter.Name = "btnCounter";
            this.btnCounter.Size = new System.Drawing.Size(87, 24);
            this.btnCounter.TabIndex = 2;
            this.btnCounter.Text = "sayac arttır";
            this.btnCounter.UseVisualStyleBackColor = true;
            this.btnCounter.Click += new System.EventHandler(this.btnCounter_Click);
            // 
            // textBoxCounter
            // 
            this.textBoxCounter.Location = new System.Drawing.Point(346, 51);
            this.textBoxCounter.Name = "textBoxCounter";
            this.textBoxCounter.Size = new System.Drawing.Size(122, 22);
            this.textBoxCounter.TabIndex = 3;
            // 
            // richTextBox2
            // 
            this.richTextBox2.Location = new System.Drawing.Point(288, 91);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.Size = new System.Drawing.Size(180, 96);
            this.richTextBox2.TabIndex = 4;
            this.richTextBox2.Text = "";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(488, 250);
            this.Controls.Add(this.richTextBox2);
            this.Controls.Add(this.textBoxCounter);
            this.Controls.Add(this.btnCounter);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.btnread);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnread;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button btnCounter;
        private System.Windows.Forms.TextBox textBoxCounter;
        private System.Windows.Forms.RichTextBox richTextBox2;
    }
}

