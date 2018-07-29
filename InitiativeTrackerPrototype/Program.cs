//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using InitiativeTracker.Models;

//namespace InitiativeTrackerPrototype
//{
//    class Program
//    {
//        static void Main(string[] args)
//        {
//            Sound sound = new Sound();

//            #region SplashScreen
//            Console.WriteLine("An old dwarf approaches your party. He greets you with a friendly wave.");
//            Console.WriteLine("\"Hail and well met, Traveller!\"");
//            Console.WriteLine();
//            Console.WriteLine("\"It seems you've run into some trouble...\"");
//            Console.WriteLine($"");
//            Console.WriteLine("(press any key to continue)");
//            sound.PlayTheme();

//            Console.ReadKey();
//            #endregion

//            #region Variables and Placeholders
//            List<Character> characterList = new List<Character>();
//            List<PlayerCharacter> playerCharacterList = new List<PlayerCharacter>();
//            List<NonPlayerCharacter> nonPlayerCharacterList = new List<NonPlayerCharacter>();
//            Dictionary<string, Character> characterDictionary = new Dictionary<string, Character>();
//            Dictionary<string, PlayerCharacter> playerCharacterDictionary = new Dictionary<string, PlayerCharacter>();
//            Dictionary<string, NonPlayerCharacter> nonPlayerCharacterDictionary = new Dictionary<string, NonPlayerCharacter>();
//            #endregion

//            //Collecting PC info
//            #region Collecting PC info
//            bool areAllPCsEntered = false;
//            bool isProgramMad = false;
//            do
//            {
//                #region Get Character Name
//                Console.Clear();
//                Console.WriteLine("What is your name, traveller?");
//                string characterName = Console.ReadLine();
//                #endregion

//                #region Get Initiative Bonus

//                Console.WriteLine($"");
//                Console.WriteLine($"{characterName}, eh? A fine name indeed!");
//                Console.WriteLine($"Tell me {characterName}, How swift are you in combat?");
//                Console.WriteLine("(enter initiative bonus: DEX modifier + other bonuses)");
//                int initBonus = 0;
//                bool isBonusANumber = false;
//                int numberOfLoops = 0;
//                do
//                {
//                    Console.WriteLine($"");
//                    string initBonusString = Console.ReadLine();
//                    if (int.TryParse(initBonusString, out initBonus))
//                    {
//                        isBonusANumber = true;
//                    }
//                    else if (numberOfLoops < 3)
//                    {
//                        numberOfLoops++;
//                        Console.WriteLine($"You misunderstand, {characterName}, I was thinking on more of number-type scale...");
//                        Console.WriteLine($"Why don't you try again?");

//                    }
//                    else if (numberOfLoops >= 3)
//                    {
//                        Console.WriteLine("It is not wise to anger me...");
//                        Console.WriteLine("(your initiative bonus is now -5, asshole)");
//                        initBonus = -5;
//                        isProgramMad = true;
//                        isBonusANumber = true;
//                    }
//                }
//                while (!isBonusANumber);
//                #endregion

//                #region Get Real Name

//                Console.WriteLine();
//                Console.WriteLine($"I see you are not of this plane... What do they call you in your world?");
//                Console.WriteLine("(enter your real name, or a nickname)");
//                string playerName = Console.ReadLine();
//                #endregion

//                #region Create and Add Character
//                PlayerCharacter newPlayerCharacter = new PlayerCharacter(characterName, playerName, initBonus);
//                characterList.Add(newPlayerCharacter);
//                characterDictionary.Add(newPlayerCharacter.CharacterName, newPlayerCharacter);
//                playerCharacterDictionary.Add(newPlayerCharacter.CharacterName, newPlayerCharacter);
//                playerCharacterList.Add(newPlayerCharacter);
//                #endregion

//                #region Add Another Character?
//                Console.WriteLine($"");
//                Console.WriteLine($"{playerName}? Am I saying that correctly? Your land must be further from ours than I thought.");
//                Console.WriteLine($"A traveller of your ability must have allies, no?");

//                bool isYOrN = true;
//                do
//                {
//                    Console.WriteLine($"");
//                    Console.WriteLine($"(are there more pcs to enter? <y/n>)");
//                    string morePlayersString = Console.ReadLine().ToLower();
//                    if (morePlayersString == "y")
//                    {
//                        Console.WriteLine($"");
//                        Console.WriteLine($"Well then, {characterName}, it was a pleasure meeting you.  Axe high, friend!");
//                        Console.WriteLine("*the old dwarf turns to speak to the next player character*)");
//                        Console.WriteLine("");
//                        Console.WriteLine("(pass the computer to the next player and then press any key to continue)");
//                        isYOrN = true;
//                    }
//                    else if (morePlayersString == "n")
//                    {
//                        areAllPCsEntered = true;
//                        isYOrN = true;
//                    }
//                    else
//                    {
//                        Console.WriteLine($"You misunderstand, {characterName}, it is a simple yes or no question.");
//                        Console.WriteLine("(enter y or n)");
//                        isYOrN = false;
//                    }
//                } while (!isYOrN);
//                #endregion
//            }
//            while (!areAllPCsEntered);
//            #endregion
//            bool isCombatDone = false;
//            do
//            {
//                #region Transfer to DM
//                Console.Clear();
//                Console.WriteLine("\"I admire your courage! Now to see what challenge awaits!\"");
//                Console.WriteLine("\"Good fortune in the battles to come!\"");
//                Console.WriteLine();
//                Console.WriteLine("(please pass the computer to the DM, then press any key to continue)");
//                Console.ReadKey();
//                #endregion

