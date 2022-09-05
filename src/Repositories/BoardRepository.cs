using Microsoft.EntityFrameworkCore;
using SleekBoard.Database;
using SleekBoard.Entities;
using System.Linq.Dynamic.Core;

namespace SleekBoard.Repositories
{
    public class BoardRepository : BaseRepository<Board>, IBoardRepository
    {
        private readonly SleekBoardContext _context = null!;

        public BoardRepository(SleekBoardContext context) : base(context)
        {
            _context = context;
        }

        public virtual async Task<List<Board>> GetAllAsync(
            string? name, 
            string? description,
            string? sorting)
        {
            var query = _context.Boards.AsQueryable();

            if (!string.IsNullOrWhiteSpace(name))
            {
                query = query.Where(x => x.Name.Contains(name));
            }
            if (!string.IsNullOrWhiteSpace(description))
            {
                query = query.Where(x => x.Name.Contains(description));
            }

            return await query
                .OrderBy(string.IsNullOrWhiteSpace(sorting) ? "CreationTime" : sorting)
                .ToListAsync();
        }
    }
}