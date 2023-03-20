using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixLetterWordChallenge.Core.Entities
{
    // TODO: Confusing name. Combining properties as a class name doesn't really mean anything.
    public class WordWithToPrint
    {
        public string Word { get; set; }
        public string ToPrint { get; set; }
    }
}
