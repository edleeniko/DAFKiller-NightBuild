using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CompID
{
    public class CompID
    {
        public static string GetPassword(int pwnum)
        {
            byte[] array = CompID.StringToByteArray(CompID.UniqueId);
            byte[] array2 = new byte[array.Length];
            for (int i = 0; i < array.Length; i++)
            {
                array2[i] = CompID.Pwhash[(int)(array[i] & byte.MaxValue)];
            }
            string text = Convert.ToBase64String(array2);
            text += pwnum;
            Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(text, CompID.Salt, CompID.PBKDF2_ITERATIONS + pwnum * 10);
            return Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(64));
        }

        public static string UniqueId
        {
            get
            {
                bool flag = CompID.mUniqueId == null;
                if (flag)
                {
                    CompID.mUniqueId = CompID.GetUniqueId();
                }
                return CompID.mUniqueId;
            }
        }

        private static string GetUniqueId()
        {
            string s = CompID.GetCPUID() + CompID.GetMotherBoardID() + CompID.GetBIOSSerial();
            return CompID.GetHash(s);
        }

        public static byte[] StringToByteArray(string hex)
        {
            int length = hex.Length;
            byte[] array = new byte[length / 2];
            for (int i = 0; i < length; i += 2)
            {
                array[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            }
            return array;
        }

        private static string GetCPUID()
        {
            string text = string.Empty;
            string empty = string.Empty;
            ManagementClass managementClass = new ManagementClass("Win32_Processor");
            ManagementObjectCollection instances = managementClass.GetInstances();
            foreach (ManagementBaseObject managementBaseObject in instances)
            {
                ManagementObject managementObject = (ManagementObject)managementBaseObject;
                bool flag = text == string.Empty;
                if (flag)
                {
                    text = managementObject.Properties["ProcessorId"].Value.ToString();
                }
            }
            return text;
        }

        private static string GetMotherBoardID()
        {
            ManagementObjectCollection instances = new ManagementClass("Win32_BaseBoard").GetInstances();
            try
            {
                ManagementObjectCollection.ManagementObjectEnumerator enumerator = instances.GetEnumerator();
                enumerator.MoveNext();
                return ((ManagementObject)enumerator.Current).Properties["SerialNumber"].Value.ToString();
            }
            catch
            {
            }
            return string.Empty;
        }

        private static string GetBIOSSerial()
        {
            ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher("SELECT * FROM Win32_BIOS");
            foreach (ManagementBaseObject managementBaseObject in managementObjectSearcher.Get())
            {
                ManagementObject managementObject = (ManagementObject)managementBaseObject;
                try
                {
                    return managementObject.GetPropertyValue("SerialNumber").ToString();
                }
                catch
                {
                }
            }
            return string.Empty;
        }

        private static string GetHash(string s)
        {
            MD5 md = MD5.Create();
            ASCIIEncoding asciiencoding = new ASCIIEncoding();
            byte[] bytes = asciiencoding.GetBytes(s);
            byte[] value = md.ComputeHash(bytes);
            return BitConverter.ToString(value).Replace("-", string.Empty);
        }

        private static readonly byte[] Pwhash = new byte[]
        {
            32,
            134,
            68,
            64,
            1,
            243,
            135,
            75,
            215,
            90,
            92,
            93,
            166,
            137,
            93,
            2,
            165,
            117,
            235,
            141,
            182,
            207,
            126,
            34,
            31,
            149,
            25,
            10,
            252,
            158,
            110,
            201,
            86,
            150,
            195,
            92,
            67,
            116,
            45,
            35,
            5,
            184,
            241,
            125,
            4,
            114,
            224,
            185,
            230,
            252,
            41,
            187,
            191,
            116,
            202,
            167,
            32,
            176,
            202,
            60,
            211,
            100,
            107,
            24,
            32,
            242,
            236,
            135,
            242,
            102,
            222,
            45,
            176,
            73,
            221,
            174,
            248,
            77,
            115,
            253,
            55,
            199,
            73,
            40,
            87,
            115,
            byte.MaxValue,
            41,
            185,
            172,
            220,
            105,
            203,
            83,
            244,
            9,
            189,
            157,
            22,
            180,
            193,
            55,
            139,
            106,
            173,
            140,
            177,
            109,
            140,
            224,
            55,
            32,
            104,
            29,
            216,
            178,
            8,
            172,
            179,
            204,
            42,
            47,
            106,
            165,
            182,
            112,
            151,
            137,
            6,
            153,
            199,
            185,
            17,
            140,
            194,
            4,
            65,
            101,
            140,
            195,
            91,
            174,
            0,
            40,
            43,
            185,
            95,
            252,
            222,
            228,
            221,
            24,
            251,
            70,
            170,
            98,
            198,
            145,
            205,
            210,
            150,
            247,
            148,
            15,
            244,
            177,
            161,
            215,
            232,
            73,
            254,
            52,
            242,
            243,
            221,
            160,
            76,
            99,
            179,
            115,
            110,
            198,
            119,
            43,
            184,
            204,
            230,
            157,
            26,
            206,
            188,
            194,
            38,
            139,
            237,
            209,
            197,
            230,
            111,
            103,
            231,
            52,
            81,
            130,
            127,
            4,
            110,
            29,
            60,
            214,
            60,
            205,
            213,
            137,
            128,
            245,
            175,
            251,
            184,
            206,
            3,
            214,
            99,
            109,
            193,
            229,
            102,
            10,
            93,
            72,
            226,
            166,
            185,
            83,
            176,
            150,
            74,
            172,
            67,
            253,
            4,
            185,
            155,
            78,
            246,
            125,
            140,
            195,
            213,
            185,
            77,
            102,
            69,
            173,
            214,
            218
        };

        private static readonly byte[] Salt = new byte[]
        {
            117,
            110,
            65,
            239,
            111,
            204,
            90,
            7,
            5,
            26,
            206,
            206,
            92,
            58,
            190,
            254,
            14,
            57,
            237,
            206,
            92,
            38,
            17,
            251,
            36,
            60,
            184,
            32,
            118,
            73,
            225,
            112,
            82,
            58,
            248,
            18,
            3,
            238,
            215,
            192,
            138,
            62,
            91,
            86,
            8,
            216,
            79,
            246,
            193,
            239,
            42,
            42,
            52,
            75,
            119,
            172,
            138,
            75,
            175,
            150,
            133,
            126,
            230,
            166,
            13,
            184,
            40,
            208,
            178,
            244,
            4,
            86,
            245,
            9,
            228,
            99,
            128,
            28,
            119,
            77,
            byte.MaxValue,
            62,
            1,
            92,
            80,
            145,
            192,
            204,
            3,
            6,
            5,
            195,
            121,
            28,
            43,
            72,
            145,
            77,
            142,
            46,
            177,
            249,
            16,
            113,
            206,
            98,
            94,
            181,
            51,
            15,
            226,
            48,
            137,
            166,
            211,
            194,
            174,
            113,
            130,
            200,
            208,
            134,
            60,
            246,
            175,
            63,
            156,
            59,
            178,
            56,
            95,
            75,
            145,
            19,
            66,
            131,
            63,
            220,
            45,
            19,
            246,
            132,
            236,
            245,
            23,
            6,
            12,
            54,
            128,
            245,
            232,
            155,
            244,
            125,
            125,
            48,
            34,
            253,
            188,
            32,
            202,
            222,
            154,
            88,
            222,
            187,
            123,
            75,
            89,
            254,
            87,
            67,
            127,
            21,
            132,
            187,
            254,
            111,
            76,
            110,
            59,
            44,
            233,
            193,
            128,
            238,
            79,
            142,
            146,
            208,
            196,
            110,
            75,
            89,
            143,
            32,
            155,
            27,
            30,
            68,
            179,
            56,
            240,
            196,
            242,
            15,
            24,
            38,
            46,
            188,
            167,
            142,
            5,
            82,
            168,
            209,
            140,
            130,
            220,
            66,
            7,
            219,
            147,
            226,
            205,
            213,
            119,
            220,
            190,
            43,
            235,
            212,
            143,
            87,
            123,
            85,
            221,
            46,
            195,
            171,
            120,
            125,
            135,
            48,
            129,
            40,
            89,
            47,
            143,
            3,
            72,
            94,
            191,
            177,
            155,
            112
        };

        private static int PBKDF2_ITERATIONS = 20480;

        private static string mUniqueId = null;
    }
}
