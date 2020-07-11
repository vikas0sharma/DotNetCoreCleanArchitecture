using MediatR;
using Retrospective.Domain.AggregatesModel.SprintAggregate;
using System.Threading;
using System.Threading.Tasks;

namespace Retrospective.Application.Sprint.Commands.AddPoint
{
    public class AddPointHandler : IRequestHandler<AddPointCommand, bool>
    {
        private readonly ISprintRepository sprintRepository;

        public AddPointHandler(ISprintRepository sprintRepository)
        {
            this.sprintRepository = sprintRepository;
        }

        public async Task<bool> Handle(AddPointCommand request, CancellationToken cancellationToken)
        {
            var sprint = await sprintRepository.Get(request.SprintId);
            if (sprint is null)
            {
                return false;
            }

            sprint.AddSprintPoint(request.SprintItemId, new Item(request.Item.Title, request.Item.Description, request.Item.AddedBy));

            sprintRepository.Update(sprint);

            return await sprintRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
