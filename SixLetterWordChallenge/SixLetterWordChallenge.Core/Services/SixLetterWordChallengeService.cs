﻿using SixLetterWordChallenge.Core.Entities;
using SixLetterWordChallenge.Core.Interfaces;

namespace SixLetterWordChallenge.Core.Services
{
    internal class SixLetterWordChallengeService : ISixLetterWordChallengeService
    {
        public IList<string> GetInitialWords(string path)
        {
            return File.ReadLines(path).Distinct().ToList();
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


            if (!completedWords.Any())
                throw new ArgumentNullException($"{nameof(words)} is empty");

            return completedWords;
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
                    incompleteWords.Add(
                        new WordWithToPrint
                        {
                            Word = word,
                            ToPrint = word
                        });
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