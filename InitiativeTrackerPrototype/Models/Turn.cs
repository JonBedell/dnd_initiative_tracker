using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitiativeTrackerPrototype.Models
{
    class Turn
    {
        //Properties
        public int TurnNumber { get; private set; }
        public Character TurnCharacter { get; private set; }

        //Constructor
        public Turn(int turnNumber, Character turnCharacter)
        {
            TurnNumber = turnNumber;
            TurnCharacter = turnCharacter;
        }

    }
}
