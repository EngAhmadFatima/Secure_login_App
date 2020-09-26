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
    public partial class NewPassword : Form
    {
        string NationalNumber;
        string Username;
        ResetPasswordForm rpf;

        // باني الفورم مع متغيرات
        public NewPassword(string UserName, string NatNumber, ResetPasswordForm RPF)
        {
            InitializeComponent();
            NationalNumber = NatNumber;
            Username = UserName;
            rpf = RPF;
        }
        public NewPassword(string UserName, string NatNumber)
        {
            InitializeComponent();
            NationalNumber = NatNumber;
            Username = UserName;
        }

        // الضغط على زر إعادة تهيئة
        private void btn_ok_Click(object sender, EventArgs e)
        {
            string Password = txt_password.Text.ToString().Trim();
            string Confirm = txt_confermPass.Text.Trim();

            if(Password !="" || Confirm != "")
            {
                if (IsPasswordValid(Password))
                {
                    if (Password == Confirm)
                    {
                        // Create an Opject from User class and call NewPassword function
                        User user = new User();
                        if (user.NewPassword(Username, Password, NationalNumber))
                        {
                            MessageBox.Show("Password Reseted.");
                            this.Close();
                            if (rpf != null) { rpf.Close(); }
                        }
                        else { MessageBox.Show("Error !!"); }
                    }
                    else { MessageBox.Show("Passwords do not matched !!"); }
                }
                else
                {
                    MessageBox.Show("Your Password: \n" +
               " - must contain at least one lowercase letter.\n" +
               " - must contain at least one uppercase letter.\n" +
               " - must contain at least one number. \n" +
               " - must contain at least one special character.\n" +
               " - must be between 12 and 18 characters long.\n");
                }

            }
            else { MessageBox.Show("Please fill Text boxes !!"); }
        }

        // الضغط على زر الالغاء
        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // اظهار تعليق عند ادخال كلمة المرور يبين أنها قد حققت الشروط المطلوبة
        private void txt_password_TextChanged(object sender, EventArgs e)
        {
            if (txt_password.Text.Trim() != "")
            {
                if (IsPasswordValid(txt_password.Text.Trim()))
                {
                    lbl_passwordvlid.Text = "Password OK";
                    lbl_passwordvlid.ForeColor = Color.Green;
                }
                else
                {
                    lbl_passwordvlid.Text = "Password Not OK";
                    lbl_passwordvlid.ForeColor = Color.Red;
                }
            }
            else lbl_passwordvlid.Text = "";
        }

        //تابع التحقق من كلمة المرور اذا كانت تحقق الشروط المطلوبة
        public bool IsPasswordValid(string password)
        {
            return password.Length >= 12 &&
                   password.Length <= 18 &&
                   password.Any(char.IsDigit) &&
                   password.Any(char.IsUpper) &&
                   password.Any(char.IsLower) &&
                   (password.Any(char.IsSymbol) || password.Any(char.IsPunctuation));
        }

        // اظهار تعليق عند ادخال تأكيد كلمة المرور يبين أنها تطابق كلمة المرور
        private void txt_confermPass_TextChanged(object sender, EventArgs e)
        {
            if (txt_confermPass.Text != "")
            {
                if (txt_password.Text.Trim() == txt_confermPass.Text.Trim())
                {
                    lbl_conferm.Text = "Matched";
                    lbl_conferm.ForeColor = Color.Green;
                }
                else
                {
                    lbl_conferm.Text = "Not Matched";
                    lbl_conferm.ForeColor = Color.Red;
                }
            }
            else { lbl_conferm.Text = ""; }
        }

        private void NewPassword_Load(object sender, EventArgs e)
        {

        }
    }
}
