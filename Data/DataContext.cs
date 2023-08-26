
// using System.Text.RegularExpressions;
using livechat.signalr.models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace livechat.signalr.Data
{
    public class DataContext : DbContext
    {

        public DbSet<User>? Users {get; set;}
        public DbSet<Group>? Groups {get; set;}
        public DbSet<Group_User>? Group_Users {get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // set up many-to-many relation
            modelBuilder.Entity<Group_User>().HasKey(gu => new { gu.GroupName, gu.UserName});
            modelBuilder.Entity<Group_User>().HasOne(gu => gu.Group).WithMany(g => g.Group_Users).HasForeignKey(gu => gu.GroupName);
            modelBuilder.Entity<Group_User>().HasOne(gu => gu.User).WithMany(u => u.Group_Users).HasForeignKey(gu => gu.UserName);

            // modelBuilder.Entity<Group>().HasMany(g => g.Books).WithOne(u => u.Group).HasForeignKey(u => u.GroupName);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options){
            base.OnConfiguring(options);
    
            string dirpath = AppDomain.CurrentDomain.BaseDirectory;
            string filename = Path.Combine(dirpath,  "chatdata.db");
            options.UseSqlite($"Data Source={filename}");
        }
    }
}