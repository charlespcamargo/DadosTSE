using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Security.Cryptography;

namespace UFSCar.BD.Commom.Util.Captcha
{
    public class Encrypting
    {


        private TripleDESCryptoServiceProvider TripleDes = new TripleDESCryptoServiceProvider();
        public Encrypting(string key)
        {
            // Initialize the crypto provider.
            TripleDes.Key = TruncateHash(key, TripleDes.KeySize / 8);
            TripleDes.IV = TruncateHash("", TripleDes.BlockSize / 8);
        }
        private byte[] TruncateHash(string key, int length)
        {

            SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider();

            // Hash the key. 
            byte[] keyBytes = System.Text.Encoding.Unicode.GetBytes(key);
            byte[] hash = sha1.ComputeHash(keyBytes);

            // Truncate or pad the hash. 
            Array.Resize(ref hash, length);
            return hash;
        }


        public string EncryptData(string plaintext)
        {

            // Convert the plaintext string to a byte array. 
            byte[] plaintextBytes = System.Text.Encoding.Unicode.GetBytes(plaintext);

            // Create the stream. 
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            // Create the encoder to write to the stream. 
            CryptoStream encStream = new CryptoStream(ms, TripleDes.CreateEncryptor(), System.Security.Cryptography.CryptoStreamMode.Write);

            // Use the crypto stream to write the byte array to the stream.
            encStream.Write(plaintextBytes, 0, plaintextBytes.Length);
            encStream.FlushFinalBlock();

            // Convert the encrypted stream to a printable string. 
            return Convert.ToBase64String(ms.ToArray());
        }

        public string DecryptData(string encryptedtext)
        {

            // Convert the encrypted text string to a byte array. 
            byte[] encryptedBytes = Convert.FromBase64String(encryptedtext);

            // Create the stream. 
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            // Create the decoder to write to the stream. 
            CryptoStream decStream = new CryptoStream(ms, TripleDes.CreateDecryptor(), System.Security.Cryptography.CryptoStreamMode.Write);

            // Use the crypto stream to write the byte array to the stream.
            decStream.Write(encryptedBytes, 0, encryptedBytes.Length);
            decStream.FlushFinalBlock();

            // Convert the plaintext stream to a string. 
            return System.Text.Encoding.Unicode.GetString(ms.ToArray());
        }
    }
}


