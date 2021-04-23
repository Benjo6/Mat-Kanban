using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mat_Kanban.ViewModels
{
    public class RoleUser
    {
        public IEnumerable<Users> Users { get; set; }
        public IEnumerable<string> Roles { get; set; }
        public string UserEmail { get; set; }
        public string Role { get; set; }
        public bool Delete { get; set; }
    }
}
