using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace QuickZ.Persistent.BaseImpl.Security
{
    // Custom Encryption Method
    public class CEM

    {
        private static readonly Encoding encoding = Encoding.Unicode;

        public static string Encrypt(string input)
        {
            Byte[] stringBytes = encoding.GetBytes(input);
            StringBuilder sbBytes = new StringBuilder(stringBytes.Length * 2);
            try
            {

                foreach (byte b in stringBytes)
                {
                    sbBytes.AppendFormat("{0:X2}", b);
                }

                return sbBytes.ToString();
            }
            catch (Exception err)
            {

                return err.Message;
            }
        }

        public static string Decrypt(string input)
        {
            int numberChars = input.Length;
            byte[] bytes = new byte[numberChars / 2];
            try
            {

                for (int i = 0; i < numberChars; i += 2)
                {
                    bytes[i / 2] = Convert.ToByte(input.Substring(i, 2), 16);
                }
                return encoding.GetString(bytes);
            }
            catch (Exception err)
            {
                return err.Message;

            }
        }

    }
}