using ChatGPTClone.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChatGPTClone.Infrastructure.Persistence.Seeders;

    public class UserSeeder : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            var initialUserId = Guid.Parse("2798212b-3e5d-4556-8629-a64eb70da4a8");

            var initialUser = new AppUser
            {
                Id = initialUserId,
                UserName = "merve",
                NormalizedUserName = "MERVE",
                Email = "merve@gmail.com",
                NormalizedEmail = "MERVE@GMAİL.COM",
                EmailConfirmed = true,
                CreatedByUserId = initialUserId.ToString(),
                CreatedOn = new DateTimeOffset(new DateTime(2024, 08, 28)),
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                FirstName = "Merve",
                LastName = "Ekşi",
                LockoutEnabled = false,
                AccessFailedCount = 0
            };

            initialUser.PasswordHash = new PasswordHasher<AppUser>().HashPassword(initialUser, "123merve123");

            builder.HasData(initialUser);
        }
    }