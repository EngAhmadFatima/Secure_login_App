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
    public partial class RegistrationForm : Form
    {
        public RegistrationForm()
        {
            InitializeComponent();
        }

        // الضغط على زر تسجيل
        private void btn_registration_Click(object sender, EventArgs e)
        {
            string UserName = txt_username.Text.ToString().Trim();
            string NatNumber = txt_natnumber.Text.ToString().Trim();
            string Phone = txt_phonenumber.Text.ToString().Trim();
            string Password = txt_password.Text.ToString().Trim();
            string Confirm = txt_confermPass.Text.Trim();
            if (UserName =="" || NatNumber == "" || Phone == "" || Password == "")
            {
                MessageBox.Show("Pleas Fill All Text Boxes !!");
            }
            else
            {
                if (IsPasswordValid(Password))
                {
                    if (Password == Confirm)
                    {
                        User user = new User();
                        MessageBox.Show(user.Register(UserName, NatNumber, Phone, Password));
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Passwords do not matched !!");
                    }
                }
                else { MessageBox.Show("Your Password: \n"+
                    " - must contain at least one lowercase letter.\n" +
                    " - must contain at least one uppercase letter.\n" +
                    " - must contain at least one number. \n"+
                    " - must contain at least one special character.\n" +
                    " - must be between 12 and 18 characters long.\n"); }
            }
         
        }

        //  معرفة اذا كان اسم المستخدم مستخدم من قبل أم لا عند الكتابة في الحقل المخصص 
        private void txt_username_TextChanged(object sender, EventArgs e)
        {
            if(txt_username.Text.ToString().Trim() != "")
            {
                if (DatabaseFunctions.UsernameUsed(txt_username.Text.ToString().Trim()))
                {
                    lbl_userused.Text = "Used";
                    lbl_userused.ForeColor = Color.Red;
                }
                else
                {
                    lbl_userused.Text = "Not Used";
                    lbl_userused.ForeColor = Color.Green;
                }
            }else lbl_userused.Text = "";
        }

        // تابع معرفة اذا كان الباسوورد يحقق الشروط المطلوبة أم لا
        public bool IsPasswordValid(string password)
        {
            return password.Length >= 12 &&
                   password.Length <= 18 &&
                   password.Any(char.IsDigit) &&
                   password.Any(char.IsUpper) &&
                   password.Any(char.IsLower) &&
                   (password.Any(char.IsSymbol) || password.Any(char.IsPunctuation));
        }

        // عند الكتابة في الحقل المخصص لكلمة المرور
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
            }else lbl_passwordvlid.Text = "";

        }

        // عند الكتابة في الحقل المخصص لتأكيد كلمة المرور
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
            }else {lbl_conferm.Text = ""; }
        }

        // عند الضغط على زر الغاء
        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void RegistrationForm_Load(object sender, EventArgs e)
        {

        }
    }
}
