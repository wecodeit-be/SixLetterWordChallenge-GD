using SixLetterWordChallenge.Core.Entities;
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
        // TODO: Suggestion: Change return type to IEnumerable<string>, that way you can enumerate the results while the file is being read
        public IList<string> GetInitialWords(string path)
        {
            try
            {
                // TODO: Using ToList means it will fully read the file in memory.
                // Suggestion: Use enumerator.
                var initialWords = File.ReadLines(path).Distinct().ToList();
                return initialWords;
            }
            // TODO: Swallowing exceptions is never a good idea.
            catch (Exception)
            {
                return new List<string>();
            }
        }

        // TODO: This method does a lot
        // * Get list of 6 letter words
        // * Get list of incomplete words
        // * Determination of (in)complete words
        public IList<WordWithToPrint> GetCompletedWords(List<string> words)
        {
            // TODO: You could just return an empty list if there aren't any words
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
            // TODO: Not necessary, you already check this in the calling method
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

            // TODO: You throw an exception here, but not in GetSixLetterWords.
            // Same comments apply, no need to throw an exception.
            if (!incompleteWords.Any())
                // TODO: Wrong exception type
                throw new ArgumentException($"{nameof(incompleteWords)} is empty");

            return incompleteWords;
        }

        private List<string> GetSixLetterWords(List<string> words)
        {
            // TODO: Not necessary, you already check this in the calling method
            if (!words.Any())
                throw new ArgumentNullException($"{nameof(words)} is empty");

            return words.Where(x => x.Length == 6).ToList();
        }
    }
}
