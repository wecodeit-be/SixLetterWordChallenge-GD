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

            if (!words.Any())
            {
                path = Directory.GetParent(Directory.GetCurrentDirectory())?.Parent?.Parent?.Parent?.Parent?.FullName;
                words = sixLetterWordChallengeService.GetInitialWords(path + input).ToList();
            }

            var challengeWords = sixLetterWordChallengeService.GetCompletedWords(words);

            foreach (var challengeword in challengeWords)
            {
                Console.WriteLine(challengeword.ToPrint);
                challengeWordsToPrint.Add(challengeword.ToPrint);
            }

            Console.WriteLine("How do you want to name the output of this challenge?");

            string fileName = Console.ReadLine();

            File.WriteAllLines(path + "/" + fileName + ".txt", challengeWordsToPrint);
        }
    }
}