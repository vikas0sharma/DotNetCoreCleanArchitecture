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

        public SprintItem(string category, SprintItemTypes sprintItemTypes)
        {
            Category = category;
            SprintItemType = sprintItemTypes;
        }

        public string Category { get; }

        public SprintItemTypes SprintItemType { get; }

        public IReadOnlyCollection<Item> Items => items;

        private readonly List<Item> items = new List<Item>();
    }
}
