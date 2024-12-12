namespace EncryptionHelper;

using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

public static class EncrytptionHelper
{
    private static readonly byte[] Key = Encoding.UTF8.GetBytes("Your32CharSecureKeyHere123456789"); // Replace with a secure key
    private static readonly byte[] IV = Encoding.UTF8.GetBytes("Your16CharIVHere"); // Replace with a secure IV

    public static string Encrypt(string? plainText)
    {
        using var aes = Aes.Create();
        aes.Key = Key;
        aes.IV = IV;
        var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

        using var ms = new MemoryStream();
        using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
        {
            using var writer = new StreamWriter(cs);
            writer.Write(plainText);
        }

        return Convert.ToBase64String(ms.ToArray());
    }

    public static string Decrypt(string? cipherText)
    {
        using var aes = Aes.Create();
        aes.Key = Key;
        aes.IV = IV;
        var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

        using var ms = new MemoryStream(Convert.FromBase64String(cipherText!));
        using var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
        using var reader = new StreamReader(cs);
        return reader.ReadToEnd();
    }
}
