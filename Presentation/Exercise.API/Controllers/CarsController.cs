
using Exercise.Application.Features.Commands.CarCommands.CreateCar;
using Exercise.Application.Features.Commands.CarCommands.RemoveCar;
using Exercise.Application.Features.Commands.CarCommands.UpdateCar;
using Exercise.Application.Features.Queries.CarQueries.GetAllCar;
using Exercise.Application.Features.Queries.CarQueries.GetCarById;
using Exercise.Application.Repositories.CarRepositories;
using Exercise.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Http.Headers;

namespace Exercise.API.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	//authorizon ekleyeceksin.
	public class CarsController : ControllerBase
	{
		readonly private ICarReadRepository _carReadRepository;
		readonly private ICarWriteRepository _carWriteRepository;
		readonly IMediator _mediator;

		public CarsController(ICarWriteRepository carWriteRepository, ICarReadRepository carReadRepository, IMediator mediator)
		{
			_carWriteRepository = carWriteRepository;
			_carReadRepository = carReadRepository;
			_mediator = mediator;
		}

		[HttpGet]
		public async Task<IActionResult> Get([FromQuery] GetAllCarQueryRequest getAllCarQueryRequest)
		{
			GetAllCarQueryResponse response = await _mediator.Send(getAllCarQueryRequest);
			return Ok(response);
		}
		[HttpPost]
		public async Task<IActionResult> Post(CreateCarCommandRequest createCarCommandRequest)
		{
			CreateCarCommandResponse response = await _mediator.Send(createCarCommandRequest);
			return StatusCode((int)HttpStatusCode.Created);
		}
		[HttpPut]
		public async Task<IActionResult> Put([FromBody]UpdateCarCommandRequest updateCarCommandRequest)
		{
			UpdateCarCommandResponse response = await _mediator.Send(updateCarCommandRequest);
			return Ok(response);
		}
		[HttpDelete("{Id}")]
		public async Task<IActionResult> Delete([FromRoute]RemoveCarCommandRequest removeCarCommandRequest)
		{
			RemoveCarCommandResponse response = await _mediator.Send(removeCarCommandRequest);
			return Ok(response);

		}
		[HttpGet("{Id}")]
		public async Task<IActionResult> Get([FromRoute] GetCarByIdQueryRequest getCarByIdQueryRequest)
		{
			GetCarByIdQueryResponse response = await _mediator.Send(getCarByIdQueryRequest);
			return Ok(response);
		}

	}
}
