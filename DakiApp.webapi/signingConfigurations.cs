using Microsoft.IdentityModel.Tokens;

namespace DakiApp.webapi
{
    public class signingConfigurations
    {
        public SecurityKey Key { get; set; }
        public SigningCredentials SigningCredentials { get; set; }

        public signingConfigurations()
        {
            using (var provider = new System.Security.Cryptography.RSACryptoServiceProvider(2048))
            {
                Key = new RsaSecurityKey(provider.ExportParameters(true));
            }

            SigningCredentials = new SigningCredentials(Key, SecurityAlgorithms.RsaSha256Signature);
        }
    }
}