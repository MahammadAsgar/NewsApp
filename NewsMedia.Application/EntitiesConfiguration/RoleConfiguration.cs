using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NewsMedia.Domain.Models.Users;

namespace NewsMedia.Application.EntitiesConfiguration
{
    public class RoleConfiguration : IEntityTypeConfiguration<AppRole>
    {
        public void Configure(EntityTypeBuilder<AppRole> builder)
        {
            builder.HasData(
                new AppRole { Id = 1, Name = "Admin", NormalizedName = "ADMIN" },
                new AppRole { Id = 2, Name = "Moderator", NormalizedName = "MODERATOR" });
            //new AppRole { Id = 3, Name = "User", NormalizedName = "USER" });
        }
    }
}
