using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using THTDotNetCore.ConsoleApp.Models;

namespace THTDotNetCore.ConsoleApp.EFCoreExamples
{
    public class AppDbContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            SqlConnectionStringBuilder _sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
            {
                DataSource = ".",
                InitialCatalog = "THARGYI",
                UserID = "sa",
                Password = "thargyi",
                TrustServerCertificate = true,
            };
            optionsBuilder.UseSqlServer(_sqlConnectionStringBuilder.ConnectionString);
        }

        public DbSet<BlogDataModel> Blogs { get; set; }
    }
}
