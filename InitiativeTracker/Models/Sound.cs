using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitiativeTracker.Models
{
    public class Sound
    {

       //Theme song
       public void PlayTheme()
        {
            for (int i = 0; i < 2; i++)
            {
                Console.Beep(659, 500);
                Console.Beep(523, 500);
                Console.Beep(440, 500);
                Console.Beep(659, 500);
                Console.Beep(523, 500);
                Console.Beep(440, 500);
                Console.Beep(699, 500);
                Console.Beep(523, 500);
                Console.Beep(440, 500);
                Console.Beep(699, 500);
                Console.Beep(523, 500);
                Console.Beep(440, 500);
            }
            Console.Beep(440, 500);
            Console.Beep(494, 500);
            Console.Beep(523, 500);
            Console.Beep(587, 500);
            Console.Beep(660, 500);
            Console.Beep(740, 500);
            Console.Beep(830, 500);
            Console.Beep(880, 500);
        }
    }
}
