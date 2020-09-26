using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace SCR_Project
{
    public partial class UpdateInfoForm : Form
    {
        // الربط مع قاعدة البيانات
        OleDbConnection con = DatabaseFunctions.ConnectDatabase();
        
        string Username;

        // باني التابع مع متغير
        public UpdateInfoForm(string UserName)
        {
            InitializeComponent();
            Username = UserName;
        }

        // رابط يفتح نافذة باسوورد جديد
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string UserName = txt_username.Text.ToString().Trim();
            string NatNumber = txt_natnumber.Text.ToString().Trim();

            if(UserName != "" && NatNumber != "")
            {
                NewPassword np = new NewPassword(UserName, NatNumber);
                np.ShowDialog();
            }
            else { MessageBox.Show("Please Fill Text Boxes !!"); }
        }

        // الضغط على زر تعديل البيانات 
        private void btn_update_Click(object sender, EventArgs e)
        {
            string UserName = txt_username.Text.ToString().Trim();
            string NatNumber = txt_natnumber.Text.ToString().Trim();
            string Phone = txt_phonenumber.Text.ToString().Trim();
            string Password = txt_password.Text.ToString().Trim();
            User user = new User();
            if (UserName != "" && NatNumber != "" && Phone != "" && Password !="")
            {
                if (user.Login(Username, Password))
                {
                    if (user.UpdateInfo(UserName, NatNumber, Phone, Password))
                    {
                        MessageBox.Show("Update is Done.");
                        // FillTextBoxs();
                    }
                    else { MessageBox.Show("Error !!"); }
                }
                else { MessageBox.Show("Your Password Not Correct !!"); }
            }
            else { MessageBox.Show("Please Fill Text Boxes !!"); }
        }

        // عند تحميل الفورم 
        private void UpdateInfoForm_Load(object sender, EventArgs e)
        {
            // اظهار بيانات المستخدم
            FillTextBoxs();
        }

        // تابع يقوم بملأ بيانات المستخدم بعد احضارها من قاعدة البيانات 
        public void FillTextBoxs()
        {
            con.Close();
            OleDbCommand command = new OleDbCommand("SELECT * FROM Users WHERE Username = '" + Username + "'", con);
            con.Open();
            OleDbDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                txt_username.Text = reader["Username"].ToString().Trim();
                txt_natnumber.Text = reader["NationalNumber"].ToString().Trim();
                txt_phonenumber.Text = reader["MobileNumber"].ToString().Trim();
            }
            con.Close();
        }

        // الغاء
        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
