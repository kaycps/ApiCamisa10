using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCamisa10.Models
{
    public class Player
    {
        public int id { get; set; }
        public string name { get; set; }

        public int idPosition { get; set; }
        public virtual Position position { get; set; }

        public int idLevel { get; set; }
        public virtual Level level { get; set; }

        public int idTeam { get; set; }
        public virtual Team team { get; set; }
        
       
    }
}
