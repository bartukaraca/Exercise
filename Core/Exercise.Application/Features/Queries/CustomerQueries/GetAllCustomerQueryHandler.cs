using Exercise.Application.Abstractions.Services;
using Exercise.Application.Repositories;
using Exercise.Application.Repositories.CarRepositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise.Application.Features.Queries.CustomerQueries
{
	public class GetAllCustomerQueryHandler : IRequestHandler<GetAllCustomerQueryRequest, GetAllCustomerQueryResponse>
	{
		readonly IMainService _mainService;

		public GetAllCustomerQueryHandler(IMainService mainService)
		{
			_mainService = mainService;
		}

		public async Task<GetAllCustomerQueryResponse> Handle(GetAllCustomerQueryRequest request, CancellationToken cancellationToken)
		{
			// Müşterileri alıyoruz
			var customers = _mainService.GetAllCustomers();

			// Response nesnesini oluşturup müşteri listesini atıyoruz
			var response = new GetAllCustomerQueryResponse
			{
				Customers = customers
			};

			return await Task.FromResult(response);
		}
	}
}