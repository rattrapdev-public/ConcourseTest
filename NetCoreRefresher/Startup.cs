using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NetCoreLibrary.Application;
using NetCoreLibrary.Domain.Organizations;
using NetCoreLibrary.Domain.Users;
using NetCoreLibrary.Infrastructure;
using NetCoreRefresher.Authorization;
using NetCoreRefresher.Config;

namespace NetCoreRefresher
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = ApiKeyAuthenticationOptions.DefaultScheme;
                    options.DefaultChallengeScheme = ApiKeyAuthenticationOptions.DefaultScheme;
                }).AddApiKeySupport(options => { });

            services.AddAuthorization(options =>
            {
                options.AddPolicy(AdminApiKeyRequirement.AdminApiRequirement, policy => policy.Requirements.Add(new AdminApiKeyRequirement()));
            });

            services.AddSingleton<IAuthorizationHandler, AdminApiKeyAuthorizationHandler>();
            
            services.AddDbContext<NetCoreLibraryDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("NetCoreDatabase")));
            services.AddScoped<IOrganizationRepository, OrganizationSqlRepository>();
            services.AddScoped<IUserRepository, UserSqlRepository>();
            services.AddScoped<IOrganizationService, OrganizationService>();
            services.Configure<ApiKeys>(options => Configuration.GetSection("ApiKeys").Bind(options));
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<NetCoreLibraryDbContext>();
                context.Database.EnsureCreated();
            }
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(errorApp =>
                {
                    errorApp.Run(async context =>
                    {
                        var exceptionHandlerPathFeature = 
                            context.Features.Get<IExceptionHandlerPathFeature>();
                        
                        var loggerFactory = errorApp.ApplicationServices.GetService<ILoggerFactory>();
                        var logger = loggerFactory.CreateLogger("exceptionLogger");
                        
                        logger.LogError(exceptionHandlerPathFeature.Error, $"An unexpected error occurred while processing {context.Request.GetDisplayUrl()}.");
                        
                        context.Response.StatusCode = 500;
                        context.Response.ContentType = "text/html";

                        await context.Response.WriteAsync("<html lang=\"en\"><body>\r\n");
                        await context.Response.WriteAsync($"{exceptionHandlerPathFeature.Error.Message}<br><br>\r\n");
                        await context.Response.WriteAsync($"{exceptionHandlerPathFeature.Error.StackTrace}<br><br>\r\n");

                        await context.Response.WriteAsync("<a href=\"/\">Home</a><br>\r\n");
                        await context.Response.WriteAsync("</body></html>\r\n");
                        await context.Response.WriteAsync(new string(' ', 512)); // IE padding
                    });
                });
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
                });
        }
    }
}