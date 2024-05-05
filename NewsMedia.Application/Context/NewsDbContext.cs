using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NewsMedia.Application.EntitiesConfiguration;
using NewsMedia.Domain.Models.Entities;
using NewsMedia.Domain.Models.Users;
using System.Reflection.Emit;

namespace NewsMedia.Application.Context
{
    public class NewsDbContext : IdentityDbContext<AppUser, AppRole, int>
    {
        public NewsDbContext(DbContextOptions<NewsDbContext> options) : base(options)
        {

        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    //optionsBuilder.EnableSensitiveDataLogging();
        //    //base.OnConfiguring(optionsBuilder);
        //}
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new RoleConfiguration());
            base.OnModelCreating(builder);

            builder.Entity<Article>()
                .HasMany(x => x.ArticleContentFiles)
                .WithOne(x => x.Article)
                .HasForeignKey(x => x.ArticleId);
        }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryBase> CategoryBases { get; set; }
        public DbSet<ArticleContentFile> ArticleContentFiles { get; set; }
        public DbSet<ArticleTitleFile> ArticleTitleFiles { get; set; }
    }
}
