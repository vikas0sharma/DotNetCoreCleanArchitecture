using MediatR;
using Retrospective.Application.Sprint.Models;
using System.Collections.Generic;

namespace Retrospective.Application.Sprint.Commands.CreateSprint
{
    public sealed class CreateSprintCommand : IRequest<bool>
    {
        public IEnumerable<SprintItemDto> SprintItems { get; set; }

        public int UserId { get; set; }

        public CreateSprintCommand()
        {
            SprintItems = new List<SprintItemDto>();
        }

        public CreateSprintCommand(int userId, IEnumerable<SprintItemDto> sprintItems)
            : this()
        {
            UserId = userId;
            SprintItems = sprintItems;
        }
    }
}
