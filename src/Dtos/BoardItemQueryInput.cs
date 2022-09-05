using SleekBoard.Enums;

namespace SleekBoard.Dtos
{
    public class BoardItemQueryInput : SortedDto
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime? MinDueDate { get; set; }
        public DateTime? MaxDueDate { get; set; }
        public Statuses? Status { get; set; }
    }
}