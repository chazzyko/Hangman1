using System;
using System.Collections;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace HangmanGame
{
    public class HangmanGame
    {
        string[] cities = new string[16] { "adelaide", "athens", "barselona", "boston", "bristol", "birmingham", "detroit", "dublin", "funchal", "gdansk", "helsinki", "irkutsk", "kaunas", "liverpool", "lisbon", "vilniius"};
        string[] bodyParts = new string[6] { "0", "/", "|", "\\", "/", "\\" };
        public const int maxWrongGuesses = 6;
        int wrongGuessCount = 0;
        int correctLetters = 0;
        ArrayList foundLetters = new ArrayList(){};
        ArrayList guessedLetters = new ArrayList(){};
        public string randomWord = "";

        public HangmanGame()
        {
            randomWord = SelectCity();
        }

        private string SelectCity()
        {
            var random = new Random();
            return cities[random.Next(cities.Length)];
        }

        public void DrawHangman()
        {
            string bodyPartsIn = "";
            for (int i = 0; i < bodyParts.Length; i++)
            {
                if (i < wrongGuessCount)
                {
                    bodyPartsIn = bodyPartsIn + bodyParts[i];
                }
                else
                {
                    bodyPartsIn = bodyPartsIn + " ";
                }
            }

            Console.WriteLine($" +---+");
            Console.WriteLine($" {bodyPartsIn[0]}   |");
            Console.WriteLine($"{bodyPartsIn[1]}{bodyPartsIn[2]}{bodyPartsIn[3]}  |");
            Console.WriteLine($"{bodyPartsIn[4]} {bodyPartsIn[5]}  |");
            Console.WriteLine($"   =====");

        }

        public void ShowWord()
        {
            for (int i = 0; i < randomWord.Length; i++)
            {
                var contains = foundLetters.Contains("" + randomWord[i]);

                if (contains)
                {
                    Console.Write($" {randomWord[i]} ");
                }
                else
                {
                    Console.Write(" _ ");
                }

            }

            Console.WriteLine("");
        }

        public bool IsLetterGuessed(string letter)
        {
            return guessedLetters.Contains("" + letter);
        }

        public bool IsLetterInWord(string letter)
        {
            return randomWord.Contains("" + letter);
        }

        public void CountCorrectLetters(string letter)
        {
            foreach(char charLetter in randomWord)
            {
                String charString = charLetter.ToString();
                if (charString == letter.ToString())
                {
                    correctLetters++;
                }
            }
        }

        public string RandomWord
        {
            get { return randomWord; }
            set { randomWord = value; }
        }

        public void Run()
        {
            //Console.WriteLine(randomWord);

            while (true)
            {
                ShowWord();
                DrawHangman();

                if ( wrongGuessCount >= maxWrongGuesses)
                {
                    Console.WriteLine("You lost.\n");
                    Console.WriteLine($"Word was {randomWord}");
                    break;
                }

                if (correctLetters == randomWord.Length)
                {
                    Console.WriteLine("You won.\n");
                    break;
                }

                Console.WriteLine("Guess your letter.\n");
                var input = Console.ReadLine();
                Console.WriteLine("");

                if (input.Equals("exit", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("Game exit.\n");
                    break;
                }                

                if (!IsLetterGuessed(input))
                {
                    guessedLetters.Add(input);

                    if (IsLetterInWord(input))
                    {                        
                        foundLetters.Add(input);
                        CountCorrectLetters(input);
                    }
                    else
                    {
                        wrongGuessCount++;
                        Console.WriteLine($"Wrong guess. Letter {input} was not found in the word.\n");
                    }
                }
                else
                {
                    Console.WriteLine($"Letter {input} was already guessed.\n");
                }
            }
        }
    }

    public class HangmanProgram
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Hangman game 1.0 ====");
            HangmanGame game = new HangmanGame();
            game.Run();
        }

    }
}
