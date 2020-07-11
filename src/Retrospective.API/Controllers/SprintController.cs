using MediatR;
using Microsoft.AspNetCore.Mvc;
using Retrospective.Application.Sprint.Commands.CreateSprint;
using System.Threading;
using System.Threading.Tasks;

namespace Retrospective.API.Controllers
{
    [Route("sprint")]
    [ApiController]
    public class SprintController : ControllerBase
    {
        private readonly IMediator mediator;

        public SprintController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task Post([FromBody] CreateSprintCommand command, CancellationToken cancellationToken)
        {
            var result = await mediator.Send(command, cancellationToken);
        }
    }
}
