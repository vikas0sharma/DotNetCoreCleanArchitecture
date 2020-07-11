using MediatR;
using Retrospective.Application.Sprint.Models;

namespace Retrospective.Application.Sprint.Commands.AddPoint
{
    public class AddPointCommand : IRequest<bool>
    {
        public int SprintId { get; set; }

        public int SprintItemId { get; set; }

        public ItemDto Item { get; set; }
    }
}
