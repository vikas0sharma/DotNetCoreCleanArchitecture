using Retrospective.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Retrospective.Domain.AggregatesModel.SprintAggregate
{
    public class Sprint : Entity, IAggregateRoot
    {
        protected Sprint()
        {
        }

        public Sprint(int createdBy)
        {
            CreatedBy = createdBy;
            CreatedOn = DateTime.Now;
        }

        public int CreatedBy { get; }

        public DateTime CreatedOn { get; }

        public IReadOnlyCollection<SprintItem> SprintItems => sprintItems;

        private readonly List<SprintItem> sprintItems = new List<SprintItem>();

        public void AddSprintItem(SprintItem sprintItem)
        {
            sprintItems.Add(sprintItem);
        }

        public void AddSprintPoint(int sprintItemId, Item item)
        {
            sprintItems
                .First(s => s.Id == sprintItemId)
                .AddSprintPoint(item);
        }
    }
}
