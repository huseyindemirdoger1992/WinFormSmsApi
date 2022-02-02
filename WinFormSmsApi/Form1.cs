using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormSmsApi.Models;

namespace WinFormSmsApi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SmsApiService smsApi = new SmsApiService();
            smsApi.SmsSender(textBox2.Text, textBox1.Text);
            MessageBox.Show("Sms Gönderi işlemi başarılı.","Durum",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }
    }
}
