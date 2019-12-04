using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Serialization;

namespace Api.Conventions
{
    public static class MvcJsonOptionsExtensions
    {
        public static void UseSnakeCase(this MvcJsonOptions options)
        {
            options.SerializerSettings.ContractResolver = new DefaultContractResolver
            {
                NamingStrategy = new SnakeCaseNamingStrategy(true, true, true),
            };
        }
    }
}
