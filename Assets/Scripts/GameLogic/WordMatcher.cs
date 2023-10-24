using System;
using System.Collections.Generic;
using System.Linq;
using Config.Models;
using Interfaces.GameLogic;
using UnityEngine;

namespace GameLogic
{
    public class WordMatcher : IWordMatcher
    {
        private List<string> words;
        // Receive the GridConfig in the constructor
        public WordMatcher(GridConfig gridConfig)
        {
            var parser = new WordParser(gridConfig.grids[0].grid);
            words = parser.ParseWords();
        }

        public bool MatchWord(string word)
        {
            var wordExists = words.Any(w => string.Equals(w, word, StringComparison.OrdinalIgnoreCase));
            Debug.Log("Word matching: " + word + " " + wordExists);
            return wordExists;
        }
    }
}