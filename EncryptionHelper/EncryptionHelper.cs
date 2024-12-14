namespace EncryptionHelper;

using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

public class EncryptionHelper(IConfiguration configuration)
{
    private readonly byte[] Key = Encoding.UTF8.GetBytes(configuration["EncryptionKey"]!);
    private readonly byte[] Iv = Encoding.UTF8.GetBytes(configuration["EncryptionIv"]!);

    private ICryptoTransform? encryptor;
    private ICryptoTransform? decryptor;
    private ICryptoTransform Encryptor
    {
        get
        {
            if (this.encryptor == null)
            {
                using var aes = Aes.Create();
                aes.Key = this.Key;
                aes.IV = this.Iv;
                this.encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
            }

            return this.encryptor;
        }
    }

    private ICryptoTransform Decryptor
    {
        get
        {
            if (this.decryptor == null)
            {
                using var aes = Aes.Create();
                aes.Key = this.Key;
                aes.IV = this.Iv;
                this.decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
            }

            return this.decryptor;
        }
    }

    public string Encrypt(string? plainText)
    {
        using var ms = new MemoryStream();
        using (var cs = new CryptoStream(ms, this.Encryptor, CryptoStreamMode.Write))
        {
            using var writer = new StreamWriter(cs);
            writer.Write(plainText);
        }

        return Convert.ToBase64String(ms.ToArray());
    }

    public string Decrypt(string? cipherText)
    {
        using var ms = new MemoryStream(Convert.FromBase64String(cipherText!));
        using var cs = new CryptoStream(ms, this.Decryptor, CryptoStreamMode.Read);
        using var reader = new StreamReader(cs);
        return reader.ReadToEnd();
    }
}
