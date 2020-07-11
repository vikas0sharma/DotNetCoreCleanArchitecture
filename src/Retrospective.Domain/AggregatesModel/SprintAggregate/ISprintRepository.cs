using Retrospective.Domain.SeedWork;
using System.Threading.Tasks;

namespace Retrospective.Domain.AggregatesModel.SprintAggregate
{
    public interface ISprintRepository : IRepository<Sprint>
    {
        Sprint Add(Sprint sprint);

        bool Update(Sprint sprint);

        Task<Sprint> Get(int sprintId);
    }
}
