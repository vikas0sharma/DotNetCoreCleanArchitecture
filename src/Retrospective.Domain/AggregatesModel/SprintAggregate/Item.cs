using Retrospective.Domain.SeedWork;
using System.Collections.Generic;

namespace Retrospective.Domain.AggregatesModel.SprintAggregate
{
    public class Item : ValueObject
    {
        protected Item()
        {
        }

        public Item(string title, string desc, int addedBy)
        {
            Title = title;
            Description = desc;
            AddedBy = addedBy;
        }

        public string Title { get; }

        public string Description { get; }

        public int AddedBy { get; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Title;
            yield return Description;
            yield return AddedBy;
        }
    }
}
