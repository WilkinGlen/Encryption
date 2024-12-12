namespace EncryptionUnitTests;

using EncryptionHelper;
using FluentAssertions;
using Microsoft.Extensions.Configuration;

public class EncryptionHelperTests
{
    private readonly IConfiguration configuration;
    private readonly EncryptionHelper encryptionHelper;

    public EncryptionHelperTests()
    {
        this.configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile(@"appsettings.test.json", false, true)
            .AddEnvironmentVariables()
            .Build();
        this.encryptionHelper = new(this.configuration);
    }

    [Theory]
    [InlineData("Glen Wilkin")]
    [InlineData("")]
    [InlineData("True")]
    [InlineData("1234567890")]
    [InlineData("public void CorrectlyEncryptsAndDecrypts(string? stringToEncrypt)\r\n{\r\nvar encryptedText = EncrytptionHelper.Encrypt(stringToEncrypt);\r\nvar actual = EncrytptionHelper.Decrypt(encryptedText);\r\n\r\n_ = actual.Should().Be(stringToEncrypt);\r\n}")]
    public void CorrectlyEncryptsAndDecrypts(string? stringToEncrypt)
    {
        var encryptedText = this.encryptionHelper.Encrypt(stringToEncrypt);
        var actual = this.encryptionHelper.Decrypt(encryptedText);

        _ = actual.Should().Be(stringToEncrypt);
    }
}
