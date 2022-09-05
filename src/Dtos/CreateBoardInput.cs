using System.ComponentModel.DataAnnotations;

namespace SleekBoard.Dtos
{
    public class CreateBoardInput
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }
    }
}