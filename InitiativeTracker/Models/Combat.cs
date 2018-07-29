using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace InitiativeTracker.Models
{
    public class Combat
    {
        List<Character> TurnOrder { get; set; }

        public Combat(List<Character> characters)
        {
            TurnOrder.AddRange(characters);

            TurnOrder.Sort(delegate (Character c1, Character c2)
            {
                return c1.InitiativeTotal.CompareTo(c2.InitiativeTotal);
            });
            TurnOrder.Reverse();
        }
        


    }
}
