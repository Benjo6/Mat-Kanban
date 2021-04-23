using Mat_Kanban.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.Services.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mat_Kanban.ViewModels
{

    public class Users
    {

        public string Email { get; set; }
        public IEnumerable<UserRole> Roles { get; set; }

    }
}
