using Exercise.Domain.Entities;
using Exercise.Domain.Entities.Common;
using Exercise.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise.Persistence.Contexts
{
	public class ExerciseDbContext : IdentityDbContext<AppUser,AppRole,string>
	{
		public ExerciseDbContext(DbContextOptions options) : base(options) { 
			
		}
		//Veritabanını temsil eden dbcontexte Car formatında bir tablo olacağını bildirdik. Adıda Cars.
		public DbSet<Car> Cars { get; set; } 
		public DbSet<CarBrand> CarBrands { get; set; } 
		public DbSet<Road> Roads { get; set; }
		

		public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
		{
			var datas = ChangeTracker.Entries<BaseEntitiy>();

			foreach (var data in datas)
			{
				_ = data.State switch
				{
					EntityState.Added => data.Entity.CreatedDate = DateTime.UtcNow,
					EntityState.Modified => data.Entity.UpdatedDate = DateTime.UtcNow,
					_ =>DateTime.UtcNow
				};
			}
			return await base.SaveChangesAsync(cancellationToken);
		}
	} 
}
