using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq;

namespace Api.Conventions
{
    internal sealed class SwashbuckleVersionInPathsDocumentFilter : IDocumentFilter
    {
        public void Apply(SwaggerDocument swaggerDoc, DocumentFilterContext context)
        {
            swaggerDoc.Paths = swaggerDoc.Paths.ToDictionary(
                (path) => path.Key.Replace("v{version}", $"v{swaggerDoc.Info.Version}"),
                (path) => path.Value);
        }
    }
}
