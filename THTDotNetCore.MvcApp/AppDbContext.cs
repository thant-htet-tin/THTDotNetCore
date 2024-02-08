using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using THTDotNetCore.MvcApp.Models;


namespace THTDotNetCore.MvcApp
{
    public class AppDbContext : DbContext
    {
      
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options) {
            
        }

        public DbSet<LoginDataModel> Users { get; set; }
    }
}
