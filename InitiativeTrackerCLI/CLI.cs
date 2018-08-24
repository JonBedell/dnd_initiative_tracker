using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitiativeTracker.Models;
using InitiativeTracker.DALs;
using System.Threading;

namespace InitiativeTrackerCLI
{
    public class CLI
    {
        const string DatabaseConnection = @"Data Source=.\SQLEXPRESS;Initial Catalog=InitiativeTracker;Integrated Security = True";
        public List<Character> _activeParty = new List<Character>();
        public List<Character> _activePartyNPCs = new List<Character>();
        public List<Character> _enemies = new List<Character>();
        public List<Character> _combatList = new List<Character>();
        private PlayerDAL _playerDAL = new PlayerDAL(DatabaseConnection);
        private PcDAL _pcDAL = new PcDAL(DatabaseConnection);
        private NpcDAL _npcDAL = new NpcDAL(DatabaseConnection);


        public void RunCLI()
        {
            PrintSplash();
            Console.ReadKey();
            //RedFlash();
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
                Console.WriteLine("4) Roll a D20");
                Console.WriteLine("q) Quit");
                Console.WriteLine();
                string userInput = CLIHelper.GetIntInRangeOrQ("Select an Option!", 1, 4, "q", true);
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
                    ChooseEnemiesMainMenu();
                }
                else if (userInput == "4")
                {
                    RollD20CLI();
                }
            }
        }

        /// <summary>
        /// Main Menu for editing party
        /// </summary>
        public void PartyEditor()
        {
            bool isGood = false;

            while (!isGood)
            {
                Console.Clear();

                PrintHeader("Party Editor");
                PrintParty();
                Console.WriteLine();
                Console.WriteLine();

                Console.WriteLine("1) Add Party Member");
                Console.WriteLine("2) Add NPC to Party");
                Console.WriteLine("3) Remove Party Memeber");
                Console.WriteLine("q) Quit");
                Console.WriteLine();

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

        /// <summary>
        /// Look through list of players 
        /// </summary>
        public void AddPcToParty()
        {
            bool isGood = false;

            while (!isGood)
            {
                Console.Clear();

                PrintHeader("Add Party Member");

                Console.WriteLine();
                Console.WriteLine("SELECT YOUR PLAYER NAME");
                Console.WriteLine();

                List<Player> players = _playerDAL.GetAllPlayers();
                List<int> playerIds = new List<int>();
                foreach (Player player in players)
                {
                    Console.WriteLine($"{player.PlayerID}) {player.Name}");
                    playerIds.Add(player.PlayerID);
                }
                Console.WriteLine();
                string input = CLIHelper.GetIntInListOrQ("enter an id:", playerIds, "q", false);
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

       /// <summary>
       /// Choose PC from player's list of characters and add to party
       /// </summary>
       /// <param name="playerID">Player_ID primary key</param>
        public void ChoosePC(int playerID)
        {
            bool isGood = false;

            while (!isGood)
            {
                Console.Clear();

                PrintHeader("Choose Your Character");
                Console.WriteLine();

                Player player = _playerDAL.GetPlayerByID(playerID);
                List<PlayerCharacter> pcs = _pcDAL.GetAllPlayersPCs(playerID);
                List<int> pcIds = new List<int>();
                if (pcs.Count > 0)
                {
                    Console.WriteLine("{0,-5}{1,-20}{2,-35}", "ID", "Name", "Class");
                    Console.WriteLine("------------------------------------------------------------");
                    foreach (PlayerCharacter pc in pcs)
                    {
                        Console.WriteLine("{0,-5}{1,-20}{2,-35}", pc.PcId, pc.Name, $"lvl {pc.Level} {pc.Race} {pc.TypeClass}");
                        pcIds.Add(pc.PcId);
                    }
                    Console.WriteLine();

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
                else
                {
                    Console.WriteLine();

                    CLIHelper.CenteredWriteline($"Sorry, {player.Name.ToUpper()} has not added a CHARACTER to the database");
                    CLIHelper.CenteredWriteline("Go to the DATABASE EDITOR to create a character and start your adventure!");
                    Console.WriteLine();
                    CLIHelper.CenteredWriteline("(press any key to return to the PARTY EDITOR)");

                    Console.ReadKey();
                    isGood = true;
                }
            }
        }

        /// <summary>
        /// Add an NPC to the party (i.e. da good guyz)
        /// </summary>
        public void AddNpcToParty()
        {
            bool isGood = false;

            while (!isGood)
            {
                Console.Clear();

                PrintHeader("Add Party Member");
                Console.WriteLine();
                PrintParty();
                Console.WriteLine();
                Console.WriteLine();

                Console.WriteLine("UNDER CONSTRUCTION - PRESS ANY KEY TO RETURN");
                Console.ReadKey();
                isGood = true;
            }
        }

        /// <summary>
        /// Remove a character from the party
        /// </summary>
        public void RemoveFromParty()
        {
            bool isGood = false;

            while (!isGood)
            {
                Console.Clear();

                PrintHeader("Remove Party Member");
                Console.WriteLine("UNDER CONSTRUCTION - PRESS ANY KEY TO RETURN");
                Console.ReadKey();
                isGood = true;

                Console.WriteLine();
            }
        }

        /// <summary>
        /// Create players or characters and add them to the database
        /// </summary>
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
                Console.WriteLine();

                string input = CLIHelper.GetIntInRangeOrQ("SELECT AN OPTION", 1, 3, "q", true);
                if (input == "Q" || input == "q")
                {
                    isGood = true;
                }
                else if (input == "1")
                {
                    AddPlayerMenu();
                }
                else if (input == "2")
                {
                    AddPlayerCharacterSelectPlayer();
                }
            }
        }

        /// <summary>
        /// Add a player Menu
        /// </summary>
        public void AddPlayerMenu()
        {
            bool isGood = false;

            while (!isGood)
            {
                Console.Clear();

                PrintHeader("Database Editor - Add Player");
                Console.WriteLine();

                string userName = CLIHelper.GetString("ENTER A NAME:");
                bool noExceptions = true;
                try
                {
                    _playerDAL.AddPlayer(userName);
                }
                catch (Exception e)
                {
                    Console.WriteLine("ERROR:" + e.Message);
                    noExceptions = false;
                    string exInput = CLIHelper.GetXorY("Sorry something's gone wrong, press Q to quit or R to retry", "q", "r");
                    if (exInput.ToLower() == "q")
                    {
                        isGood = true;
                    }
                }

                if (noExceptions)
                {
                    Console.WriteLine("Success! Press any key to return to the DataBase Editor Main Menu");
                    Console.ReadKey();
                    isGood = true;
                }

            }
        }

        /// <summary>
        /// Choose Player for PC to add to Database
        /// </summary>
        public void AddPlayerCharacterSelectPlayer()
        {
            bool isGood = false;

            while (!isGood)
            {
                Console.Clear();

                PrintHeader("Add Player Character - Select Player");

                Console.WriteLine();
                Console.WriteLine("SELECT YOUR PLAYER NAME");
                Console.WriteLine();

                List<Player> players = _playerDAL.GetAllPlayers();
                List<int> playerIds = new List<int>();
                foreach (Player player in players)
                {
                    Console.WriteLine($"{player.PlayerID}) {player.Name}");
                    playerIds.Add(player.PlayerID);
                }
                Console.WriteLine();
                string input = CLIHelper.GetIntInListOrQ("enter an id:", playerIds, "q", false);
                if (input == "Q" || input == "q")
                {
                    isGood = true;
                }
                else
                {
                    GetNewPcInfoAndAddToDB(Convert.ToInt32(input));
                    isGood = true;
                }
            }
        }

        /// <summary>
        /// Get PC info from player inputs and add character to database
        /// </summary>
        /// <param name="playerId">Player ID to assign character to</param>
        public void GetNewPcInfoAndAddToDB(int playerId)
        {
            bool isGood = false;

            while (!isGood)
            {
                Console.Clear();

                PrintHeader("Database Editor - Add Player");
                Console.WriteLine();

                string name = CLIHelper.GetString("ENTER A NAME:");
                string race = CLIHelper.GetString("ENTER CHARACTER RACE:");
                string typeClass = CLIHelper.GetString("ENTER CLASS:");
                int level = CLIHelper.GetIntInRange("ENTER LEVEL:",1,20,false);
                int initiativeBonus = CLIHelper.GetIntInRange("ENTER INITIATIVE BONUS (DEX modifier + eqpt bonuses):",1,20,false);
                int AC = CLIHelper.GetInteger("ENTER ARMOR CLASS:");
                string description = CLIHelper.GetString("ENTER CHARACTER DESCRIPTION:");

                bool noExceptions = true;
                try
                {
                    _pcDAL.AddPC(playerId, name, typeClass, level, initiativeBonus, AC, race, description);
                }
                catch (Exception e)
                {
                    Console.WriteLine("ERROR:" + e.Message);
                    noExceptions = false;
                    string exInput = CLIHelper.GetXorY("Sorry something's gone wrong, press Q to quit or R to retry", "q", "r");
                    if (exInput.ToLower() == "q")
                    {
                        isGood = true;
                    }
                }

                if (noExceptions)
                {
                    Console.WriteLine("Success! Press any key to return to the DataBase Editor Main Menu");
                    Console.ReadKey();
                    isGood = true;
                }

            }
        }

        /// <summary>
        /// First Step of Combat: Add Enemies! Maybe Move this to Main Menu....
        /// </summary>
        public void ChooseEnemiesMainMenu()
        {
            bool isGood = false;

            while (!isGood)
            {
                Console.Clear();

                PrintHeader("COMBAT - Choose Enemies");
                Console.WriteLine();
                PrintEnemies();
                Console.WriteLine();
                Console.WriteLine("1) Search By Enemy Type");
                Console.WriteLine("2) Search By Challenge Rating");
                Console.WriteLine("3) Search For Preloaded Encounter");
                Console.WriteLine("4) Clear Current Enemies");
                Console.WriteLine("5) GET INTIATIVE ROLLS");
                Console.WriteLine("q) Quit");
                Console.WriteLine();
                string userInput = CLIHelper.GetIntInRangeOrQ("Choose Your Search Method or Press (Q) to Quit to Main Menu", 1, 5, "q", true);

                if (userInput == "Q" || userInput == "q")
                {
                    isGood = true;
                }
                else if (userInput == "1")
                {
                    string enemyType = CLIHelper.GetString("Enter Enemy Type:");
                    SearchEnemyType(enemyType);
                }
                else if (userInput == "2")
                {
                    bool isRange = false;
                    while (!isRange)
                    {
                        double crMin = CLIHelper.GetDoubleInRange("Enter Minimum CR:", 1, 20);
                        double crMax = CLIHelper.GetDoubleInRange("Enter Maximum CR:", 1, 20);
                        if (crMax >= crMin)
                        {
                            SearchChallengeRange(crMin, crMax);
                            isRange = true;
                            isGood = true;
                        }
                        else
                        {
                            string retryChoice = CLIHelper.GetXorYorZ("Max is less than Min! Press (Y) to Retry, " +
                                "(R) to Return to enemy selection, or (Q) to quit to Main Menu", "Y", "R", "Q", true);
                            if (retryChoice.ToLower() == "r" || retryChoice.ToLower() == "q")
                            {
                                isRange = true;
                            }
                            if (retryChoice.ToLower() == "q")
                            {
                                isGood = true;
                            }

                        }
                    }
                }
                else if (userInput == "3")
                {
                    int encounterID = CLIHelper.GetInteger("Enter Encounter ID number:");
                    SearchEncounter(encounterID);
                }
                else if (userInput == "4")
                {
                    ClearEnemies();
                }
                else if (userInput == "5")
                {
                    LetsRoll();
                }
            }
        }

        /// <summary>
        /// Confirm that the user would like to clear the enemies list, if yes, clear em out!
        /// </summary>
        public void ClearEnemies()
        {
            if (_enemies.Count() == 0)
            {
                Console.WriteLine("No Enemies to Clear! Press Any Key to Continue.");
                Console.ReadKey();
            }
            else
            {
                bool confirm = CLIHelper.GetBoolCustom("Are You Sure You Would Like To Clear the enemies List?","y", "n");

                if (confirm)
                {
                    _enemies.Clear();
                    Console.WriteLine("Clear Successful. Press any key to continue");
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine("Nice Save! Press any key to continue");
                    Console.ReadKey();
                }
            }
        }

        /// <summary>
        /// Search Enemy Database by Enemy Type (i.e. Gerblin, WereRat, etc)
        /// </summary>
        /// <param name="type"></param>
        public void SearchEnemyType(string type)
        {
            bool isGood = false;

            while (!isGood)
            {
                Console.Clear();

                PrintHeader("COMBAT - Choose Enemies by Type");
                Console.WriteLine();

                Console.WriteLine($"TYPE: {type}");
                Console.WriteLine();

                List<NonPlayerCharacter> enemies = _npcDAL.GetEnemiesByType(type);
                int listCount = 1;
                Console.WriteLine("{0,-10}{1,-20}{2,-20}{3,-5}{4,-5}", "Select No.", "Type", "Race", "CR", "AC");
                Console.WriteLine("------------------------------------------------------------");

                foreach (NonPlayerCharacter enemy in enemies)
                {
                    Console.WriteLine("{0,-10}{1,-20}{2,-20}{3,-5}{4,-5}", " "+listCount.ToString()+")", enemy.TypeClass, enemy.Race, enemy.Level, enemy.ArmorClass);
                    Console.WriteLine($"DESCRIPTION: {enemy.Description}");
                    Console.WriteLine();
                    listCount++;
                }
                int enemyCount = enemies.Count();
                string userInput = CLIHelper.GetIntInRangeOrQ("Select Enemy or Press (Q) to Return to Enemy Search", 1, enemyCount, "q", false);
                Console.WriteLine();

                if(userInput.ToLower() == "q")
                {
                    isGood = true;
                }
                else
                {
                    int index = int.Parse(userInput) -1;
                    NonPlayerCharacter enemy = enemies[index];
                    NameAndAddEnemy(enemy);
                    bool addMore = CLIHelper.GetBoolCustom("Enemy Added! Press (A) to add another enemy from this list or (R) to return to enemy search?", "a", "r");
                    if(!addMore)
                    {
                        isGood = true;
                    }
                }
            }
        }

        /// <summary>
        /// Rename an Enemy Character and add it to list
        /// </summary>
        /// <param name="enemy">Enemy whose name you would like to change</param>
        public void NameAndAddEnemy(NonPlayerCharacter enemy)
        {
            if (enemy.Name == "")
            {
                string name = CLIHelper.GetString($"Enter a name for {enemy.TypeClass}");
                enemy.Name = name;
            }
            else
            {
                bool changeName = CLIHelper.GetBoolCustom($"This enemy's name is currently {enemy.Name}." +
                    " Would you like to change it?", "y", "n");
                if (changeName)
                {
                    string name = CLIHelper.GetString($"Enter a name for {enemy.TypeClass}");
                    enemy.Name = name;
                }
            }
            _enemies.Add(enemy);

        }

        /// <summary>
        /// Search Enemy Database by Challenge Rating (CR) Range
        /// </summary>
        /// <param name="crMin">Minimum Challenge Rating Value</param>
        /// <param name="crMax">Maximum Challenge Rating Value</param>
        public void SearchChallengeRange(double crMin, double crMax)
        {
            bool isGood = false;

            while (!isGood)
            {
                Console.Clear();

                PrintHeader("COMBAT - Choose Enemies");
                Console.WriteLine();
                Console.WriteLine($"Min CR: {crMin}       Max CR: {crMax}");
                Console.WriteLine();

            }
        }

        /// <summary>
        /// Search Database for Preloaded Encounter by ID
        /// </summary>
        /// <param name="type"></param>
        public void SearchEncounter(int encounterID)
        {
            bool isGood = false;

            while (!isGood)
            {
                Console.Clear();

                PrintHeader("Search Encounter");
                Console.WriteLine("UNDER CONSTRUCTION - PRESS ANY KEY TO RETURN");
                Console.ReadKey();
                isGood = true;

                Console.WriteLine();
            }
        }

        /// <summary>
        /// Combine Lists of Characters and Collect Rolls
        /// </summary>
        public void LetsRoll()
        {
            bool isGood = false;

            while (!isGood)
            {
                Console.Clear();

                PrintHeader("COMBAT - ROLL INITIATIVE");
                PrintParty();
                Console.WriteLine();
                PrintEnemies();
                Console.WriteLine();

                //Combine Lists
                _combatList.Clear();
                _combatList.AddRange(_activeParty);
                _combatList.AddRange(_activePartyNPCs);
                _combatList.AddRange(_enemies);

                //Collect Raw Rolls or Auto Roll
                foreach(Character item in _combatList)
                {
                    int roll = 0;
                    string wantRoll = CLIHelper.GetIntInRangeOrQ($"Enter Raw Roll for {item.Name} or press (A) for auto Roll", 1, 20, "A", false);
                    if (wantRoll.ToLower() == "a")
                    {
                        roll = RollD20ForChar(item.Name);
                    }
                    else
                    {
                        roll = int.Parse(wantRoll);
                    }
                    item.InitiativeRoll = roll;
                }
                Console.ReadKey();
                Console.WriteLine("Roll's Collected! Press any Key to show combat order");
                isGood = true;
                ShowCombatOrder();
            }
        }

        /// <summary>
        /// Final Screen! Shows the turn order based on initiative rolls.
        /// </summary>
        public void ShowCombatOrder()
        {
            bool isGood = false;

            while (!isGood)
            {
                Console.Clear();

                PrintHeader("COMBAT - Turn Order");
                Console.WriteLine();
                Console.WriteLine("{0,-6}{1,-20}{2,-20}{3,-5}{4,-5}", "Init.", "Name", "Class/Type", "CR/lvl", "AC");
                Console.WriteLine("------------------------------------------------------------");
                Combat combat = new Combat(_combatList);
                foreach (Character item in combat.TurnOrder)
                {
                    Console.WriteLine("{0,-6}{1,-20}{2,-20}{3,-5}{4,-5}", item.InitiativeTotal, item.Name, item.TypeClass, item.Level, item.ArmorClass);
                }

                Console.WriteLine();
                string quit = CLIHelper.GetX("Press (Q) to finish combat", "q");
                bool quitConfirm = CLIHelper.GetBoolCustom("Are you sure you want to quit?", "y", "n");

                if(quitConfirm)
                {
                    isGood = true;
                }

            }
        }

        /// <summary>
        /// Roll a D20
        /// </summary>
        public int RollD20ForChar(string name)
        {
            int result = 20;
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            CLIHelper.ClearCurrentConsoleLine();
            bool isDone = false;
            while (!isDone)
            {
                Random random = new Random();
                int roll = random.Next(1, 20);
                Console.WriteLine($"{name}'s Roll: {roll.ToString()}");
                bool wantReroll = CLIHelper.GetBoolCustom("Roll Again?", "y", "n");
                if (wantReroll)
                {
                    Console.SetCursorPosition(0, Console.CursorTop - 2);
                    CLIHelper.ClearCurrentConsoleLine();
                }
                else
                {
                    result = roll;
                    isDone = true;
                }
            }
            return result;
        }

        /// <summary>
        /// Print a roll and give option to reroll
        /// </summary>
        public void RollD20CLI()
        {
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            CLIHelper.ClearCurrentConsoleLine();
            Console.WriteLine();
            bool isDone = false;
            while (!isDone)
            { 
                Random random = new Random();
                int roll = random.Next(1, 20);
                Console.WriteLine($"Your Roll: {roll.ToString()}");
                bool wantReroll = CLIHelper.GetBoolCustom("Roll Again?", "y", "n");
                if(wantReroll)
                {
                    Console.SetCursorPosition(0, Console.CursorTop - 2);
                    CLIHelper.ClearCurrentConsoleLine();
                }
                else
                {
                    isDone = true;
                }
            }
        }

        /// <summary>
        /// Return Nat 20
        /// </summary>
        /// <returns></returns>
        public int CheatRollD20()
        {
            return 20;
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
                    Console.WriteLine($"{pc.Name} - lvl {pc.Level} {pc.TypeClass}");
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
                    Console.WriteLine($"{npc.Name} - {npc.TypeClass}");
                }
            }

        }

        /// <summary>
        /// Prints out the list of the current party and any accompanying NPCS
        /// </summary>
        public void PrintEnemies()
        {
            Console.WriteLine("CURRENT ENEMIES:");
            Console.WriteLine("--------------");


            bool haveEnemy = EnemyCheck();

            if (haveEnemy)
            {
                foreach (NonPlayerCharacter npc in _enemies)
                {
                    Console.WriteLine($"{npc.Name} - {npc.TypeClass} CR: {npc.Level}");
                }
            }
            else
            {
                Console.WriteLine("NO ENEMIES");
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

        /// <summary>
        /// See if there are any PCs in the active party
        /// </summary>
        /// <returns>true if there is at least 1 party member</returns>
        public bool EnemyCheck()
        {
            bool result = false;
            if (_enemies.Count() > 0)
            {
                result = true;
            }
            return result;
        }

        /// <summary>
        /// Print Cool Logo!
        /// </summary>
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

        /// <summary>
        /// It's supposed to Make the console flash red
        /// </summary>
        public void RedFlash()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Thread.Sleep(500);
            Console.ForegroundColor = ConsoleColor.Black;
            Thread.Sleep(500);
            Console.ForegroundColor = ConsoleColor.Red;
            Thread.Sleep(500);
            Console.ForegroundColor = ConsoleColor.Black;
            Thread.Sleep(500);
        }

    }
}