//                //Collecting NPC info
//                #region Collecting NPC info
//                bool areAllNPCsEntered = false;
//                do
//                {
//                    nonPlayerCharacterList.Clear();
//                    #region Get Enemy Name
//                    Console.Clear();
//                    Console.WriteLine("Enter enemy name...");
//                    string enemyName = Console.ReadLine();
//                    #endregion

//                    #region Get Enemy Initiative Bonus
//                    int initBonus = 0;
//                    bool isEnemyBonusInt = false;
//                    Console.WriteLine();
//                    Console.WriteLine($"Enter enemy initiative bonus...");
//                    do
//                    {
//                        string initBonusString = Console.ReadLine();
//                        if (int.TryParse(initBonusString, out initBonus))
//                        {
//                            isEnemyBonusInt = true;
//                        }
//                        else
//                        {
//                            Console.WriteLine();
//                            Console.WriteLine("Please enter an integer value...");
//                        }
//                    }
//                    while (!isEnemyBonusInt);
//                    #endregion

//                    #region Create and Add Enemy
//                    NonPlayerCharacter enemy = new NonPlayerCharacter(enemyName, initBonus);
//                    characterList.Add(enemy);
//                    characterDictionary.Add(enemy.CharacterName, enemy);
//                    nonPlayerCharacterDictionary.Add(enemy.CharacterName, enemy);
//                    nonPlayerCharacterList.Add(enemy);
//                    #endregion

//                    #region Add another enemy?
//                    bool isYOrN = true;
//                    do
//                    {
//                        Console.WriteLine($"");
//                        Console.WriteLine($"(are there more NPCs to enter? <y/n>)");
//                        string morePlayersString = Console.ReadLine().ToLower();
//                        if (morePlayersString == "y")
//                        {
//                            Console.WriteLine($"");
//                            Console.WriteLine("NEXT ENEMY");
//                        }
//                        else if (morePlayersString == "n")
//                        {
//                            areAllNPCsEntered = true;
//                        }
//                        else
//                        {
//                            Console.WriteLine("enter y or n");
//                            isYOrN = false;
//                        }
//                    } while (!isYOrN);
//                    #endregion
//                }
//                while (!areAllNPCsEntered);
//                #endregion

//                //Let's Roll!
//                #region Collecting Individual Rolls & Sorting List

//                #region get rolls
//                foreach (Character character in playerCharacterList)
//                {
//                    int roll = 0;
//                    bool rollIsInt = false;
//                    do
//                    {
//                        Console.WriteLine();
//                        Console.WriteLine($"Enter raw roll for {character.CharacterName}");
//                        string rollString = Console.ReadLine();
//                        if (int.TryParse(rollString, out roll))
//                        {
//                            rollIsInt = true;
//                        }
//                        else
//                        {
//                            Console.WriteLine("please enter an integer value for the roll...");
//                        }
//                    }
//                    while (!rollIsInt);
//                    character.InitiativeRoll = roll;
//                }
//                foreach (Character character in nonPlayerCharacterList) 
//                {
//                    int roll = 0;
//                    bool rollIsInt = false;
//                    do
//                    {
//                        Console.WriteLine();
//                        Console.WriteLine($"Enter raw roll for {character.CharacterName}");
//                        string rollString = Console.ReadLine();
//                        if (int.TryParse(rollString, out roll))
//                        {
//                            rollIsInt = true;
//                        }
//                        else
//                        {
//                            Console.WriteLine("please enter an integer value for the roll...");
//                        }
//                    }
//                    while (!rollIsInt);
//                    character.InitiativeRoll = roll;
//                }
//                #endregion

//                //sort the list
//                #region Sort List
//                List<Character> turnOrder = new List<Character>();
//                turnOrder.AddRange(playerCharacterList);
//                turnOrder.AddRange(nonPlayerCharacterList);

//                turnOrder.Sort(delegate (Character c1, Character c2)
//                {
//                    return c1.InitiativeTotal.CompareTo(c2.InitiativeTotal);
//                });
//                turnOrder.Reverse();
//                #endregion

//                #endregion

//                //Display Turn Order
//                #region Display Turn Order
//                Console.Clear();
//                Console.WriteLine("{0, -30}{1, -30}", "Character Name", "Total Initiative");
//                Console.WriteLine("---------------------------------------------------");
//                foreach (Character character in turnOrder)
//                {
//                    Console.WriteLine("{0, -30}{1, -30}", character.CharacterName.ToString(), character.InitiativeTotal.ToString());
//                }
//                #endregion

//                //Start New Combat?
//                #region Start New Combat or Quit?
//                Console.WriteLine($"");
//                Console.WriteLine($"enter [new combat] for new combat or [done] to quit program");
//                bool isDoneOrNewCombat = false;
//                do
//                {
//                    string continueCombat = Console.ReadLine();
//                    continueCombat.ToLower();
//                    if (continueCombat == "done")
//                    {
//                        isCombatDone = true;
//                        isDoneOrNewCombat = true;
//                    }
//                    else if (continueCombat == "new combat")
//                    {
//                        isDoneOrNewCombat = true;
//                    }
//                }
//                while (!isDoneOrNewCombat);
//                #endregion
//            }
//            while (!isCombatDone);
//        }
//    }
//}


