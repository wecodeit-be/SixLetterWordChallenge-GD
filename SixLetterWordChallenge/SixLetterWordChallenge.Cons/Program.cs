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

            string input = "/input.txt";

            var path = Directory.GetParent(Directory.GetCurrentDirectory())?.Parent?.Parent?.Parent?.Parent?.FullName;

            var words = sixLetterWordChallengeService.GetInitialWords(path + input).ToList();

            var challengeWords = sixLetterWordChallengeService.GetCompletedWords(words);

            foreach (var challengeword in challengeWords)
            {
                Console.WriteLine(challengeword.ToPrint);
            }
        }
    }
}