using Mat_Kanban.Models;
using Mat_Kanban.Services;
using Mat_Kanban.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Mat_Kanban.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class HomeController : Controller
    {
        private readonly BoardService _boardService;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, BoardService boardService)
        {
            _logger = logger;
            _boardService = boardService;
        }

        public IActionResult Index()
        {
            var m = _boardService.List();
            return View(m);
        }
        [Authorize(Policy = "OrganizerAccess")]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(NewBoard vm)
        {
            _boardService.AddBoard(vm);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
