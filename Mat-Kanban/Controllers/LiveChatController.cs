using Mat_Kanban.Data;
using Mat_Kanban.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mat_Kanban.Controllers
{
    public class LiveChatController : Controller
    {



        [Authorize(Policy = "ObserverAccess")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
