using System;
using System.Collections.Generic;
using System.Text;

namespace AIC.CrossCutting.Interfaces
{
    public interface ICustomCryptography
    {
        string Encrypt(string plainText);
        string Decrypt(string cipherText);
    }
}
