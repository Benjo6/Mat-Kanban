using Mat_Kanban.Services;
using Mat_Kanban.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Email;

namespace Mat_Kanban.Controllers
{
    public class BoardController : Controller
    {
        private readonly BoardService _boardService;

        public BoardController(BoardService boardService)
        {
            _boardService = boardService;
        }

        [Authorize (Policy = "ObserverAccess")]
        public IActionResult Index(int id)
        {
            var m = _boardService.GetBoard(id);
            return View(m);
        }


        [Authorize (Policy = "TeamPlayerAccess")]
        public IActionResult Details(int id)
        {
            return View(_boardService.GetBoard(id));
        }

        [Authorize (Policy = "ContributorAccess")]
        public IActionResult AddCard(int id)
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddCard(AddCard vm)
        {
            if (!ModelState.IsValid) return View(vm);

            _boardService.AddCard(vm);

            return RedirectToAction(nameof(Index), new { id = vm.Id });
        }
    }
}
