using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Email
{
    public class Card
    {
        public int Id { get; set; }
        [Required]
        public string Contents { get; set; }
        public string Notes { get; set; }
        public int ColumnId { get; set; }

        public Column Column { get; set; }


    }
}