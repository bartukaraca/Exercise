using Exercise.Application.Features.Queries.CarBrandQueries.GetAllCarBrand;
using Exercise.Application.Features.Queries.CarBrandQueries.GetCarBrandById;
using Exercise.Application.Repositories.CarBrandRepositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Exercise.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CarBrandsController : ControllerBase
	{
		readonly private ICarBrandReadRepository _carBrandReadRepository;
		readonly private ICarBrandWriteRepository _carBrandWriteRepository;
		readonly IMediator _mediatR;

		public CarBrandsController(ICarBrandReadRepository carBrandReadRepository, ICarBrandWriteRepository carBrandWriteRepository, IMediator mediatR)
		{
			_carBrandReadRepository = carBrandReadRepository;
			_carBrandWriteRepository = carBrandWriteRepository;
			_mediatR = mediatR;
		}
		[HttpGet]
		public async Task<IActionResult> Get([FromQuery] GetAllCarBrandQueryRequest getAllCarBrandQuery)
		{
			GetAllCarBrandQueryResponse response = await _mediatR.Send(getAllCarBrandQuery);
			return Ok(response);
		}
		[HttpGet("{id}")]
		public async Task<IActionResult> Get([FromRoute] GetCarBrandByIdQueryRequest getCarBrandByIdQueryRequest)
		{
			GetCarBrandByIdQueryResponse response = await _mediatR.Send(getCarBrandByIdQueryRequest);
			return Ok(response);
		}
	}
}
