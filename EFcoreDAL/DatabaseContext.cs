using EFmodels;
using Microsoft.EntityFrameworkCore;
namespace EFcoreDAL
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }

        public DbSet<UserModel> UserTable { get; set; }
        public DbSet<RoleModel> RoleTable { get; set; }
        public DbSet<UserWithRoleModel> UserRoleTable { get; set; }
        public DbSet<MenuModel> MenuTable { get; set; }
        public DbSet<UserMenuModel> UserMenuTable { get; set; }
        public DbSet<CategoryModel> CategoryTable { get; set; }
        public DbSet<PostModel> PostTable { get; set; }
        public DbSet<APostCategoryModel> PostCategoryTable { get; set; }
        public DbSet<APostWithMultipleImageModel> MultipleImagePostTable { get; set; }
        public DbSet<MultipleImageData> MultipleImageDataTable { get; set; }

        // Fluent Api
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // modelBuilder.Entity<UserModel>().HasKey(p => p.UserID);
            // modelBuilder.Entity<UserWithRoleModel>().HasNoKey();
            // modelBuilder.Entity<UserMenuModel>().HasNoKey();
        }
    }
}