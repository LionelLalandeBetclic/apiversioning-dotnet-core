using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace VersioningWithConventions
{
    /// <summary>
    /// Represents the application bootstrap.
    /// </summary>
    public sealed class Startup
    {
        private readonly IConfiguration _configuration;
        private readonly IHostingEnvironment _environment;

        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <param name="environment">The environment.</param>
        public Startup(IConfiguration configuration, IHostingEnvironment environment)
        {
            _configuration = configuration;
            _environment = environment;
        }

        /// <summary>
        /// Configures the application services.
        /// </summary>
        /// <param name="services">The services.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            // Warning: Set compatibility version to 2.1 in .Net Core 2.2!
            services.AddMvcCore()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddApiExplorer()
                .AddDataAnnotations()
                .AddJsonFormatters()
                .AddVersionedApiExplorer();
            services.AddApiVersioning();
            services.AddSwaggerGen();

            // TODO DON'T prefix with "api" if base URL already contains it in Development, Staging, Production environments!
            services.ConfigureConventions(useApiPrefix: true);
        }

        /// <summary>
        /// Configures the application pipeline.
        /// </summary>
        /// <param name="app">The application.</param>
        public void Configure(IApplicationBuilder app)
        {
            if (_environment.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUI();
        }
    }
}
