using SixLetterWordChallenge.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixLetterWordChallenge.Core.Interfaces
{
    public interface ISixLetterWordChallengeService
    {
        IList<string> GetInitialWords(string path);
        IList<WordWithToPrint> GetCompletedWords(List<string> words);
    }
}
