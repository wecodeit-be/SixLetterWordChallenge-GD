using SixLetterWordChallenge.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixLetterWordChallenge.Core.Interfaces
{
    // TODO: The name of this interface doesn't mean anything. What does this interface stand for? 
    // Example: IWordCombinationService, this tells me that it's used to make combinations with words.
    public interface ISixLetterWordChallengeService
    {
        IList<string> GetInitialWords(string path);
        IList<WordWithToPrint> GetCompletedWords(List<string> words);
    }
}
