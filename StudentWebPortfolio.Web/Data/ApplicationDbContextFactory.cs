using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using StudentWebPortfolio.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace StudentWebPortfolio.Web.Data
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "..", "secretsettings.json"), optional: true)
                .Build();

            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            builder.UseSqlServer(config.GetConnectionString("DefaultConnection"));

            return new ApplicationDbContext(builder.Options);
        }
    }
}
