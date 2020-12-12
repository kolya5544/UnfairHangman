using System;
using System.Collections.Generic;
using System.IO;

namespace UnfairHangman
{
    class Program
    {
        public static string[] wordlist = Properties.Resources.words.Split('\n');
        public static Random rng = new Random();

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to *unfair* hangman.");
            Console.WriteLine("Made by: kolya5544");
            Console.WriteLine("You can only play against CPU. It will choose a word and will try its hardest to cheat and make you lose.");
            Console.WriteLine("Good luck!");
            Console.WriteLine();
            Console.WriteLine("Press 'Enter' to start the game.");
            Console.ReadLine();
            Console.WriteLine("Word generation...");
            List<string> fitting = new List<string>();
            int length = rng.Next(5, 12);
            for (int i = 0; i<wordlist.Length; i++)
            {
                string w = wordlist[i];
                if (w.Length != (length + 1)) continue;
                fitting.Add(w);
            }
            Console.Clear();
            int lives = 6;
            Dictionary<char, bool> guesses = new Dictionary<char, bool>();
            string realWord = null;
            while (lives > 0)
            {
                Console.Clear();

                if (realWord == null)
                {
                    for (int i = 0; i<length; i++)
                    {
                        Console.Write("_ ");
                    }
                } else
                {
                    for (int i = 0; i<length; i++)
                    {
                        Console.Write((guesses.ContainsKey(realWord[i]) ? realWord[i].ToString() : "_") + " ");
                    }
                }
                Console.WriteLine(); Console.WriteLine();
                WriteCurrentHangman(lives);

                Console.Write("Incorrect guesses: ");
                foreach (KeyValuePair<char, bool> kvp in guesses)
                {
                    if (!kvp.Value)
                    {
                        Console.Write(kvp.Key + " ");
                    }
                }
                Console.WriteLine();
                while (true)
                {
                    Console.Write("Your guess (letters only): ");
                    string guess = Console.ReadLine();
                    if (guess.Length == 1)
                    {
                        char g = guess[0];
                        if (guesses.ContainsKey(g))
                        {
                            Console.WriteLine("Didnt you already play this letter?");
                        } else
                        {
                            if (realWord == null)
                            {
                                //fun is here
                                List<string> wordsAfterApplyingStuff = new List<string>();
                                for (int i = 0; i < fitting.Count; i++)
                                {
                                    string wrd = fitting[i];
                                    bool flag = true;
                                    for (int b = 0; b < wrd.Length; b++)
                                    {
                                        if (guesses.ContainsKey(wrd[b]) || wrd[b] == g)
                                        {
                                            flag = false; break;
                                        }
                                    }
                                    if (flag) wordsAfterApplyingStuff.Add(wrd);
                                }
                                if (wordsAfterApplyingStuff.Count != 0)
                                {
                                    fitting = wordsAfterApplyingStuff;
                                    Console.WriteLine("Wrong letter. Nice try!");
                                    guesses.Add(g, false);
                                    break;
                                }
                                else
                                {
                                    realWord = fitting[rng.Next(0, fitting.Count)];
                                    if (realWord.Contains(g))
                                    {
                                        Console.WriteLine("You got it right. Lucky one!");
                                        guesses.Add(g, true);
                                        break;
                                    }
                                }
                            } else
                            {
                                if (realWord.Contains(g))
                                {
                                    Console.WriteLine("You got it right. Lucky one!");
                                    guesses.Add(g, true);
                                    lives++;
                                    break;
                                } else
                                {
                                    Console.WriteLine("Wrong letter. Nice try!");
                                    guesses.Add(g, false);
                                    break;
                                }
                            }
                        }
                    } else
                    {
                        Console.WriteLine("You can only guess letters one after another.");
                    }
                }
                Console.WriteLine("Press 'Enter'...");
                Console.ReadLine();
                lives--;
            }
            Console.Clear();
            bool lost = realWord == null;
            if (realWord != null)
            {
                foreach (char c in realWord)
                {
                    if (!guesses.ContainsKey(c))
                    {
                        lost = true; break;
                    }
                }
            }
            if (lost)
            {
                Console.WriteLine($"You've lost! The word was {(realWord == null ? fitting[rng.Next(0, fitting.Count - 1)] : realWord)}");
            } else
            {
                Console.WriteLine($"You've won! (how?) The word was '{realWord}'");
            }
            Console.WriteLine("Your guesses were:");
            foreach (KeyValuePair<char, bool> kvp in guesses)
            {
                Console.WriteLine($"'{kvp.Key}' - {(kvp.Value ? "correct" : "incorrect")}");
            }
            Console.WriteLine(); Console.WriteLine();
            if (lost) { WriteCurrentHangman(0); } else { WriteCurrentHangman(-1); }
            Console.WriteLine();
            Console.Write("Press 'Enter' to end the game");
            Console.ReadLine();
        }

        private static void WriteCurrentHangman(int lives)
        {
            string[] states =
            {
                 Properties.Resources.StateFirst,
                 Properties.Resources.StateSecond,
                 Properties.Resources.StateThird,
                 Properties.Resources.StateFourth,
                 Properties.Resources.StateFifth,
                 Properties.Resources.StateSixth,
                 Properties.Resources.StateDead,
                 Properties.Resources.StateSaved
            };
            Console.WriteLine(states[states.Length - lives - 2]);
        }
    }
}
