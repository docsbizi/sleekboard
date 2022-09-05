using System.ComponentModel.DataAnnotations;

namespace SleekBoard.Dtos
{
    public class CreateBoardItemInput
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public DateTime? DueDate { get; set; }
    }
}