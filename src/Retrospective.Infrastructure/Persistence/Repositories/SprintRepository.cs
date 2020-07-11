using Microsoft.EntityFrameworkCore;
using Retrospective.Domain.AggregatesModel.SprintAggregate;
using Retrospective.Domain.SeedWork;
using System.Threading.Tasks;

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

        public Sprint Add(Sprint sprint)
        {
            if (sprint.IsTransient())
            {
                return sprintContext.Sprints.Add(sprint).Entity;
            }
            return sprint;
        }

        public async Task<Sprint> Get(int sprintId)
        {
            var sprint = await sprintContext
                .Sprints
                .FirstOrDefaultAsync(s => s.Id == sprintId);

            return sprint;
        }

        public bool Update(Sprint sprint)
        {
            sprintContext.Entry(sprint).State = EntityState.Modified;
            return true;
        }
    }
}
