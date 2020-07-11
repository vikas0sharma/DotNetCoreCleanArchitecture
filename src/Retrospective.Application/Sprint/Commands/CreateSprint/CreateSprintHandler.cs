using MediatR;
using Retrospective.Application.Sprint.Models;
using System.Threading;
using System.Threading.Tasks;
using domain = Retrospective.Domain.AggregatesModel.SprintAggregate;

namespace Retrospective.Application.Sprint.Commands.CreateSprint
{
    public class CreateSprintHandler : IRequestHandler<CreateSprintCommand, SprintDto>
    {
        private readonly domain.ISprintRepository sprintRepository;

        public CreateSprintHandler(domain.ISprintRepository sprintRepository)
        {
            this.sprintRepository = sprintRepository;
        }

        public async Task<SprintDto> Handle(CreateSprintCommand request, CancellationToken cancellationToken)
        {
            var sprint = new domain.Sprint(request.UserId);

            foreach (var item in request.SprintItems)
            {
                sprint.AddSprintItem(new domain.SprintItem(item.Category, item.SprintItemType));
            }

            var result = sprintRepository.Add(sprint);

            await sprintRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            return new SprintDto
            {
                Id = result.Id
            };
        }
    }
}
