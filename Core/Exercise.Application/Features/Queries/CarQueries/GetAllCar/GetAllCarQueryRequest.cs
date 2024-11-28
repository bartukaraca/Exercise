using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise.Application.Features.Queries.CarQueries.GetAllCar
{
    public class GetAllCarQueryRequest : IRequest<GetAllCarQueryResponse>
    {
    }
}
