using System;
using System.Collections.Generic;
using System.Linq;
using Interfaces.GameLogic;

namespace GameLogic
{
    public class WordMatcher : IWordMatcher
    {
        private readonly List<Word> _words;

        // Receive the GridConfig in the constructor
        public WordMatcher(List<List<string>> grid)
        {
            var parser = new WordParser(grid);
            _words = parser.ParseWords();
        }

        public Word MatchAndRemoveWord(string word)
        {
            var matchedWord =
                _words.FirstOrDefault(w => string.Equals(w.Text, word, StringComparison.OrdinalIgnoreCase));
            _words.Remove(matchedWord);
            return matchedWord;
        }
    }
}