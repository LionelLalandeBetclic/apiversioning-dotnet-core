using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq;

namespace Api.Conventions
{
    internal sealed class SwashbuckleRemoveVersionParametersOperationFilter : IOperationFilter
    {
        public void Apply(Operation operation, OperationFilterContext context)
        {
            var apiVersion = context?.ApiDescription?.GetApiVersion();
            if (apiVersion != null)
            {
                var versionParameter = operation?.Parameters?.SingleOrDefault(p => p.In == "path" && (p.Name == "version" || p.Name == "api-version"));
                if (versionParameter != null)
                {
                    operation.Parameters.Remove(versionParameter);
                }
                else
                {
                    var parameters = operation?.Parameters?.Where(p => p.In == "query");
                    foreach (var parameter in parameters)
                    {
                        parameter.Name = parameter.Name.ToSnakeCase();
                    }
                }
            }
        }
    }
}
