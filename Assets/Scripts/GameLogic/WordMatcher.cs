using System;
using System.Collections.Generic;
using System.Linq;
using Config.Models;
using Interfaces.GameLogic;

namespace GameLogic
{
    public class WordMatcher : IWordMatcher
    {
        private readonly List<Word> _words;
        // Receive the GridConfig in the constructor
        public WordMatcher(GridConfig gridConfig)
        {
            var parser = new WordParser(gridConfig.grids[0].grid);
            _words = parser.ParseWords();
        }

        public Word MatchWord(string word)
        {
            var matchedWord = _words.FirstOrDefault(w => string.Equals(w.Text, word, StringComparison.OrdinalIgnoreCase));
            return matchedWord;
        }
    }
}