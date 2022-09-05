using SleekBoard.Entities;

namespace SleekBoard.Repositories
{
    public interface IBoardRepository : IBaseRepository<Board> 
    {
        Task<List<Board>> GetAllAsync(string? name, string? description,string? sorting);
    }
}
