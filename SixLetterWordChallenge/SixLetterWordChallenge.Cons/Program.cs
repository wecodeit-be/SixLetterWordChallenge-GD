using Microsoft.Extensions.DependencyInjection;
using SixLetterWordChallenge.Core.Interfaces;
using SixLetterWordChallenge.Core.Services;

namespace SixLetterWordChallenge.Cons
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                .AddScoped<ISixLetterWordChallengeService, SixLetterWordChallengeService>()
                .BuildServiceProvider();

            var sixLetterWordChallengeService = serviceProvider.GetRequiredService<ISixLetterWordChallengeService>();

            List<string> challengeWordsToPrint = new();

            string input = "/input.txt";

            var path = Directory.GetCurrentDirectory();

            var words = sixLetterWordChallengeService.GetInitialWords(path + input).ToList();

            // TODO: Why is this here? It should never be empty. Copy input.txt when building. (Build action)
            if (!words.Any())
            {
                // TODO: Let's ignore the fact that this should not be here for a second.
                // Following constructs should be avoided: Parent?.Parent?.Parent?.Parent?.FullName
                // It's not super easy to read what this does.
                path = Directory.GetParent(Directory.GetCurrentDirectory())?.Parent?.Parent?.Parent?.Parent?.FullName;
                words = sixLetterWordChallengeService.GetInitialWords(path + input).ToList();
            }

            var challengeWords = sixLetterWordChallengeService.GetCompletedWords(words);

            foreach (var challengeword in challengeWords)
            {
                Console.WriteLine(challengeword.ToPrint);
                
                // TODO: This list has no purpose. See comment on line 49
                challengeWordsToPrint.Add(challengeword.ToPrint);
            }

            Console.WriteLine("How do you want to name the output of this challenge?");

            string fileName = Console.ReadLine();

            // Use LINQ instead of a new list
            File.WriteAllLines(path + "/" + fileName + ".txt", challengeWordsToPrint);
        }
    }
}
