using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace AutoLogin
{
    public class Crypto
    {
        // Bad style, but set the IV and salt static
        string IVstring = "gk&LZhY$FWEnlZvZHRya1*EE0)H9mZmktHtuMkl(b%w_CnfjqpJwwHcrL&%20(hq";
        byte[] salt = Encoding.UTF8.GetBytes("uzjz7sXV3prD1rE^wy64rT!Nn8c#btM5pOL%OlbJ5p-M#Hg@L%1vu0i4SEZn@hrW");

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2202:Do not dispose objects multiple times")]
        public string Encrypt(byte[] plainTextBytes, string pwd)
        {
            // Convert the IV and password string to bytes
            Rfc2898DeriveBytes IV = new Rfc2898DeriveBytes(IVstring, salt, 10);
            Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(pwd, salt, 10);

            // Create the aes crypto provider and set the Key and IV bytes
            AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
            aes.Key = key.GetBytes(16);
            aes.IV = IV.GetBytes(16);

            // Convert the plain text to bytes
            //byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

            // Create the memory stream that the crypto stream will send data to
            MemoryStream msEncrypt = new MemoryStream();

            // Create the crypto stream used to do the encryption
            CryptoStream csEncrypt = new CryptoStream(msEncrypt, aes.CreateEncryptor(), CryptoStreamMode.Write);
            
            // Encrypt the bytes from the plain text and clean up
            csEncrypt.Write(plainTextBytes, 0, plainTextBytes.Length);
            csEncrypt.FlushFinalBlock();
            csEncrypt.Close();

            // Save the encrypted bytes to a byte array and clean up
            byte[] encrypted = msEncrypt.ToArray();
            msEncrypt.Close();

            // return a string from the encrypted byte array
            return Convert.ToBase64String(encrypted);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2202:Do not dispose objects multiple times")]
        public MemoryStream Decrypt(string cryptoText, string pwd)
        {
            // Continue only if the string has data
            if (cryptoText.Length > 0)
            {
                try
                {
                    // Convert the IV and password string to bytes
                    Rfc2898DeriveBytes IV = new Rfc2898DeriveBytes(IVstring, salt, 10);
                    Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(pwd, salt, 10);

                    // Create the aes crypto provider and set the Key and IV bytes
                    AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
                    aes.Key = key.GetBytes(16);
                    aes.IV = IV.GetBytes(16);

                    // Convert the encrypted text to bytes
                    byte[] cryptoTextBytes = Convert.FromBase64String(cryptoText);

                    // Create the memory stream that the crypto stream will send data to
                    MemoryStream msDecrypt = new MemoryStream(cryptoTextBytes);

                    // Create the crypto stream used to do the decryption
                    CryptoStream csDecrypt = new CryptoStream(msDecrypt, aes.CreateDecryptor(), CryptoStreamMode.Read);

                    // Create a byte array to hold the decrypted plain text bytes
                    byte[] plainTextBytes = new byte[cryptoTextBytes.Length];

                    // Decrypt the encrypted text bytes and clean up
                    int count = csDecrypt.Read(plainTextBytes, 0, plainTextBytes.Length);
                    msDecrypt.Close();
                    csDecrypt.Close();

                    // Convert the decrypted plain text bytes to a string
                    string decrypted = Encoding.UTF8.GetString(plainTextBytes, 0, count);

                    return new MemoryStream(plainTextBytes);
                }
                catch (Exception) { }
            }
            return null;
        }
    }
}