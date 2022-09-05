using SleekBoard.Entities;
using SleekBoard.Enums;

namespace SleekBoard.Repositories
{
    public interface IBoardItemRepository : IBaseRepository<BoardItem> 
    {
        Task<List<BoardItem>> GetAllByBoardIdAsync(
            Guid boardId, 
            string? name, 
            string? description,            
            DateTime? minDueDate, 
            DateTime? maxDueDate,
            Statuses? status, 
            string? sorting);
    }
}