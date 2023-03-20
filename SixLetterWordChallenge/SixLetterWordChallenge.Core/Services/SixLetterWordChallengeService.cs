﻿using SixLetterWordChallenge.Core.Entities;
using SixLetterWordChallenge.Core.Interfaces;

namespace SixLetterWordChallenge.Core.Services
{
    public class SixLetterWordChallengeService : ISixLetterWordChallengeService
    {
        // TODO: This method does not belong here. This class shouldn't know/care where the data comes from. Move to another class, e.g. FileReader
        // A class should hold one responsibility.
        // This class is responsible for:
        // * Reading files
        // * Combining words
        public IList<string> GetInitialWords(string path)
        {
            try
            {
                var initialWords = File.ReadLines(path).Distinct().ToList();
                return initialWords;
            }
            catch (Exception)
            {
                return new List<string>();
            }
        }

        public IList<WordWithToPrint> GetCompletedWords(List<string> words)
        {
            if (!words.Any())
                throw new ArgumentNullException($"{nameof(words)} is empty");

            var completedWords = new List<WordWithToPrint>();

            var sixLetterWords = GetSixLetterWords(words);

            var incompleteWords = GetIncompleteWords(words);

            do
            {
                var newIncompleteWords = new List<WordWithToPrint>();

                foreach (var incompleteWord in incompleteWords)
                {
                    foreach (var wordToAdd in words)
                    {
                        var newWord = incompleteWord.Word + wordToAdd;

                        if (newWord.Length == 6 && sixLetterWords.Any(slw => newWord.Contains(slw)))
                        {
                            var newCompleteWord = new WordWithToPrint
                            {
                                Word = newWord,
                                ToPrint = $"{incompleteWord.ToPrint} + {wordToAdd} = {sixLetterWords.FirstOrDefault(w => w == newWord)}"
                            };

                            completedWords.Add(newCompleteWord);
                        }

                        if (newWord.Length < 6 && sixLetterWords.Any(slw => slw.Contains(newWord)))
                        {
                            var newIncompleteWord = new WordWithToPrint
                            {
                                Word = newWord,
                                ToPrint = $"{incompleteWord.ToPrint} + {wordToAdd}"
                            };

                            newIncompleteWords.Add(newIncompleteWord);
                        }
                    }
                }

                incompleteWords.Clear();
                incompleteWords = newIncompleteWords;
            } while (incompleteWords.Any());

            // TODO: Why throw an exception? It should be ok if there are no combinations.
            if (!completedWords.Any())
                // TODO: Also, this exception type is wrong, the only argument in this method is "List<string> words"
                throw new ArgumentNullException($"{nameof(words)} is empty");

            // TODO: Is sorting here necessary?
            // If you really want to sort, do it where you print it.
            return completedWords.OrderBy(x => x.Word).ToList();
        }

        private List<WordWithToPrint> GetIncompleteWords(List<string> words)
        {
            if (!words.Any())
                throw new ArgumentNullException($"{nameof(words)} is empty");

            var incompleteWords = new List<WordWithToPrint>();

            foreach (var word in words)
            {
                if (word.Length < 6)
                {
                    var incompleteWord = new WordWithToPrint
                    {
                        Word = word,
                        ToPrint = word
                    };

                    incompleteWords.Add(incompleteWord);
                }
            }

            if (!incompleteWords.Any())
                throw new ArgumentException($"{nameof(incompleteWords)} is empty");

            return incompleteWords;
        }

        private List<string> GetSixLetterWords(List<string> words)
        {
            if (!words.Any())
                throw new ArgumentNullException($"{nameof(words)} is empty");

            return words.Where(x => x.Length == 6).ToList();
        }
    }
}
