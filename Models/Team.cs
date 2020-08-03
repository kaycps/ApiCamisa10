using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCamisa10.Models
{
    public class Team
    {
        public int id { get; set; }
        public string dayofweak { get; set; }
        public string hour { get; set; }

        public int IdUser { get; set; }
        public virtual User user { get; set; }

        public virtual ICollection<Player> players { get; set; }

    }
}
