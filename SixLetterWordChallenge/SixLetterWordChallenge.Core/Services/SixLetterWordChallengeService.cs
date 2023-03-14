using SixLetterWordChallenge.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixLetterWordChallenge.Core.Services
{
    internal class SixLetterWordChallengeService : ISixLetterWordChallengeService
    {
        public IList<string> GetInitialWords(string path)
        {
            return File.ReadLines(path).Distinct().ToList();
        }
    }
}
