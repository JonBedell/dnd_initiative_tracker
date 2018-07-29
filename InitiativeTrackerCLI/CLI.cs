using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitiativeTracker.Models;
using InitiativeTracker.DALs;

namespace InitiativeTrackerCLI
{
    public class CLI
    {
        const string DatabaseConnection = @"Data Source=.\SQLEXPRESS;Initial Catalog=InitiativeTracker;Integrated Security = True";
        public List<Character> _activeParty = new List<Character>();
        public List<Character> _activePartyNPCs = new List<Character>();
        private PlayerDAL _playerDAL = new PlayerDAL(DatabaseConnection);
        private PcDAL _pcDAL = new PcDAL(DatabaseConnection);
        private NpcDAL _npcDAL = new NpcDAL(DatabaseConnection);


        public void RunCLI()
        {
            PrintSplash();
            Console.ReadKey();
            MainMenu();
        }

        /// <summary>
        /// Main Menu After Splash Screen
        /// </summary>
        public void MainMenu()
        {
            bool programOver = false;

            while (!programOver)
            {
                Console.Clear();

                PrintHeader("Initiative Tracker - Main Menu");
                PrintParty();
                Console.WriteLine();
                Console.WriteLine();

                Console.WriteLine("1) Party Editor");
                Console.WriteLine("2) Database Editor");
                Console.WriteLine("3) ENTER COMBAT");
                Console.WriteLine("q) Quit");
                Console.WriteLine();
                string userInput = CLIHelper.GetIntInRangeOrQ("Select an Option!", 1, 3, "q", true);
                if (userInput == "Q" || userInput == "q")
                {
                    programOver = true;
                }
                else if (userInput == "1")
                {
                    PartyEditor();
                }
                else if (userInput == "2")
                {
                    DataBaseEditor();
                }
                else if (userInput == "3")
                {
                }
            }
        }

        public void PartyEditor()
        {
            bool isGood = false;

            while (!isGood)
            {
                Console.Clear();

                PrintHeader("Party Editor");
                PrintParty();
                Console.WriteLine();

                Console.WriteLine("1) Add Party Member");
                Console.WriteLine("2) Add NPC to Party");
                Console.WriteLine("3) Remove Party Memeber");
                Console.WriteLine("q) Quit");

                string input = CLIHelper.GetIntInRangeOrQ("SELECT AN OPTION", 1, 2, "q", true);
                if (input == "Q" || input == "q")
                {
                    isGood = true;
                }
                else if (input == "1")
                {
                    AddPcToParty();
                }
                else if (input == "2")
                {
                    RemoveFromParty();
                }
                else if (input == "3")
                {
                    RemoveFromParty();
                }
            }
        }

        public void AddPcToParty()
        {
            bool isGood = false;

            while (!isGood)
            {
                Console.Clear();

                PrintHeader("Add Party Member");

                Console.WriteLine();
                List<Player> players = _playerDAL.GetAllPlayers();
                List<int> playerIds = new List<int>();
                foreach (Player player in players)
                {
                    Console.WriteLine($"{player.PlayerID}) {player.Name}");
                    playerIds.Add(player.PlayerID);
                }
                string input = CLIHelper.GetIntInListOrQ("Select Player", playerIds, "q", false);
                if (input == "Q" || input == "q")
                {
                    isGood = true;
                }
                else
                {
                    ChoosePC(Convert.ToInt32(input));
                    isGood = true;
                }
            }
        }

        public void ChoosePC(int playerID)
        {
            bool isGood = false;

            while (!isGood)
            {
                Console.Clear();

                PrintHeader("Choose Your Character");
                Console.WriteLine("{0,-15}{1,-20}{2,-35}", "ID", "Name", "Class");

                Console.WriteLine();
                List<PlayerCharacter> pcs = _pcDAL.GetAllPlayersPCs(playerID);
                List<int> pcIds = new List<int>();
                foreach (PlayerCharacter pc in pcs)
                {
                    Console.WriteLine("{0,-15}{1,-20}{2,-35}", pc.PcId, pc.Name, $"lvl {pc.Level} {pc.Class}");
                    pcIds.Add(pc.PcId);
                }
                string input = CLIHelper.GetIntInListOrQ("Select Character or Press Q to Quit", pcIds, "q", false);
                if (input == "Q" || input == "q")
                {
                    isGood = true;
                }
                else
                {
                    int pcIndex = Convert.ToInt32(input) - 1;
                    _activeParty.Add(pcs[pcIndex]);
                    isGood = true;
                }
            }
        }


        public void AddNpcToParty()
        {
            bool isGood = false;

            while (!isGood)
            {
                Console.Clear();

                PrintHeader("Add Party Member");
                PrintParty();
                Console.WriteLine();
            }
        }

        public void RemoveFromParty()
        {
            bool isGood = false;

            while (!isGood)
            {
                Console.Clear();

                PrintHeader("Remove Party Member");
                

                Console.WriteLine();
            }
        }


        public void DataBaseEditor()
        {
            bool isGood = false;

            while (!isGood)
            {
                Console.Clear();

                PrintHeader("Database Editor - Main Menu");
                Console.WriteLine();
                Console.WriteLine("1) Add Player");
                Console.WriteLine("2) Create Character");
                Console.WriteLine("3) Create NPC");
                Console.WriteLine("q) Quit");

                string input = CLIHelper.GetIntInRangeOrQ("SELECT AN OPTION", 1, 3, "q", true);
                if (input == "Q" || input == "q")
                {
                    isGood = true;
                }


            }
        }



