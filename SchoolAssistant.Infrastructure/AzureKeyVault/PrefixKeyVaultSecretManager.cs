using Azure.Extensions.AspNetCore.Configuration.Secrets;
using Azure.Security.KeyVault.Secrets;
using Microsoft.Extensions.Configuration;

namespace SchoolAssistant.Infrastructure.AzureKeyVault
{
    public class PrefixKeyVaultSecretManager : KeyVaultSecretManager
    {
        private readonly string _prefix;

        public PrefixKeyVaultSecretManager(string prefix)
        {
            _prefix = $"{prefix}-";
        }

        public override bool Load(SecretProperties secret)
        {
            return secret.Name.StartsWith(_prefix, StringComparison.InvariantCulture);
        }

        public override string GetKey(KeyVaultSecret secret)
        {
            return secret.Name[_prefix.Length..]
                .Replace("--", ConfigurationPath.KeyDelimiter, StringComparison.InvariantCulture);
        }
    }
}
