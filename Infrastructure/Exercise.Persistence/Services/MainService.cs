using Exercise.Application.Abstractions.Services;
using Exercise.Application.Services;
using Exercise.Application.ViewModels.Customer;
using Exercise.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise.Persistence.Services
{
	
	public class MainService : IMainService
	{
		private readonly ExerciseDbContext _context;
		private readonly IDataService _dataService;
	

		public MainService(ExerciseDbContext context, IDataService dataService)
		{
			_context = context;
			_dataService = dataService;
		}

		public IEnumerable<CustomerViewModel> GetAllCustomers()
		{
			string query = "SELECT Id, Name, CreatedDate, UpdatedDate FROM customers";

			// Dapper'ı kullanarak veri çekiyoruz
			var customers = _dataService.ReturnDataAsIE<CustomerViewModel>(query);

			return customers;
		}
	}
}
