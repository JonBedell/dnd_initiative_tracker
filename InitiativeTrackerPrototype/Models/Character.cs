using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitiativeTrackerPrototype.Models
{
    public abstract class Character
    {
        #region Properties

        public string CharacterName { get; protected set; }
        public int InitiativeBonus { get; protected set; }
        public int InitiativeRoll { get; set; }
        public int ArmorClass { get; protected set; }
        public string TypeClass { get; set; }
        public int Level { get; set; }

        //Derived Properties
        public int InitiativeTotal
        {
            get
            {
                return InitiativeRoll + InitiativeBonus;
            }
        }

        #endregion


        
    }
}
