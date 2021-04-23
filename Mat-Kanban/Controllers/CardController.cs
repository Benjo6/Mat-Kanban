using Email;
using Mat_Kanban.Data;
using Mat_Kanban.Services;
using Mat_Kanban.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Mat_Kanban.Controllers
{
    public class CardController : Controller
    {
        private readonly CardService _cardService;
        private readonly ApplicationDbContext _context;
        public CardController(CardService cardService,  ApplicationDbContext context)
        {
            _cardService = cardService;
            _context = context;
        }
        [Authorize(Policy = "TeamPlayerAccess")]
        [HttpGet]
        public IActionResult Details(int id)
        {
            var vm = _cardService.GetDetails(id);

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Update(CardDetails cardDetails)
        {
           var email = new EmailWorker();
           //var appContext = _context.Boards.Include(c => c.User).Where(i => i.UserEmail == User.Identity.Name);
            _cardService.Update(cardDetails);

            if (cardDetails.Column == 3)
            {
               await email.SendEmailAsync(cardDetails);
                TempData["Message"] = "Saved Card and Emails sent";
            }
            
            TempData["Message"] = "Saved Card";
            

            return RedirectToAction(nameof(Details), new { id = cardDetails.Id });
        }
    }
}
