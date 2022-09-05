using SleekBoard.Enums;

namespace SleekBoard.Dtos
{
    public class BoardItemOutput
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime? DueDate { get; set; }
        public Statuses Status { get; set; }
        public DateTime? CompletionTime { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime? ModificationTime { get; set; }
    }
}
