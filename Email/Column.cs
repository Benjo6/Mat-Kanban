using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Email
{
    public class Column
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public List<Card> Cards { get; set; } = new List<Card>();
        public int BoardId { get; set; }

    }
}
