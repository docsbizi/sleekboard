namespace SleekBoard.Dtos
{
    public class BoardQueryInput : SortedDto
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}