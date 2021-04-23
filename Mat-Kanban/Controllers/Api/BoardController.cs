using Mat_Kanban.Services;
using Mat_Kanban.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mat_Kanban.Controllers.Api
{
    public class BoardController : Controller
    {
        private readonly BoardService _boardService;

        public BoardController(BoardService boardService)
        {
            _boardService = boardService;
        }

        [HttpPost("movecard")]
        public IActionResult MoveCard([FromBody] MoveCardCommand c)
        {
            _boardService.Move(c);

            return Ok(new
            {
                Moved = true
            });
        }
    } 
}
