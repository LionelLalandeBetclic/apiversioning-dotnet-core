using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using Api.Conventions;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Formatters;
using System.IO;
using System.Reflection;

#if NETCOREAPP2_1 || NETCOREAPP2_2
using Newtonsoft.Json;
#else
using Microsoft.OpenApi.Models;
#endif

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ConventionsServiceCollectionExtensions
    {
        public static IServiceCollection UsePagination(this IServiceCollection services, Action<PaginationOptions> configureDelegate)
        {
            return services.Configure<MvcOptions>((options) =>
                options.ModelBinderProviders.Insert(0, new PaginationModelBinderProvider(configureDelegate)));
        }

        public static IServiceCollection ConfigureConventions(this IServiceCollection services, bool useApiPrefix = false) =>
            services
                .UsePagination((options) => options.DefaultLimit = 10)
                .ConfigureVersioning()
                .ConfigureRoutePrefixes(useApiPrefix)
                .ConfigureNaming()
                .ConfigureOpenApi();
        // TODO more configuration to check
        ////services.Configure<ApiBehaviorOptions>((options) =>
        ////{
        ////    //options.
        ////});
        ////services.Configure<ApiExplorerOptions>((options) =>
        ////{
        ////    options.
        ////});

        public static IServiceCollection ConfigureVersioning(this IServiceCollection services) =>
             services.Configure<ApiVersioningOptions>((options) =>
             {
                 options.ReportApiVersions = true;
                 options.AssumeDefaultVersionWhenUnspecified = true; // or false?

                 // Choose only 1 option...
                 options.ApiVersionReader = ApiVersionReader.Combine(
                     new QueryStringApiVersionReader("api_version"),
                     new HeaderApiVersionReader("x-api-version"),
                     new MediaTypeApiVersionReader(),
                     new UrlSegmentApiVersionReader());

                 // Used for migrating for non-versioning APIs to versioning APIs!
                 options.DefaultApiVersion = new ApiVersion(1, 0);
             });

        public static IServiceCollection ConfigureRoutePrefixes(this IServiceCollection services, bool useApiPrefix = false) =>
            services.Configure<MvcOptions>((options) =>
            {
                options.Conventions.Add(new ApiVersionPrefixConvention());
                if (useApiPrefix)
                    options.Conventions.Add(new ApiPrefixConvention());
            });

        public static IServiceCollection ConfigureNaming(this IServiceCollection services) =>
            services
#if NETCOREAPP2_1 || NETCOREAPP2_2
                .Configure<MvcJsonOptions>((options) =>
                {
                    // Do not serialize NULL values
                    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;

                    options.UseSnakeCase();
                })
#endif
                .Configure<MvcOptions>((options) =>
                {
                    options.Conventions.Add(new KebabCaseControllerNameConvention());
                    options.Conventions.Add(new SnakeCaseQueryStringNameConvention());
                    options.Conventions.Add(new PaginationParameterConvention());
                });

        public static IServiceCollection ConfigureOpenApi(this IServiceCollection services)
        {
            var apiVersions = services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();
            return services
                .Configure<SwaggerGenOptions>((options) =>
                {
                    options.DocumentFilter<SwashbuckleVersionInPathsDocumentFilter>();
                    options.OperationFilter<SwashbuckleRemoveVersionParametersOperationFilter>();
                    options.OperationFilter<SwashbuckleUpdatePaginationParameterOperationFilter>();

#if NETCOREAPP2_1 || NETCOREAPP2_2
                    options.DescribeAllEnumsAsStrings(); // Enums are in PascalCase!
#endif
                    foreach (var desc in apiVersions.ApiVersionDescriptions)
                    {
#if NETCOREAPP2_1 || NETCOREAPP2_2
                        options.SwaggerDoc(desc.GroupName, new Info { Version = desc.ApiVersion.ToString(), Title = $"My API v{desc.ApiVersion}" });
#else
                        options.SwaggerDoc(desc.GroupName, new OpenApiInfo { Version = desc.ApiVersion.ToString(), Title = $"My API v{desc.ApiVersion}" });
#endif
                        var xmlFile = Path.ChangeExtension(Assembly.GetEntryAssembly().Location, ".xml");
                        options.IncludeXmlComments(xmlFile);
                    }
                })
                .Configure<SwaggerUIOptions>((options) =>
                {
                    options.DocumentTitle = "My Title";
                    foreach (var desc in apiVersions.ApiVersionDescriptions.Reverse())
                    {
                        var url = string.Format("/swagger/{0}/swagger.json", desc.GroupName);
                        var description = string.Format("Version {0}", desc.GroupName);
                        options.SwaggerEndpoint(url, description);
                    }
                });
        }
    }
}
