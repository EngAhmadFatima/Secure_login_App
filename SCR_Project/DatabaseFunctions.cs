using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Reflection;

//هذا الكلاس خاص بالتوابع التي تتعامل مع قاعدة البيانات
namespace SCR_Project
{
    class DatabaseFunctions
    {
        // الربط مع قاعدة البيانات
        static OleDbConnection  con = ConnectDatabase();
        public static OleDbConnection ConnectDatabase()
        {
            string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path+@"\Login.accdb";
            OleDbConnection con = new OleDbConnection(connectionString);
            return con;
        }

        // INSERT into Database.
        public static void InsertIntoUsers(string Username, string Pass_HV, string NationalNumber, string MobileNumber, string Personal_Info_HV)
        {
            OleDbCommand Command1 = new OleDbCommand("INSERT INTO Users(Username,Pass_HV,NationalNumber,MobileNumber,Personal_Info_HV) VALUES (@Username,@Pass_HV,@NationalNumber,@MobileNumber,@Personal_Info_HV)", con);
            Command1.Parameters.AddWithValue("@Username", Username);
            Command1.Parameters.AddWithValue("@Pass_HV", Pass_HV);
            Command1.Parameters.AddWithValue("@NationalNumber", NationalNumber);
            Command1.Parameters.AddWithValue("@MobileNumber", MobileNumber);
            Command1.Parameters.AddWithValue("@Personal_Info_HV", Personal_Info_HV);
            con.Open();
            Command1.ExecuteNonQuery();
            con.Close();
        }

        // If this username is used or not.
        public static bool UsernameUsed(string Username)
        {
            int count = 0;
            OleDbCommand Command = new OleDbCommand("SELECT ID FROM Users WHERE Username='" + Username + "'", con);
            con.Open();
            OleDbDataReader reader = Command.ExecuteReader();
            while (reader.Read()) { count++; }
            con.Close();
            if (count > 0) return true;
            else return false;
        }

        // UPDATE Password in Database.
        public static void UpdatePassword(string Username, string Pass_HV)
        {
            OleDbCommand command = new OleDbCommand("UPDATE Users SET Pass_HV = @Pass_HV WHERE Username = @Username", con);
            command.Parameters.AddWithValue("@Pass_HV", Pass_HV);
            command.Parameters.AddWithValue("@Username", Username);
            con.Open();
            command.ExecuteNonQuery();
            con.Close();
        }

        // UPDATE User information.
        public static void UpdateInformation(int ID ,string Username, string Pass_HV, string NationalNumber, string MobileNumber, string Personal_Info_HV)
        {
            OleDbCommand Command1 = new OleDbCommand("UPDATE Users SET"+
                " Username = @Username"+
                ",Pass_HV = @Pass_HV "+
                ",NationalNumber = @NationalNumber"+
                ",MobileNumber = @MobileNumber"+
                ",Personal_Info_HV = @Personal_Info_HV"+
                " WHERE ID = @ID", con);
            Command1.Parameters.AddWithValue("@Username", Username);
            Command1.Parameters.AddWithValue("@Pass_HV", Pass_HV);
            Command1.Parameters.AddWithValue("@NationalNumber", NationalNumber);
            Command1.Parameters.AddWithValue("@MobileNumber", MobileNumber);
            Command1.Parameters.AddWithValue("@Personal_Info_HV", Personal_Info_HV);
            Command1.Parameters.AddWithValue("@ID", ID);
            con.Open();
            Command1.ExecuteNonQuery();
            con.Close();
        }

        // Get User ID by Username.
        public static int GetUserID(string Username)
        {
            OleDbCommand command = new OleDbCommand("SELECT ID FROM Users WHERE Username = '"+Username+"'",con);
            con.Open();
             int id = int.Parse(command.ExecuteScalar().ToString());
            con.Close();
            return id;
        }
    }
}
