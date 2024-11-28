using Exercise.Application.Repositories;
using Exercise.Application.Repositories.CarRepositories;
using Exercise.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise.Application.Features.Commands.CarCommands.RemoveCar
{
    public class RemoveCarCommandHandler : IRequestHandler<RemoveCarCommandRequest, RemoveCarCommandResponse>
    {

        readonly ICarWriteRepository _carWriteRepository;

        public RemoveCarCommandHandler(ICarWriteRepository carWriteRepository)
        {
            _carWriteRepository = carWriteRepository;
        }

        public async Task<RemoveCarCommandResponse> Handle(RemoveCarCommandRequest request, CancellationToken cancellationToken)
        {

            await _carWriteRepository.RemoveAsync(request.Id);
            await _carWriteRepository.SaveAsync();
            return new RemoveCarCommandResponse
            {
                Message = "Araç başarıyla silindi.",
                Success = true,
            };

        }
    }
}
