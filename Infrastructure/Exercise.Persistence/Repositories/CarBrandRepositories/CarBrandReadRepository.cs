using Exercise.Application.Repositories.CarBrandRepositories;
using Exercise.Domain.Entities;
using Exercise.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise.Persistence.Repositories.CarBrandRepositories
{
	public class CarBrandReadRepository : ReadRepository<CarBrand>, ICarBrandReadRepository
	{
		public CarBrandReadRepository(ExerciseDbContext context) : base(context)
		{
		}
	}
}
