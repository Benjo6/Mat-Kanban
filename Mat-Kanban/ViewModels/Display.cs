using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mat_Kanban.ViewModels
{
    public class Display
    {
        public IEnumerable<string> Roles { get; set; }
        public IEnumerable<Users> Users { get; set; }
    }
}
