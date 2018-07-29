using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitiativeTrackerPrototype.Models
{
    public class PlayerCharacter: Character
    {
        //Properties
        public string RealName { get; protected set; }


        //Constructor
        
        public PlayerCharacter(string characterName, string realName, int initiativeBonus)
        {
            CharacterName = characterName;
            RealName = realName;
            InitiativeBonus = initiativeBonus;
        }

    }
}
