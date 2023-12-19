using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.Web.Data
{
	public class AuthDbContext : IdentityDbContext
	{
		public AuthDbContext(DbContextOptions options) : base(options)
		{
		}


		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			// Seed Roles (User, Admin, SuperAdmin)

			var adminRoleId = "683575de-0cba-4697-9c32-bb475d717905";
			var superAdminRoleId = "4d89c898-7286-4e75-a054-059f12495bad";
			var userRoleId = "50915800-a987-41af-abd0-9142f704b06f";

			var roles = new List<IdentityRole>
			{
				new IdentityRole
				{
					Name = "Admin",
					NormalizedName = "Admin",
					Id = adminRoleId,
					ConcurrencyStamp = adminRoleId
				},
				new IdentityRole
				{
					Name = "SuperAdmin",
					NormalizedName = "SuperAdmin",
					Id = superAdminRoleId,
					ConcurrencyStamp = superAdminRoleId
				},
				new IdentityRole
				{
					Name = "User",
					NormalizedName = "User",
					Id = userRoleId,
					ConcurrencyStamp = userRoleId
				}


			};

			builder.Entity<IdentityRole>().HasData(roles);

			// Seed SuperAdminUser
			var superAdminId = "5a2827ad-60d2-4809-9342-02fd152c3635";

			var superAdminUser = new IdentityUser
			{
				UserName = "superadmin@bloggie.com",
				Email = "superadmin@bloggie.com",
				NormalizedEmail = "superadmin@bloggie.com".ToUpper(),
				NormalizedUserName = "superadmin@bloggie.com".ToUpper(),
				Id = superAdminId
			};

			superAdminUser.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(superAdminUser, "Superadmin@123");

			builder.Entity<IdentityUser>().HasData(superAdminUser);

			// Add All roles to SuperAdminUser

			var superAdminRoles = new List<IdentityUserRole<string>>
			{
				new IdentityUserRole<string>
				{
				RoleId = adminRoleId,
				UserId = superAdminId
				},
				new IdentityUserRole<string>
				{
				RoleId = superAdminRoleId,
				UserId = superAdminId
				},
				new IdentityUserRole<string>
				{
				RoleId = userRoleId,
				UserId = superAdminId
				}
			};

			builder.Entity<IdentityUserRole<string>>().HasData(superAdminRoles);
		}
	}
}
