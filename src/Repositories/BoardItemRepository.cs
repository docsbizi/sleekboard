using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;
using SleekBoard.Database;
using SleekBoard.Entities;
using SleekBoard.Enums;

namespace SleekBoard.Repositories
{
    public class BoardItemRepository : BaseRepository<BoardItem>, IBoardItemRepository
    {
        private readonly SleekBoardContext _context = null!;

        public BoardItemRepository(SleekBoardContext context): base(context)
        {
            _context = context;
        }

        public async Task<List<BoardItem>> GetAllByBoardIdAsync(
            Guid boardId, 
            string? name, 
            string? description,            
            DateTime? minDueDate, 
            DateTime? maxDueDate,
            Statuses? status, 
            string? sorting)
        {
            var query = _context.BoardItems.AsQueryable();

            if (!string.IsNullOrWhiteSpace(name))
            {
                query = query.Where(x => x.Name.Contains(name));
            }
            if (!string.IsNullOrWhiteSpace(description))
            {
                query = query.Where(x => x.Name.Contains(description));
            }
            if (minDueDate != null)
            {
                query = query.Where(x => x.DueDate >= minDueDate);
            }
            if (maxDueDate != null)
            {
                query = query.Where(x => x.DueDate <= maxDueDate);
            }
            if (status != null)
            {
                query = query.Where(x => x.Status == status);
            }

            return await query
                .OrderBy(string.IsNullOrWhiteSpace(sorting) ? "CreationTime" : sorting)
                .ToListAsync();
        }
    }
}