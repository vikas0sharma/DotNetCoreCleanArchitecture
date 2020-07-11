using Retrospective.Domain.Enums;
using Retrospective.Domain.SeedWork;
using System.Collections.Generic;

namespace Retrospective.Domain.AggregatesModel.SprintAggregate
{
    public class SprintItem : Entity
    {
        protected SprintItem()
        {
        }

        public SprintItem(string category, SprintItemType sprintItemTypes)
        {
            Category = category;
            SprintItemType = sprintItemTypes;
        }

        public string Category { get; }

        public SprintItemType SprintItemType { get; }

        public IReadOnlyCollection<Item> Items => items;

        public void AddSprintPoint(Item item)
        {
            items.Add(item);
        }

        private readonly List<Item> items = new List<Item>();
    }
}
