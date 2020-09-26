using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SCR_Project
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        // 
        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        // تابع لطباعة اسم المستخدم على الفورم
        public void PrintUsername(string Username)
        {
            lbl_username.Text = Username;
        }

        // تابع انهاء الفورم
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        // تابع تسجيل الخروج من النظام
        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            LoginForm lf = new LoginForm();
            lf.Show();
        }

        // تابع تعديل البيانات
        private void editInformationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdateInfoForm uf = new UpdateInfoForm(lbl_username.Text);
            uf.ShowDialog();
        }

        // تابع انشاء حساب جديد
        private void newAccountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RegistrationForm rf = new RegistrationForm();
            rf.ShowDialog();
        }
    }
}
