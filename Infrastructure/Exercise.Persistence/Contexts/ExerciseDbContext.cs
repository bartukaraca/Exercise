using Exercise.Domain.Entities;
using Exercise.Domain.Entities.Common;
using Exercise.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise.Persistence.Contexts
{
	public class ExerciseDbContext : IdentityDbContext<AppUser, AppRole, string>
	{
		public ExerciseDbContext(DbContextOptions options) : base(options)
		{

		}
		//Veritabanını temsil eden dbcontexte Car formatında bir tablo olacağını bildirdik. Adıda Cars.
		public DbSet<Car> Cars { get; set; }
		public DbSet<Road> Roads { get; set; }
		public DbSet<RoadStatus> RoadStatuses { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Road>()
		   .HasOne(r => r.RoadStatus)
		   .WithMany()
		   .HasForeignKey(r => r.RoadStatusId);

			// Car ve Road arasındaki ilişki  
			modelBuilder.Entity<Car>()
				.HasOne(c => c.Road)
				.WithMany()
				.HasForeignKey(c => c.RoadId);

			modelBuilder.Entity<IdentityUserLogin<string>>()
				.HasKey(login => new { login.LoginProvider, login.ProviderKey });

			modelBuilder.Entity<IdentityUserRole<string>>(entity =>
			{
				entity.HasKey(iur => new { iur.UserId, iur.RoleId });
			});

			modelBuilder.Entity<IdentityUserToken<string>>().HasKey(token => new { token.UserId, token.LoginProvider });
		}

		public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
		{
			var datas = ChangeTracker.Entries<BaseEntitiy>();

			foreach (var data in datas)
			{
				_ = data.State switch
				{
					EntityState.Added => data.Entity.CreatedDate = DateTime.UtcNow,
					EntityState.Modified => data.Entity.UpdatedDate = DateTime.UtcNow,
					_ => DateTime.UtcNow
				};
			}
			return await base.SaveChangesAsync(cancellationToken);
		}
	}
}
