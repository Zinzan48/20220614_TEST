using Microsoft.EntityFrameworkCore;

namespace _20220614_TEST.Models
{
    // 繼承 DbContext
    public class DataContext : DbContext
    {
        // 建構子參數 DbContextOptions<T> 並傳入父類別
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<tblSignupItem>()
       .HasKey(c => new { c.cItemID, c.cMobile });
        }

        // 一般習慣在 Model 命名使用單數，這邊資料表命名使用複數
        public DbSet<tblSignup> tblSignup { get; set; }
        public DbSet<tblSignupItem> tblSignupItem { get; set; }
        public DbSet<tblActiveItem> tblActiveItem { get; set; }
    }
}
