using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace HSBCReward.AppCode.Helpers
{
    public class HSBCSecurity
    {
        private static byte[] sERMPrimaryKey = { 88, 5, 56, 34, 16, 9, 43, 67, 10, 32, 55, 87, 45, 65, 83, 49 };
        private static byte[] sERMSecondaryKey = { 12, 34, 98, 90, 23, 56, 81, 90, 53, 33, 5, 65, 43, 25, 8, 71 };

        //Private key for string encryption general
        private static byte[] sERMPrimaryKeyG = { 7, 5, 56, 34, 4, 36, 23, 67, 76, 56, 55, 34, 45, 65, 87, 92 };
        private static byte[] sERMSecondaryKeyG = { 78, 34, 98, 90, 23, 16, 91, 90, 53, 33, 5, 65, 43, 83, 8, 71 };


        private static Byte[] ERMPrimaryKey
        {
            get { return sERMPrimaryKey; }
        }
        private static Byte[] ERMSecondaryKey
        {
            get { return sERMSecondaryKey; }
        }
        private static Byte[] ERMPrimaryKeyG
        {
            get { return sERMPrimaryKeyG; }
        }
        private static Byte[] ERMSecondaryKeyG
        {
            get { return sERMSecondaryKeyG; }
        }
        public static  string ERMEncryptString(string sTextValue)
        {
            string sEncryptedString = String.Empty;

            try
            {
                System.Security.Cryptography.RijndaelManaged crptManagedObject = new RijndaelManaged();

                if (sTextValue != string.Empty)
                {
                    MemoryStream msMemoryStream = new MemoryStream();
                    CryptoStream csCryptoStream = new CryptoStream(msMemoryStream, crptManagedObject.CreateEncryptor(sERMPrimaryKey, ERMSecondaryKey), CryptoStreamMode.Write);
                    StreamWriter swStreamWriter = new StreamWriter(csCryptoStream);
                    swStreamWriter.Write(sTextValue);
                    swStreamWriter.Flush();
                    csCryptoStream.FlushFinalBlock();
                    msMemoryStream.Flush();
                    sEncryptedString = Convert.ToBase64String(msMemoryStream.GetBuffer(), 0, (int)msMemoryStream.Length);
                }
                else
                {
                    sEncryptedString = string.Empty;
                }
            }
            catch (Exception Ex)
            {
                throw new Exception("ERMEncryptString", Ex);
            }

            return sEncryptedString;
        }


        public static string ERMDecryptString(string sEncryptedString)
        {
            string sDecryptedString = String.Empty;

            try
            {
                System.Security.Cryptography.RijndaelManaged cryptObj = new RijndaelManaged();
                if (sEncryptedString != string.Empty)
                {
                    byte[] bufBuffer = Convert.FromBase64String(sEncryptedString.Trim());
                    MemoryStream msMemoryStream = new MemoryStream(bufBuffer);
                    msMemoryStream.Position = 0;
                    CryptoStream csCryptoStream = new CryptoStream(msMemoryStream, cryptObj.CreateDecryptor(ERMPrimaryKey, ERMSecondaryKey), CryptoStreamMode.Read);
                    StreamReader swStreamWriter = new StreamReader(csCryptoStream);
                    sDecryptedString = swStreamWriter.ReadToEnd();
                }
                else
                    sDecryptedString = String.Empty;
            }
            catch (Exception Ex)
            {
                throw new Exception("ERMDecryptString", Ex);
            }

            // Return the decrypted text
            return sDecryptedString;
        }


        public static string ERMEncryptStringGeneral(string sTextValue)
        {
            string sEncryptedString = String.Empty;

            try
            {
                System.Security.Cryptography.RijndaelManaged crptManagedObject = new RijndaelManaged();
                if (sTextValue != string.Empty)
                {
                    MemoryStream msMemoryStream = new MemoryStream();
                    CryptoStream csCryptoStream = new CryptoStream(msMemoryStream, crptManagedObject.CreateEncryptor(ERMPrimaryKeyG, ERMSecondaryKeyG), CryptoStreamMode.Write);
                    StreamWriter swStreamWriter = new StreamWriter(csCryptoStream);
                    swStreamWriter.Write(sTextValue);
                    swStreamWriter.Flush();
                    csCryptoStream.FlushFinalBlock();
                    msMemoryStream.Flush();
                    sEncryptedString = Convert.ToBase64String(msMemoryStream.GetBuffer(), 0, (int)msMemoryStream.Length);
                }
                else
                {
                    sEncryptedString = string.Empty;
                }
            }
            catch (Exception Ex)
            {
                throw new Exception("ERMEncryptStringGeneral", Ex);
            }

            return sEncryptedString.Replace("+", "^^^^^");
        }


        public static string ERMDecryptStringGeneral(string sEncryptedString)
        {
            string sDecryptedString = String.Empty;

            sEncryptedString = sEncryptedString.Replace("^^^^^", "+");
            try
            {
                System.Security.Cryptography.RijndaelManaged cryptObj = new RijndaelManaged();
                cryptObj.Padding = PaddingMode.PKCS7; // Ensure correct padding mode

                if (sEncryptedString != string.Empty)
                {
                    byte[] bufBuffer = Convert.FromBase64String(sEncryptedString.Trim());
                    MemoryStream msMemoryStream = new MemoryStream(bufBuffer);
                    msMemoryStream.Position = 0;
                    CryptoStream csCryptoStream = new CryptoStream(msMemoryStream, cryptObj.CreateDecryptor(ERMPrimaryKeyG, ERMSecondaryKeyG), CryptoStreamMode.Read);
                    StreamReader swStreamWriter = new StreamReader(csCryptoStream);
                    sDecryptedString = swStreamWriter.ReadToEnd();
                }
                else
                    sDecryptedString = String.Empty;
            }
            catch (Exception Ex)
            {
                throw new Exception("ERMDecryptStringGeneral", Ex);
            }

            // Return the decrypted text
            return sDecryptedString;
        }

        public static String GeneratePassword(int iLength)
        {
            //int minPassSize = 6;
            //int maxPassSize = 12;
            StringBuilder stringBuilder = new StringBuilder();
            char[] charArray = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%^&*()".ToCharArray();
            //int newPassLength = new Random().Next(minPassSize, maxPassSize);
            char character;
            Random rnd = new Random(DateTime.Now.Millisecond);
            for (int i = 0; i < iLength; i++)
            {
                character = charArray[rnd.Next(0, (charArray.Length - 1))];
                stringBuilder.Append(character);
            }
            return stringBuilder.ToString();
        }


    }
}