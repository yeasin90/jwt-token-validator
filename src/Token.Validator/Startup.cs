using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Token.Validator.Configurations;
using Token.Validator.Services;

namespace Token.Validator
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.Configure<AuthorizationServerConfig>(Configuration.GetSection(nameof(AuthorizationServerConfig)));

            services.AddScoped<IConfigurationManager<OpenIdConnectConfiguration>, CustomOpenIdConfigurationManager>();

            services.AddScoped<IAuthorizationService, AuthorizationService>(opt =>
            {
                var openIdConfig = opt.GetRequiredService<IConfigurationManager<OpenIdConnectConfiguration>>();
                var authConfig = opt.GetService<IOptions<AuthorizationServerConfig>>();
                var obj = new AuthorizationService(authConfig)
                {
                    SigningKeys = openIdConfig.GetConfigurationAsync(CancellationToken.None).Result.SigningKeys
                };
                return obj;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
