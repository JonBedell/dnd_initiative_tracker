using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitiativeTracker;
using InitiativeTracker.Models;

namespace InitiativeTrackerCLI
{
    class Program
    {
        static void Main(string[] args)
        {
            
            CLI menu = new CLI();
            menu.RunCLI();

        }
    }
}



