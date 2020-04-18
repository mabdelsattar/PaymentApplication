using System;
using System.IO;
using System.Security.Cryptography;

namespace Payment.Business.Services
{
    public interface IEncryptionService
    {
        string Encrypt(string source, string key);
    }
}
