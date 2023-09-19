using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace md5
{
    internal class PasswordHasher
    {
        public static string HashPassword(string password)
        {
            MD5 mD5 = MD5.Create();
            byte[] b = Encoding.ASCII.GetBytes(password); //7 разрядные числа
            byte[] hash = mD5.ComputeHash(b); //перевод в хэшфункцию
            StringBuilder sb = new StringBuilder();
            foreach (var c in hash) 
            { 
            sb.Append(c.ToString("X2"));
            }
            return Convert.ToString(sb);
        }
    }
}
