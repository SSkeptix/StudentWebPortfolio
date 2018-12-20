using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StudentWebPortfolio.Data;
using StudentWebPortfolio.Data.Entities;

[assembly: HostingStartup(typeof(StudentWebPortfolio.Web.Areas.Identity.IdentityHostingStartup))]
namespace StudentWebPortfolio.Web.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}