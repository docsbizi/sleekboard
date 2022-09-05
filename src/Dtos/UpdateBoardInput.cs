using System.ComponentModel.DataAnnotations;

namespace SleekBoard.Dtos
{
    public class UpdateBoardInput
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;
    }
}