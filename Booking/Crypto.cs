using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking
{
    public static class Crypto
    {
        public static String kdf(String password, String salt)
        {
            int c = 3;
            int dkLength = 20;
            String t = password + salt;
            for (int i = 0; i < c; i++)
                t = hash(t);
            
            return t[0..dkLength];
        }

        private static String hash(String input)
        {
            return Convert.ToHexString(System.Security.Cryptography.SHA1.HashData(Encoding.UTF8.GetBytes(input)));
        }
    }
}
