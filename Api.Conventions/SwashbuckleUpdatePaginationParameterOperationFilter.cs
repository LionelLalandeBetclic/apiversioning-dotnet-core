using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq;

namespace Api.Conventions
{
    internal sealed class SwashbuckleUpdatePaginationParameterOperationFilter : IOperationFilter
    {
        public SwashbuckleUpdatePaginationParameterOperationFilter(IOptions<PaginationOptions> options) =>
            _options = options.Value;

        public void Apply(Operation operation, OperationFilterContext context)
        {
            var parameters = context.MethodInfo.GetParameters();
            foreach (var parameter in parameters)
            {
                if (parameter.ParameterType == typeof(Pagination))
                {
                    operation.Parameters.Remove(operation.Parameters.Single(p => p.Name == parameter.Name));
                    context.SchemaRegistry.Definitions.Remove("Pagination");

                    operation.Parameters.Add(new NonBodyParameter
                    {
                        Type = "int",
                        In = "query",
                        Name = "offset",
                        Description = "Pagination: offset of first item to return (default value: 0)",
                        Required = false,
                    });
                    operation.Parameters.Add(new NonBodyParameter
                    {
                        Type = "int",
                        In = "query",
                        Name = "limit",
                        Description = $"Pagination: maximum number of items to return (default value: {_options.DefaultLimit})",
                        Required = false,
                    });
                }
            }
        }

        private readonly PaginationOptions _options;
    }
}
