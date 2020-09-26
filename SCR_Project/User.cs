using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;

namespace SCR_Project
{
    // كلاس اليوزر والذي يحتوي على كافة التوابع المتعلقة بالعمليات على اليوزر
    
    class User
    {
        // Data base connection
        OleDbConnection con = DatabaseFunctions.ConnectDatabase();

        // باني الصف
        public User()
        {
        }

        // تابع تسجيل يوزر جديد
        public string Register(string Username, string NationalNumber, string MobileNumber, string Password)
        {
            // تابع التهشير المطبق على كلمة المرور والرقم الوطني
           string Pass_HV = HashFunctions.Pass_HV(Password, NationalNumber);

            // تابع التهشير المطبق على اسم المستخدم والرقم الوطني ورقم الجوال
            string Personal_Info_HV = HashFunctions.Personal_Info_HV(Username, NationalNumber, MobileNumber);

            try
            {
                if (!DatabaseFunctions.UsernameUsed(Username))
                {
                    DatabaseFunctions.InsertIntoUsers(Username, Pass_HV, NationalNumber, MobileNumber, Personal_Info_HV);
                    return "Registration Success";
                }
                else { return "User Name Used Before !! Pleas Choose another one."; }

            }
            catch(Exception ex) { return ex.Message; }
        }

        // تابع تسجيل الدخول
        public bool Login(string Username, string Password)
        {
            string pass = "", nat = "";
            try
            {
                OleDbCommand Command = new OleDbCommand("SELECT Pass_HV,NationalNumber FROM Users WHERE Username='" + Username + "'", con);
                con.Open();
                OleDbDataReader reader = Command.ExecuteReader();
                while (reader.Read())
                {
                     pass = reader["Pass_HV"].ToString().Trim();
                     nat = reader["NationalNumber"].ToString().Trim();
                }
                con.Close();
                if (pass !="" && nat !="")
                {
                    string HashedPass = HashFunctions.Pass_HV(Password, nat);
                    if (pass == HashedPass) { return true; }
                    else return false;
                }
                else return false;
            }
            catch { return false; }
          
        }

        // تابع تعديل البيانات
        public bool UpdateInfo(string Username, string NationalNumber, string MobileNumber ,string Password)
        {
            int ID = DatabaseFunctions.GetUserID(Username);
            string Pass_HV = HashFunctions.Pass_HV(Password, NationalNumber);
            string Personal_Info_HV = HashFunctions.Personal_Info_HV(Username, NationalNumber, MobileNumber);
            try
            {
                DatabaseFunctions.UpdateInformation(ID, Username, Pass_HV, NationalNumber, MobileNumber, Personal_Info_HV);
                return true;
            }
            catch { return false; }
        }

        // تابع تهيئة كلمة المرور
        public bool ResetPassword(string Username, string NationalNumber, string MobileNumber)
        {
            // تابع التهشير المطبق على اسم المستخدم والرقم الوطني وكلمة المرور
            string Personal_Info_HV = HashFunctions.Personal_Info_HV(Username, NationalNumber, MobileNumber);

            string Info_HV="";
            try
            {
                OleDbCommand Command = new OleDbCommand("SELECT Personal_Info_HV FROM Users WHERE Username='" + Username + "'", con);
                con.Open();
                OleDbDataReader reader = Command.ExecuteReader();
                while (reader.Read())
                {
                    Info_HV = reader["Personal_Info_HV"].ToString().Trim();
                }
                con.Close();
                if (Info_HV != "")
                {
                    // اذا تطابق نتيجة تابع تهشير البيانات التي ادخلها المستخدم مع النتيجة المخزنة في قاعدة البيانات
                    if (Info_HV == Personal_Info_HV) { return true; }
                    else return false;
                }
                else return false;
            }
            catch { return false; }
        }

        // تابع اضافة كلمة مرور جديدة
        public bool NewPassword(string Username, string Password, string NationalNumber)
        {
            string Pass_HV = HashFunctions.Pass_HV(Password, NationalNumber);
            try
            {
                DatabaseFunctions.UpdatePassword(Username, Pass_HV);
                return true;
            }
            catch { return false; }
        }
    }
}
