﻿using Exercise.Application.Repositories.RoadRepositories;
using Exercise.Domain.Entities;
using Exercise.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise.Persistence.Repositories.RoadRepositories
{
	public class RoadReadRepository : ReadRepository<Road>, IRoadReadRepository

	{
		public RoadReadRepository(ExerciseDbContext context) : base(context)
		{
		}

		public async Task<Road> GetByIdWithStatusAsync(int id, bool tracking = true)
		{
			return await _context.Roads
						 .Include(r => r.RoadStatus)
						 .FirstOrDefaultAsync(r => r.Id == id);
		}
	}
}
