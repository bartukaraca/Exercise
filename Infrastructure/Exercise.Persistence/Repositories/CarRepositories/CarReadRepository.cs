using Exercise.Application.Repositories.CarRepositories;
using Exercise.Domain.Entities;
using Exercise.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise.Persistence.Repositories.CarRepositories
{
	public class CarReadRepository : ReadRepository<Car>, ICarReadRepository
	{
		public CarReadRepository(ExerciseDbContext context) : base(context)
		{
		}
	}
}
