using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAFKiller.Core
{
    public class HexDecConverter
    {
        public static double HexToDec(string hexValue)
        {
            char[] hexChars = hexValue.ToUpperInvariant().ToCharArray();
            Array.Reverse(hexChars);
            double result = 0;
            string reference = "0123456789ABCDEF";
            for(int i = 0; i < hexChars.Length; i++)
            { result += (reference.IndexOf(hexChars[i]) * Math.Pow(16d, (double)i)); }
            return result;
        }

        public static string DecToHex(int dec)
        {
            if (dec < 1) return "0";
            int hex = dec;
            string hexStr = string.Empty;
            while(dec > 0)
            {
                hex = dec % 16;
                if(hex < 10)
                { hexStr = hexStr.Insert(0, Convert.ToChar(hex + 48).ToString()); }
                else
                { hexStr = hexStr.Insert(0, Convert.ToChar(hex + 55).ToString()); }
                dec /= 16;
            }
            return hexStr;
        }
    }
}
