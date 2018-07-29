using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitiativeTracker.Models
{
    public class PlayerCharacter: Character
    {
        //Properties
        public string Class { get; set; }
        public int Level { get; set; }
        public int PlayerID { get; set; }
        public int PcId { get; set; }

            

    }
}
