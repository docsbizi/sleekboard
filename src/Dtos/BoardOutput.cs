namespace SleekBoard.Dtos
{
    public class BoardOutput
    {
        public Guid Id { get; set; }    
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime CreationTime { get; set; }
        public DateTime? ModificationTime { get; set; }
    }
}