        /// <summary>
        /// Prints out a centered header with SWORDS!
        /// </summary>
        /// <param name="header">String to be used as header</param>
        public void PrintHeader(string header)
        {
            string space = "   ";
            string decoration = "";

            string handleLeft = ("       O                ");
            string swordLeft = ("{o)xxx |===============-");
            string handleRight = ("                O       ");
            string swordRight = ("-===============| xxx(o}");

            for (int i = 0; i < header.Length; i++)
            {
                decoration += "*";
            }

            string snName = swordLeft + space + header + space + swordRight;
            string fullHandle = handleLeft + space + decoration + space + handleRight;

            Console.SetCursorPosition((Console.WindowWidth - snName.Length) / 2, Console.CursorTop);
            Console.WriteLine(fullHandle);
            Console.SetCursorPosition((Console.WindowWidth - snName.Length) / 2, Console.CursorTop);
            Console.WriteLine(snName);
            Console.SetCursorPosition((Console.WindowWidth - snName.Length) / 2, Console.CursorTop);
            Console.WriteLine(fullHandle);
            Console.WriteLine();
        }

        /// <summary>
        /// Prints out the list of the current party and any accompanying NPCS
        /// </summary>
        public void PrintParty()
        {
            Console.WriteLine("CURRENT PARTY:");
            Console.WriteLine("--------------");


            bool havePCs = PartyPcCheck();
            bool haveNPCs = PartyNpcCheck();

            if (havePCs)
            {
                foreach (PlayerCharacter pc in _activeParty)
                {
                    Console.WriteLine($"{pc.Name} - lvl {pc.Level} {pc.Class}");
                }
            }
            else
            {
                Console.WriteLine("NO PLAYER CHARACTERS - GO TO PARTY EDITOR TO ADD PCs");
            }
            if (haveNPCs)
            {
                Console.WriteLine("NPCs:");
                foreach (NonPlayerCharacter npc in _activePartyNPCs)
                {
                    Console.WriteLine($"{npc.Name} - {npc.Type}");
                }
            }

        }

        /// <summary>
        /// See if there are any PCs in the active party
        /// </summary>
        /// <returns>true if there is at least 1 party member</returns>
        public bool PartyPcCheck()
        {
            bool result = false;
            if (_activeParty.Count() > 0)
            {
                result = true;
            }
            return result;
        }

        /// <summary>
        /// See if there are any NPCs in the active party
        /// </summary>
        /// <returns>true if there is at least 1 NPC party member</returns>
        public bool PartyNpcCheck()
        {
            bool result = false;
            if (_activePartyNPCs.Count() > 0)
            {
                result = true;
            }
            return result;
        }

        public void PrintSplash()
        {
            Sound sound = new Sound();
            string title = "   _________ _       ___________________________ _______ __________________          _______ ";


            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.SetCursorPosition((Console.WindowWidth - title.Length) / 2, Console.CursorTop);
            Console.WriteLine(@"   _________ _       ___________________________ _______ __________________          _______ ");
            Console.SetCursorPosition((Console.WindowWidth - title.Length) / 2, Console.CursorTop);
            Console.WriteLine(@"   \__   __/( (    /|\__   __/\__   __/\__   __/(  ___  )\__   __/\__   __/|\     /|(  ____ \");
            Console.SetCursorPosition((Console.WindowWidth - title.Length) / 2, Console.CursorTop);
            Console.WriteLine(@"      ) (   |  \  ( |   ) (      ) (      ) (   | (   ) |   ) (      ) (   | )   ( || (    \/");
            Console.SetCursorPosition((Console.WindowWidth - title.Length) / 2, Console.CursorTop);
            Console.WriteLine(@"      | |   |   \ | |   | |      | |      | |   | (___) |   | |      | |   | |   | || (__    ");
            Console.SetCursorPosition((Console.WindowWidth - title.Length) / 2, Console.CursorTop);
            Console.WriteLine(@"      | |   | (\ \) |   | |      | |      | |   |  ___  |   | |      | |   ( (   ) )|  __)   ");
            Console.SetCursorPosition((Console.WindowWidth - title.Length) / 2, Console.CursorTop);
            Console.WriteLine(@"      | |   | | \   |   | |      | |      | |   | (   ) |   | |      | |    \ \_/ / | (      ");
            Console.SetCursorPosition((Console.WindowWidth - title.Length) / 2, Console.CursorTop);
            Console.WriteLine(@"   ___) (___| )  \  |___) (___   | |   ___) (___| )   ( |   | |   ___) (___  \   /  | (____/\");
            Console.SetCursorPosition((Console.WindowWidth - title.Length) / 2, Console.CursorTop);
            Console.WriteLine(@"   \_______/|/    )_)\_______/   )_(   \_______/|/     \|   )_(   \_______/   \_/   (_______/");
            Console.SetCursorPosition((Console.WindowWidth - title.Length) / 2, Console.CursorTop);

            Console.SetCursorPosition((Console.WindowWidth - title.Length) / 2, Console.CursorTop);
            Console.WriteLine("       O                                                                               O       ");
            Console.SetCursorPosition((Console.WindowWidth - title.Length) / 2, Console.CursorTop);
            Console.WriteLine("{o)xxx |===============-                     * * *                     -===============| xxx(o}");
            Console.SetCursorPosition((Console.WindowWidth - title.Length) / 2, Console.CursorTop);
            Console.WriteLine("       O                                                                               O       ");
            Console.WriteLine();
            Console.SetCursorPosition((Console.WindowWidth - title.Length) / 2, Console.CursorTop);
            Console.WriteLine("                                   (press any key to continue)");
            //sound.PlayTheme();

        }

    }
}
