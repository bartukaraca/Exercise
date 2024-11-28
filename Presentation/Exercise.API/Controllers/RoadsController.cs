using Exercise.Application.Features.Commands.RoadCommands.CreateRoad;
using Exercise.Application.Features.Commands.RoadCommands.RemoveRoad;
using Exercise.Application.Features.Commands.RoadCommands.UpdateRoad;
using Exercise.Application.Features.Queries.CarQueries.GetAllCar;
using Exercise.Application.Features.Queries.RoadQueries.GetAllRoad;
using Exercise.Application.Features.Queries.RoadQueries.GetRoadById;
using Exercise.Application.Repositories.CarRepositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Exercise.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class RoadsController : ControllerBase
	{
		readonly ICarReadRepository _carReadRepository;
		readonly ICarWriteRepository _carWriteRepository;
		readonly IMediator _mediatR;

		public RoadsController(ICarReadRepository carReadRepository, ICarWriteRepository carWriteRepository, IMediator mediatR)
		{
			_carReadRepository = carReadRepository;
			_carWriteRepository = carWriteRepository;
			_mediatR = mediatR;
		}
		[HttpGet]
		public async Task<IActionResult> Get([FromQuery] GetAllRoadQueryRequest getAllRoadQueryRequest)
		{
			GetAllRoadQueryResponse response = await _mediatR.Send(getAllRoadQueryRequest);
			return Ok(response);
		}
		[HttpGet("{Id}")]
		public async Task<IActionResult> Get([FromRoute] GetRoadByIdQueryRequest getRoadByIdQueryRequest)
		{
			GetRoadByIdQueryResponse response = await _mediatR.Send(getRoadByIdQueryRequest);
			return Ok(response);
		}
		[HttpPost]
		public async Task<IActionResult> Post(CreateRoadCommandRequest createRoadCommandRequest)
		{
			CreateRoadCommandResponse response = await _mediatR.Send(createRoadCommandRequest);
			return StatusCode((int)HttpStatusCode.Created);
		}
		[HttpDelete("{Id}")]
		public async Task<IActionResult> Delete(RemoveRoadCommandRequest removeRoadCommandRequest)
		{
			RemoveRoadCommandResponse response = await _mediatR.Send(removeRoadCommandRequest);
			return Ok(response);

		}
		[HttpPut]
		public async Task<IActionResult> Put([FromBody] UpdateRoadCommandRequest updateRoadCommandRequest)
		{
			UpdateRoadCommandResponse response = await _mediatR.Send(updateRoadCommandRequest);
			return Ok(response);
		}
	}
}
