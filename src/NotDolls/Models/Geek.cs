using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotDolls.Models
{
    public class Geek
    {
        public int GeekId { get; set; }
        public string Username { get; set; }
        public string EmailAddress { get; set; }
        public string Location { get; set; }
        public DateTime CreatedDate { get; set; }

        public ICollection<Inventory> Figurines { get; set; } // one to many relationship, one Geek can have many Figurines
    }
}
