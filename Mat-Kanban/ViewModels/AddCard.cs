using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Mat_Kanban.ViewModels
{
    public class AddCard
    {
        public int Id { get; set; }
        [Required]
        public string Contents { get; set; }
    }
}
