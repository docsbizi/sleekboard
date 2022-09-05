using System.ComponentModel.DataAnnotations;
using SleekBoard.Enums;

namespace SleekBoard.Dtos
{
    public class UpdateBoardItemInput
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public DateTime? DueDate { get; set; }

        public Statuses Status { get; set; }
    }
}