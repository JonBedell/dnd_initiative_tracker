using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitiativeTrackerPrototype.Models
{
    public class NonPlayerCharacter: Character
    {
        //Properties - No additional needed

        //Constructor
        public NonPlayerCharacter(string characterName, int initiativeBonus)
        {
            CharacterName = characterName;
            InitiativeBonus = initiativeBonus;
        }

    }
}
