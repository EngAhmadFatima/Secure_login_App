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
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        // الغاء تسجيل الدخول
        private void btn_cancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // الضغط على زر تسجيل الدخول
        private void btn_login_Click(object sender, EventArgs e)
        {
            string Username = txt_username.Text.Trim();
            string Password = txt_password.Text.Trim();

            if(Username == "" || Password == "")
            {
                MessageBox.Show("Pleas Fill All Text Boxes !!");
            }
            else
            {
                User user = new User();

                if (user.Login(Username, Password))
                {
                    MainForm mf = new MainForm();
                    mf.Show();
                    mf.PrintUsername(Username);
                    this.Hide();
                }
                else MessageBox.Show("Username or Password Not Correct !!");
            }
        }

        // رابط يفتح نافذة تهيئة كلمة المرور
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ResetPasswordForm rpf = new ResetPasswordForm();
            rpf.ShowDialog();
        }

        // رابط يفتح نافذة تسجيل حساب جديد
        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            RegistrationForm rf = new RegistrationForm();
            rf.ShowDialog();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
        }
    }
}
