using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SleekBoard.Dtos;
using SleekBoard.Entities;
using SleekBoard.Repositories;

namespace SleekBoard.Controllers
{
    [Route("api/boards")]
    [ApiController]
    public class BoardController : ControllerBase
    {
        private readonly IBoardRepository _boardRepository;
        private readonly IBoardItemRepository _boardItemRepository;
        private readonly IMapper _mapper;

        public BoardController(
            IBoardRepository boardRepository,
            IBoardItemRepository boardItemRepository,
            IMapper mapper)
        {
            _boardRepository = boardRepository;
            _boardItemRepository = boardItemRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BoardOutput>>> GetBoards([FromQuery] BoardQueryInput input)
        {
            var boards = await _boardRepository.GetAllAsync(input.Name, input.Description, input.Sorting);
            return _mapper.Map<List<Board>, List<BoardOutput>>(boards);
        }

        [HttpGet("{boardId}")]
        public async Task<ActionResult<BoardOutput>> GetBoard(Guid boardId)
        {
            var board = await _boardRepository.FindAsync(boardId);
            if (board == null)
            {
                return NotFound();
            }

            return _mapper.Map<Board, BoardOutput>(board);
        }

        [HttpPut("{boardId}")]
        public async Task<IActionResult> UpdateBoard(Guid boardId, UpdateBoardInput input)
        {
            var board = await _boardRepository.FindAsync(boardId);
            if (board == null)
            {
                return NotFound();
            }

            board
                .UpdateName(input.Name)
                .UpdateDescription(input.Description);

            await _boardRepository.UpdateAsync(board);

            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> CreateBoard(CreateBoardInput input)
        {
            var board = new Board(
                input.Name,
                input.Description);

            await _boardRepository.AddAsync(board);

            return CreatedAtAction(nameof(GetBoard), new { boardId = board.Id }, new { Id = board.Id });
        }

        [HttpDelete("{boardId}")]
        public async Task<IActionResult> DeleteBoard(Guid boardId)
        {
            var board = await _boardRepository.FindAsync(boardId);
            if (board == null)
            {
                return NotFound();
            }

            await _boardRepository.DeleteAsync(boardId);
            return NoContent();
        }

        [HttpGet("{boardId}/items")]
        public async Task<ActionResult<IEnumerable<BoardItemOutput>>> GetBoardItems(Guid boardId, [FromQuery] BoardItemQueryInput input)
        {
            var board = await _boardRepository.FindAsync(boardId);
            if (board == null)
            {
                return NotFound();
            }

            var items = await _boardItemRepository.GetAllByBoardIdAsync(
                boardId, 
                input.Name, 
                input.Description, 
                input.MinDueDate, 
                input.MaxDueDate,
                input.Status,
                sorting: input.Sorting);

            return _mapper.Map<List<BoardItem>, List<BoardItemOutput>>(items);
        }

        [HttpGet("{boardId}/items/{itemId}")]
        public async Task<ActionResult<BoardItemOutput>> GetBoardItem(Guid boardId, Guid itemId)
        {
            var board = await _boardRepository.FindAsync(boardId);
            if (board == null)
            {
                return NotFound();
            }

            var item = await _boardItemRepository.FindAsync(itemId);
            if (item == null)
            {
                return NotFound();
            }

            return _mapper.Map<BoardItem, BoardItemOutput>(item);
        }

        [HttpPost("{boardId}/items")]
        public async Task<IActionResult> CreateBoardItem(Guid boardId, CreateBoardItemInput input)
        {
            var board = await _boardRepository.FindAsync(boardId);
            if (board == null)
            {
                return NotFound();
            }

            var item = new BoardItem(
                input.Name,
                boardId,
                input.Description,
                input.DueDate
            );

            await _boardItemRepository.AddAsync(item);

            return CreatedAtAction(nameof(GetBoardItem), new { boardId = boardId, itemId = item.Id }, new { Id = item.Id });
        }

        [HttpPut("{boardId}/items/{itemId}")]
        public async Task<IActionResult> UpdateBoardItem(Guid boardId, Guid itemId, UpdateBoardItemInput input)
        {
            var board = await _boardRepository.FindAsync(boardId);
            if (board == null)
            {
                return NotFound();
            }

            var item = await _boardItemRepository.FindAsync(itemId);
            if (item == null)
            {
                return NotFound();
            }

            item
                .UpdateName(input.Name)
                .UpdateDescription(input.Description)
                .UpdateDueDate(input.DueDate)
                .UpdateStatus(input.Status);

            await _boardItemRepository.UpdateAsync(item);

            return NoContent();
        }

        [HttpDelete("{boardId}/items/{itemId}")]
        public async Task<IActionResult> DeleteBoardItem(Guid boardId, Guid itemId)
        {
            var board = await _boardRepository.FindAsync(boardId);
            if (board == null)
            {
                return NotFound();
            }

            var item = await _boardItemRepository.FindAsync(itemId);
            if (item == null)
            {
                return NotFound();
            }

            await _boardItemRepository.DeleteAsync(itemId);

            return NoContent();
        }
    }
}
