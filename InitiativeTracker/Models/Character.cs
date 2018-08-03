using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitiativeTracker.Models
{
    public abstract class Character
    {
        public string Name { get; set; }
        public int InitiativeBonus { get; set; }
        public int InitiativeRoll { get; set; }
        public int ArmorClass { get; set; }
        public string Description { get; set; }
        public string TypeClass { get; set; }
        public double Level { get; set; }
        public string Race { get; set; }

        //Derived Properties
        public int InitiativeTotal
        {
            get
            {
                return InitiativeRoll + InitiativeBonus;
            }
        }
               
    }
}
