using AIC.CrossCutting.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace AIC.CrossCutting.Helpers
{
    public class CustomCryptography: ICustomCryptography
    {
        #region attributes
        private string _passPhrase;
        private string _saltValue;
        private string _hashAlgorithm;
        private int _passwordIterations;
        private string _initVector;
        private int _keySize;
        #endregion
        #region properties
        public string PassPhrase
        {
            get { return _passPhrase; }
            set { _passPhrase = value; }
        }
        public string SaltValue
        {
            get { return _saltValue; }
            set { _saltValue = value; }
        }
        public string HashAlgorithm
        {
            get { return _hashAlgorithm; }
            set { _hashAlgorithm = value; }
        }
        public int PasswordIterations
        {
            get { return _passwordIterations; }
            set { _passwordIterations = value; }
        }
        public string InitVector
        {
            get { return _initVector; }
            set { _initVector = value; }
        }
        public int KeySize
        {
            get { return _keySize; }
            set { _keySize = value; }
        }
        #endregion
        /// <summary>
        /// string passPhrase = "Pas5pr@se";        // can be any string
        /// string saltValue = "s@1tValue";         // can be any string
        /// string hashAlgorithm = "SHA1";          // can be "MD5"
        /// int passwordIterations = 2;             // can be any number
        /// string initVector = "@1B2c3D4e5F6g7H8"; // must be 16 bytes
        /// int keySize = 256;                      // can be 192 or 128
        /// </summary>
        public string Encrypt(string plainText)
        {
            _passPhrase = "Pas5pr@se";
            _saltValue = "s@1tValue";
            _hashAlgorithm = "SHA1";
            _passwordIterations = 2;
            _initVector = "@1B2c3D4e5F6g7H8";
            _keySize = 256;
            // Convert strings into byte arrays.
            // Let us assume that strings only contain ASCII codes.
            // If strings include Unicode characters, use Unicode, UTF7, or UTF8 
            // encoding.
            byte[] initVectorBytes = Encoding.ASCII.GetBytes(_initVector);
            byte[] saltValueBytes = Encoding.ASCII.GetBytes(_saltValue);

            // Convert our plaintext into a byte array.
            // Let us assume that plaintext contains UTF8-encoded characters.
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

            // First, we must create a password, from which the key will be derived.
            // This password will be generated from the specified passphrase and 
            // salt value. The password will be created using the specified hash 
            // algorithm. Password creation can be done in several iterations.
            PasswordDeriveBytes password = new PasswordDeriveBytes(
                                                            _passPhrase,
                                                            saltValueBytes,
                                                            _hashAlgorithm,
                                                            _passwordIterations);

            // Use the password to generate pseudo-random bytes for the encryption
            // key. Specify the size of the key in bytes (instead of bits).
            byte[] keyBytes = password.GetBytes(_keySize / 8);

            // Create uninitialized Rijndael encryption object.
            //RijndaelManaged symmetricKey = new RijndaelManaged();
            var symmetricKey = Aes.Create();

            // It is reasonable to set encryption mode to Cipher Block Chaining
            // (CBC). Use default options for other symmetric key parameters.
            symmetricKey.Mode = CipherMode.CFB;

            // Generate encryptor from the existing key bytes and initialization 
            // vector. Key size will be defined based on the number of the key 
            // bytes.
            ICryptoTransform encryptor = symmetricKey.CreateEncryptor(
                                                             keyBytes,
                                                             initVectorBytes);

            // Define memory stream which will be used to hold encrypted data.
            MemoryStream memoryStream = new MemoryStream();

            // Define cryptographic stream (always use Write mode for encryption).
            CryptoStream cryptoStream = new CryptoStream(memoryStream,
                                                         encryptor,
                                                         CryptoStreamMode.Write);
            // Start encrypting.
            cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);

            // Finish encrypting.
            cryptoStream.FlushFinalBlock();

            // Convert our encrypted data from a memory stream into a byte array.
            byte[] cipherTextBytes = memoryStream.ToArray();

            // Close both streams.
            memoryStream.Close();
            cryptoStream.Close();

            // Convert encrypted data into a base64-encoded string.
            string cipherText = Convert.ToBase64String(cipherTextBytes);
            // Return encrypted string.
            return cipherText.Replace("/", "slash");
        }
        public string Decrypt(string cipherText)
        {
            _passPhrase = "Pas5pr@se";
            _saltValue = "s@1tValue";
            _hashAlgorithm = "SHA1";
            _passwordIterations = 2;
            _initVector = "@1B2c3D4e5F6g7H8";
            _keySize = 256;
            // Convert strings defining encryption key characteristics into byte
            // arrays. Let us assume that strings only contain ASCII codes.
            // If strings include Unicode characters, use Unicode, UTF7, or UTF8
            // encoding.
            byte[] initVectorBytes = Encoding.ASCII.GetBytes(_initVector);
            byte[] saltValueBytes = Encoding.ASCII.GetBytes(_saltValue);

            // Convert our ciphertext into a byte array.
            cipherText = cipherText.Replace(' ', '+').Replace("slash", "/");
            byte[] cipherTextBytes = Convert.FromBase64String(cipherText);

            // First, we must create a password, from which the key will be 
            // derived. This password will be generated from the specified 
            // passphrase and salt value. The password will be created using
            // the specified hash algorithm. Password creation can be done in
            // several iterations.
            PasswordDeriveBytes password = new PasswordDeriveBytes(
                                                            _passPhrase,
                                                            saltValueBytes,
                                                            _hashAlgorithm,
                                                            _passwordIterations);

            // Use the password to generate pseudo-random bytes for the encryption
            // key. Specify the size of the key in bytes (instead of bits).
            byte[] keyBytes = password.GetBytes(_keySize / 8);

            // Create uninitialized Rijndael encryption object.
            //RijndaelManaged symmetricKey = new RijndaelManaged();
            var symmetricKey = Aes.Create();


            // It is reasonable to set encryption mode to Cipher Block Chaining
            // (CBC). Use default options for other symmetric key parameters.
            symmetricKey.Mode = CipherMode.CFB;

            // Generate decryptor from the existing key bytes and initialization 
            // vector. Key size will be defined based on the number of the key 
            // bytes.
            ICryptoTransform decryptor = symmetricKey.CreateDecryptor(
                                                             keyBytes,
                                                             initVectorBytes);

            // Define memory stream which will be used to hold encrypted data.
            MemoryStream memoryStream = new MemoryStream(cipherTextBytes);

            // Define cryptographic stream (always use Read mode for encryption).
            CryptoStream cryptoStream = new CryptoStream(memoryStream,
                                                          decryptor,
                                                          CryptoStreamMode.Read);

            // Since at this point we don't know what the size of decrypted data
            // will be, allocate the buffer long enough to hold ciphertext;
            // plaintext is never longer than ciphertext.
            byte[] plainTextBytes = new byte[cipherTextBytes.Length];

            // Start decrypting.
            int decryptedByteCount = cryptoStream.Read(plainTextBytes,
                                                       0,
                                                       plainTextBytes.Length);

            // Close both streams.
            memoryStream.Close();
            cryptoStream.Close();

            // Convert decrypted data into a string. 
            // Let us assume that the original plaintext string was UTF8-encoded.
            string plainText = Encoding.UTF8.GetString(plainTextBytes,
                                                       0,
                                                       decryptedByteCount);//?.Replace("\t", "");

            // Return decrypted string.   
            return plainText;
        }
    }
}
