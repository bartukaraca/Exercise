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
	public class CarBrandWriteRepository : WriteRepository<CarBrand>, ICarBrandWriteRepository
	{
		public CarBrandWriteRepository(ExerciseDbContext context) : base(context)
		{
		}
	}
}
