using NHunspell;
using System;
using System.IO;

namespace NHunspellSpellCheck
{
    static class Program
    {
        static void Main(string[] args)
        {

            Console.Write("Enter any food product's name: ");
            string word = Console.ReadLine();
            Check(word);

            Console.ReadLine();
        }

        static void Check(string word)
        {
            string affPath = Path.GetFullPath(@"..\..\Dictionaries\affix.aff");
            string dicPath = Path.GetFullPath(@"..\..\Dictionaries\dictionary.dic");

            using (Hunspell _hunspell = new Hunspell())
            {
                _hunspell.Load(affPath, dicPath);
                // Can add new words directly using this command: "_hunspell.Add(word);"

                var suggestions = _hunspell.Suggest(word);
                int count = suggestions.Count;

                if (count > 0)
                {
                    Console.WriteLine(count.ToString());
                    Console.WriteLine("Did you mean: ");
                    foreach (string suggestion in suggestions)
                    {
                        Console.WriteLine(suggestion);
                    }
                }
                else
                {
                    Console.WriteLine("Sorry, could not find the word in the dictionary.");
                    Console.WriteLine("Would you like to add the word to the dictionary? (Y/N)");

                    string choice = Console.ReadLine();
                    switch(choice)
                    {
                        case "y" :
                        case "Y":
                            _hunspell.Add(word);
                            Console.Write("New word has been added in the dictionary: " + word);
                            break;
                        case "n":
                        case "N":
                            Console.Write("Thank you for using NHunspell.");
                            break;
                        default:
                            Console.Write("Sorry, can't understand your choice.");
                            Console.Write("Application terminating..");
                            break;
                    }
                }
            }
        }
    }
}
