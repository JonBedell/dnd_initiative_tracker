using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitiativeTrackerCLI
{
    public class CLIHelper
    {
        public static DateTime GetDateTime(string message)
        {
            string userInput = String.Empty;
            DateTime dateValue = DateTime.MinValue;
            int numberOfAttempts = 0;

            do
            {
                if (numberOfAttempts > 0)
                {
                    Console.WriteLine("Invalid date format. Please try again");
                }
                Console.WriteLine(message + " ");
                userInput = Console.ReadLine();
                numberOfAttempts++;
            }
            while (!DateTime.TryParse(userInput, out dateValue));

            return dateValue;
        }

        public static int GetInteger(string message)
        {
            string userInput = String.Empty;
            int intValue = 0;
            int numberOfAttempts = 0;

            do
            {
                if (numberOfAttempts > 0)
                {
                    Console.WriteLine("Invalid input format. Please try again");
                }

                Console.Write(message + " ");
                userInput = Console.ReadLine();
                numberOfAttempts++;
            }
            while (!int.TryParse(userInput, out intValue));

            return intValue;

        }


        public static double GetDouble(string message)
        {
            string userInput = String.Empty;
            double doubleValue = 0.0;
            int numberOfAttempts = 0;

            do
            {
                if (numberOfAttempts > 0)
                {
                    Console.Write("Invalid input format. Please try again");
                }

                Console.Write(message + " ");
                userInput = Console.ReadLine();
                numberOfAttempts++;
            }
            while (!double.TryParse(userInput, out doubleValue));

            return doubleValue;

        }

        public static bool GetBool(string message)
        {
            string userInput = String.Empty;
            bool boolValue = false;
            int numberOfAttempts = 0;

            do
            {
                if (numberOfAttempts > 0)
                {
                    Console.WriteLine("Invalid input format. Please try again");
                }

                Console.Write(message + " ");
                userInput = Console.ReadLine();
                numberOfAttempts++;
            }
            while (!bool.TryParse(userInput, out boolValue));

            return boolValue;
        }

        public static string GetString(string message)
        {
            string userInput = String.Empty;
            int numberOfAttempts = 0;

            do
            {
                if (numberOfAttempts > 0)
                {
                    Console.WriteLine("Invalid input format. Please try again");
                }
                
                Console.Write(message + " ");
                userInput = Console.ReadLine();
                numberOfAttempts++;
            }
            while (String.IsNullOrEmpty(userInput));

            return userInput;
        }

        /// <summary>
        /// Prompt user for an interger within a range or a quit character.  Determine whether you want the input to be a ReadKey or ReadLine.
        /// </summary>
        /// <param name="message">Prompt Message for user</param>
        /// <param name="minValue">Smallest accepted value for user input</param>
        /// 
        /// <param name="maxValue">Largest accepted value for user input</param>
        /// <param name="quitLetter">Character or String that will be accepted</param>
        /// <param name="isReadKey">True for a Console.Readkey (i.e. values are les than 10) False for Console.Readline</param>
        /// <returns>Returns a string that is either the int value as a string or the acceptable string/char</returns>
        public static string GetIntInRangeOrQ(string message, int minValue, int maxValue, string quitLetter, bool isReadKey)
        {
            string result = "";
            bool isDone = false;
            int numberOfAttempts = 0;
            string userInput;
            int userInputInt = 0;
            
            do
            {
                if (numberOfAttempts > 0)
                {
                    Console.WriteLine("");
                    Console.WriteLine("Invalid input format. Please try again");
                    Console.WriteLine("");
                }

                Console.WriteLine(message + " ");

                if (isReadKey)
                {
                    userInput = Console.ReadKey().KeyChar.ToString();
                }
                else
                {
                    userInput = Console.ReadLine();
                }

                if(userInput.ToLower() == quitLetter.ToLower())
                {
                    result = quitLetter.ToLower();
                    isDone = true;
                }
                else
                {
                    bool isInteger = int.TryParse(userInput, out userInputInt);
                    if(isInteger && userInputInt >= minValue && userInputInt <= maxValue)
                    {
                        result = userInputInt.ToString();
                        isDone = true;
                    }
                }

                numberOfAttempts++;
            }
            while (!isDone);

            return result;
        }

        /// <summary>
        /// Prompt user for an interger within a range.  Determine whether you want the input to be a ReadKey or ReadLine.
        /// </summary>
        /// <param name="message">Prompt Message for user</param>
        /// <param name="minValue">Smallest accepted value for user input</param>
        /// <param name="maxValue">Largest accepted value for user input</param>
        /// <param name="isReadKey">True for a Console.Readkey (i.e. values are les than 10) False for Console.Readline</param>
        /// <returns>Returns a string that is either the int value as a string or the acceptable string/char</returns>
        public static int GetIntInRange(string message, int minValue, int maxValue, bool isReadKey)
        {
            int result = minValue;
            bool isDone = false;
            int numberOfAttempts = 0;
            string userInput;
            int userInputInt = 0;

            do
            {
                if (numberOfAttempts > 0)
                {
                    Console.WriteLine("");
                    Console.WriteLine("Invalid input format. Please try again");
                    Console.WriteLine("");
                }

                Console.WriteLine(message + " ");

                if (isReadKey)
                {
                    userInput = Console.ReadKey().KeyChar.ToString();
                }
                else
                {
                    userInput = Console.ReadLine();
                }


                bool isInteger = int.TryParse(userInput, out userInputInt);
                if (isInteger && userInputInt >= minValue && userInputInt <= maxValue)
                {
                    result = userInputInt;
                    isDone = true;
                }

                numberOfAttempts++;
            }
            while (!isDone);

            return result;
        }

        /// <summary>
        /// Prompt user for an interger within a range.  Determine whether you want the input to be a ReadKey or ReadLine.
        /// </summary>
        /// <param name="message">Prompt Message for user</param>
        /// <param name="minValue">Smallest accepted value for user input</param>
        /// <param name="maxValue">Largest accepted value for user input</param>
        /// <param name="isReadKey">True for a Console.Readkey (i.e. values are les than 10) False for Console.Readline</param>
        /// <returns>Returns a string that is either the int value as a string or the acceptable string/char</returns>
        public static double GetDoubleInRange(string message, double minValue, double maxValue)
        {
            double result = minValue-1;
            bool isDone = false;
            int numberOfAttempts = 0;
            string userInput;
            double userInputDbl = minValue-1;

            do
            {
                if (numberOfAttempts > 0)
                {
                    Console.WriteLine("");
                    Console.WriteLine("Invalid input format. Please try again");
                    Console.WriteLine("");
                }

                Console.WriteLine(message + " ");

                userInput = Console.ReadLine();

                bool isDouble = double.TryParse(userInput, out userInputDbl);
                if (isDouble && userInputDbl >= minValue && userInputDbl <= maxValue)
                {
                    result = userInputDbl;
                    isDone = true;
                }

                numberOfAttempts++;
            }
            while (!isDone);

            return result;
        }


        /// <summary>
        /// Prompt user for an interger included in a list of integers or a quit character.  Determine whether you want the input to be a ReadKey or ReadLine.
        /// </summary>
        /// <param name="message">Prompt Message for user</param>
        /// <param name="ints">List of integers that are allowed to be selected</param>
        /// <param name="quitLetter">Character or String that will be accepted</param>
        /// <param name="isReadKey">True for a Console.Readkey (i.e. values are les than 10) False for Console.Readline</param>
        /// <returns>Returns a string that is either the int value as a string or the acceptable string/char</returns>
        public static string GetIntInListOrQ(string message, List<int> ints, string quitLetter, bool isReadKey)
        {
            string result = "";
            bool isDone = false;
            int numberOfAttempts = 0;
            string userInput;
            int userInputInt = 0;

            do
            {
                if (numberOfAttempts > 0)
                {
                    Console.WriteLine("");
                    Console.WriteLine("Invalid input format. Please try again");
                    Console.WriteLine("");
                }

                Console.WriteLine(message + " ");

                if (isReadKey)
                {
                    userInput = Console.ReadKey().KeyChar.ToString();
                }
                else
                {
                    userInput = Console.ReadLine();
                }

                if (userInput.ToLower() == quitLetter.ToLower())
                {
                    result = quitLetter.ToLower();
                    isDone = true;
                }
                else
                {
                    bool isInteger = int.TryParse(userInput, out userInputInt);

                    if (isInteger && ints.Contains(userInputInt))
                    {
                        result = userInputInt.ToString();
                        isDone = true;
                    }
                }

                numberOfAttempts++;
            }
            while (!isDone);

            return result;
        }


        /// <summary>
        /// Get a bool value from a user by having them enter a custom true/false word (i.e. yes/no)
        /// </summary>
        /// <param name="message">User prompt</param>
        /// <param name="trueWord">User input that will return a "true" value</param>
        /// <param name="falseWord">User input that will return a "false" value</param>
        /// <returns></returns>
        public static bool GetBoolCustom(string message, string trueWord, string falseWord)
        {
            string userInput = String.Empty;
            bool boolValue = false;
            int numberOfAttempts = 0;
            bool goodInput = false;

            while (!goodInput)
            {
                if (numberOfAttempts > 0)
                {
                    Console.WriteLine("Invalid input format. Please try again");
                }

                Console.Write($"{message} <{trueWord}/{falseWord}>");
                Console.WriteLine();
                userInput = Console.ReadLine();
                if( userInput.ToLower() == trueWord.ToLower() || userInput.ToLower() == falseWord.ToLower())
                {
                    goodInput = true;
                }

                numberOfAttempts++;
            }

            if (userInput == trueWord)
            {
                boolValue = true;
            }
                      

            return boolValue;
        }

        /// <summary>
        /// Request string input from user for a value "x"
        /// </summary>
        /// <param name="message">Message Prompt</param>
        /// <param name="x">Acceptable String Value</param>
        /// <returns></returns>
        public static string GetX(string message, string x)
        {
            string userInput = String.Empty;
            int numberOfAttempts = 0;
            bool goodInput = false;

            while (!goodInput)
            {
                if (numberOfAttempts > 0)
                {
                    Console.SetCursorPosition(0, Console.CursorTop - 2);
                    ClearCurrentConsoleLine();
                    Console.WriteLine($"Invalid input. Please choose {x} to continue");
                }

                Console.Write(message);
                Console.WriteLine();
                userInput = Console.ReadKey().KeyChar.ToString();
                if (userInput.ToLower() == x.ToLower())
                {
                    goodInput = true;
                }

                numberOfAttempts++;
            }

            return userInput;
        }

        /// <summary>
        /// Request string input from user for a value "x" or "y"
        /// </summary>
        /// <param name="message">Message Prompt</param>
        /// <param name="x">Acceptable String Value 1</param>
        /// <param name="y">Acceptable String Value 2</param>
        /// <returns></returns>
        public static string GetXorY(string message, string x, string y)
        {
            string userInput = String.Empty;
            int numberOfAttempts = 0;
            bool goodInput = false;

            while (!goodInput)
            {
                if (numberOfAttempts > 0)
                {
                    Console.WriteLine($"Invalid input. Please Choose {x} or {y}");
                }

                Console.Write($"{message} <{x}/{y}>");
                Console.WriteLine();
                userInput = Console.ReadKey().ToString();
                if (userInput.ToLower() == x.ToLower() || userInput.ToLower() == y.ToLower())
                {
                    goodInput = true;
                }

                numberOfAttempts++;
            }
            
            return userInput;
        }

        /// <summary>
        /// Request string input from user for a value "x", "y", or "z"
        /// </summary>
        /// <param name="message">Message Prompt</param>
        /// <param name="x">Acceptable String Value 1</param>
        /// <param name="y">Acceptable String Value 2</param>
        /// <param name="y">Acceptable String Value 3</param
        public static string GetXorYorZ(string message, string x, string y, string z, bool isReadKey)
        {
            string userInput = String.Empty;
            int numberOfAttempts = 0;
            bool goodInput = false;

            while (!goodInput)
            {
                if (numberOfAttempts > 0)
                {
                    Console.WriteLine($"Invalid input. Please Choose {x},{y}, or {z}");
                }

                Console.Write($"{message} <{x}/{y}/{z}>");
                Console.WriteLine();
                if (isReadKey)
                {
                    userInput = Console.ReadKey().ToString();
                }
                else
                {
                    userInput = Console.ReadLine();
                }

                if (userInput.ToLower() == x.ToLower() || userInput.ToLower() == y.ToLower() || userInput.ToLower() == z.ToLower())
                {
                    goodInput = true;
                }

                numberOfAttempts++;
            }

            return userInput;
        }

        /// <summary>
        /// Have console write text in center of console window.  Credit to Will K.!
        /// </summary>
        /// <param name="message">Text To Display</param>
        public static void CenteredWriteline(string message)
        {
            Console.SetCursorPosition((Console.WindowWidth - message.Length) / 2, Console.CursorTop);
            Console.WriteLine(message);
        }

        /// <summary>
        /// Clear the current line of the console
        /// </summary>
        public static void ClearCurrentConsoleLine()
        {
            int currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, currentLineCursor);
        }

    }
}
