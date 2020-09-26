using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace SCR_Project
{
    // كلاس توابع التهشير
    class HashFunctions
    {
        //تابع تهشير كلمة المرور مع الرقم الوطني
        public static string Pass_HV(string Password, string NatNumber)
        {
            UnicodeEncoding un = new UnicodeEncoding();
            MD5 md = MD5.Create();
            byte[] PasswordByte = un.GetBytes(Password);
            byte[] NatNumberByte = un.GetBytes(NatNumber);
            byte[] merged = new byte[PasswordByte.Length + NatNumberByte.Length];
            PasswordByte.CopyTo(merged, 0);
            NatNumberByte.CopyTo(merged, PasswordByte.Length);
            byte[] HashResult = md.ComputeHash(merged);
            string Pass_HV = Convert.ToBase64String(HashResult);
            return Pass_HV;
        }

        // تابع التهشير المطبق على اسم المستخدم والرقم الوطني ورقم الجوال
        public static string Personal_Info_HV(string UserName, string NatNumber, string Phone)
        {
            UnicodeEncoding un = new UnicodeEncoding();
            MD5 md = MD5.Create();
            byte[] UserNameByte = un.GetBytes(UserName);
            byte[] NatNumberByte = un.GetBytes(NatNumber);
            byte[] PhoneByte = un.GetBytes(Phone);
            byte[] merged = new byte[UserName.Length + NatNumberByte.Length + PhoneByte.Length];
            UserNameByte.CopyTo(merged, 0);
            NatNumberByte.CopyTo(merged, UserNameByte.Length);
            PhoneByte.CopyTo(merged, NatNumberByte.Length);
            byte[] HashResult = md.ComputeHash(merged);

            string Personal_Info_HV = Convert.ToBase64String(HashResult);
            return Personal_Info_HV;
        }
    }
}
