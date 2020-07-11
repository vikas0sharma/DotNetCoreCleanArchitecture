using Retrospective.Domain.Enums;

namespace Retrospective.Application.Sprint.Models
{
    public class SprintItemDto
    {
        public string Category { get; set; }

        public SprintItemType SprintItemType { get; set; }
    }
}
