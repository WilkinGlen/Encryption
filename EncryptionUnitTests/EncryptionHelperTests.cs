namespace EncryptionUnitTests;

using EncryptionHelper;
using FluentAssertions;

public class EncryptionHelperTests
{
    [Theory]
    [InlineData("Glen Wilkin")]
    [InlineData("")]
    [InlineData("True")]
    [InlineData("1234567890")]
    [InlineData("public void CorrectlyEncryptsAndDecrypts(string? stringToEncrypt)\r\n{\r\nvar encryptedText = EncrytptionHelper.Encrypt(stringToEncrypt);\r\nvar actual = EncrytptionHelper.Decrypt(encryptedText);\r\n\r\n_ = actual.Should().Be(stringToEncrypt);\r\n}")]
    public void CorrectlyEncryptsAndDecrypts(string? stringToEncrypt)
    {
        var encryptedText = EncrytptionHelper.Encrypt(stringToEncrypt);
        var actual = EncrytptionHelper.Decrypt(encryptedText);

        _ = actual.Should().Be(stringToEncrypt);
    }
}
