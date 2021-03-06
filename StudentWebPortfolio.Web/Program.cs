﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using StudentWebPortfolio.Data;
using StudentWebPortfolio.Web.Data;
using StudentWebPortfolio.Web.Managers;

namespace StudentWebPortfolio.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    var context = services.GetRequiredService<ApplicationDbContext>();
                    var userManager = services.GetRequiredService<UserManager>();
                    var roleManager = services.GetRequiredService<RoleManager>();
                    ApplicationDbInitializer.Initialize(context, userManager, roleManager);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred creating the DB.");
                }
            }

            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
            => WebHost.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    var env = hostingContext.HostingEnvironment;
                    var sharedFolder = Path.Combine(env.ContentRootPath, "..");

                    config
                        .AddJsonFile(Path.Combine(sharedFolder, "secretsettings.json"), optional: true) // When running using dotnet run
                        .AddJsonFile("secretsettings.json", optional: true); // When app is published
                })
                .UseStartup<Startup>();
    }
}
