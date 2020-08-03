using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCamisa10.Models
{
    public class Position
    {
        public int id { get; set; }
        public string name { get; set; }

        public virtual ICollection<Player> players{get;set;}
    }
}
