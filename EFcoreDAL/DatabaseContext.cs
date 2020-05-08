using EFmodels;
using Microsoft.EntityFrameworkCore;
namespace EFcoreDAL
{
    public class DatabaseContext : DbContext
    {
        //static readonly IConfiguration _configuration;
        // public DatabaseContext() : base(_configuration["ConnectionString:DbConnectionString"])
        // {

        // }
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }   
        
        public DbSet<UserModel> UserTable { get; set; }
        public DbSet<RoleModel> RoleTable { get; set; }
        
        // Fluent Api
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // modelBuilder.Entity<UserModel>().HasKey(p => p.UserID);
        }
    }
}