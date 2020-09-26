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
    public partial class ResetPasswordForm : Form
    {
        public ResetPasswordForm()
        {
            InitializeComponent();
        }

        private void ResetPasswordForm_Load(object sender, EventArgs e)
        {

        }

        private void txt_username_TextChanged(object sender, EventArgs e)
        {

        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_ok_Click(object sender, EventArgs e)
        {
            string UserName = txt_username.Text.ToString().Trim();
            string NatNumber = txt_natnumber.Text.ToString().Trim();
            string Phone = txt_phonenumber.Text.ToString().Trim();

            User user = new User();
            // اذا تطابقت نتيجة تهشير البيانات التي أدخلها المستخدم مع نتيجة تابع التهشير المخزنة في قاعدة البيانات
           if( user.ResetPassword(UserName, NatNumber, Phone))
            {
                MessageBox.Show("Correct Information.");
                NewPassword np = new NewPassword(UserName,NatNumber,this);
                np.ShowDialog();
            }
           else { MessageBox.Show("InCorrect Information"); }
        }
    }
}
