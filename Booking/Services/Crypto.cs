using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Services
{
    public static class Crypto
    {
        public static string kdf(string password, string salt)
        {
            int c = 3;
            int dkLength = 20;
            string t = password + salt;
            for (int i = 0; i < c; i++)
                t = hash(t);

            return t[0..dkLength];
        }

        private static string hash(string input)
        {
            return Convert.ToHexString(System.Security.Cryptography.SHA1.HashData(Encoding.UTF8.GetBytes(input)));
        }
    }
}
