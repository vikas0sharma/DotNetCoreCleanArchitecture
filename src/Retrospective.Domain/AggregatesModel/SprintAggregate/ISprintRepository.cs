using Retrospective.Domain.SeedWork;

namespace Retrospective.Domain.AggregatesModel.SprintAggregate
{
    public interface ISprintRepository : IRepository<Sprint>
    {
        Sprint AddSprint(Sprint sprint);
    }
}
