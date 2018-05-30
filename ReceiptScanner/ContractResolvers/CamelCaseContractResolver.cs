using Newtonsoft.Json.Serialization;
using System;

namespace ReceiptScanner.ContractResolvers
{
    public class CamelCaseContractResolver : DefaultContractResolver
    {
        protected override string ResolvePropertyName(string propertyName)
        {
            return Char.ToLowerInvariant(propertyName[0]) + propertyName.Substring(1);
        }
    }
}