using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAFKiller.Core
{
    public class StringBinaryConverter
    {
        public string StringToBinary(string data)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in data.ToCharArray())
                sb.Append(Convert.ToString(c, 2).PadLeft(8, '0'));
            return sb.ToString();
        }

        public string BinaryToString(string data)
        {
            List<byte> bytes = new List<byte>();
            for (int i = 0; i < data.Length; i += 8)
                bytes.Add(Convert.ToByte(data.Substring(i, 8), 2));
            return Encoding.ASCII.GetString(bytes.ToArray());
        }
    }
}
