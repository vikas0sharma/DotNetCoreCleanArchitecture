using Retrospective.Domain.AggregatesModel.SprintAggregate;
using Retrospective.Domain.SeedWork;

namespace Retrospective.Infrastructure.Persistence.Repositories
{
    public class SprintRepository : ISprintRepository
    {
        private readonly SprintContext sprintContext;

        public SprintRepository(SprintContext sprintContext)
        {
            this.sprintContext = sprintContext;
        }

        public IUnitOfWork UnitOfWork => sprintContext;

        public Sprint AddSprint(Sprint sprint)
        {
            return sprintContext.Sprints.Add(sprint).Entity;
        }
    }
}
