using Exercise.Application.ViewModels.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise.Application.Features.Queries.CustomerQueries
{
    public class GetAllCustomerQueryResponse
    {
		public IEnumerable<CustomerViewModel> Customers { get; set; }
	}
}
