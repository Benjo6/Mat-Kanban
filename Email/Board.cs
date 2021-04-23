using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Email
{
   
    public class Board
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public List<Column> Columns { get; set; } = new List<Column>();
        
        [ForeignKey("UserId")]
        public virtual IdentityUser User { get; set; }
        public string UserId { get; set; }
        public string UserEmail { get; set; }
    }
}
