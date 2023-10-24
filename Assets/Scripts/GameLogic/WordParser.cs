using System.Collections.Generic;
using System.Text;

namespace GameLogic
{
    public class WordParser
    {
        private readonly List<List<string>> _grid;

        public WordParser(List<List<string>> grid)
        {
            _grid = grid ?? throw new System.ArgumentNullException(nameof(grid));
        }

        public List<string> ParseWords()
        {
            var words = new List<string>();
            words.AddRange(ParseWordsFromRows());
            words.AddRange(ParseWordsFromColumns());
            return words;
        }

        private IEnumerable<string> ParseWordsFromRows()
        {
            var words = new List<string>();
            foreach (var row in _grid)
            {
                words.AddRange(ParseWordsFromStringList(row));
            }
            return words;
        }

        private IEnumerable<string> ParseWordsFromColumns()
        {
            var words = new List<string>();
            int rowCount = _grid.Count;
            int colCount = _grid[0].Count;

            for (int col = 0; col < colCount; col++)
            {
                var column = new List<string>();
                for (int row = 0; row < rowCount; row++)
                {
                    column.Add(_grid[row][col]);
                }
                words.AddRange(ParseWordsFromStringList(column));
            }
            return words;
        }

        private IEnumerable<string> ParseWordsFromStringList(List<string> strList)
        {
            var words = new List<string>();
            var word = new StringBuilder();
            foreach (var letter in strList)
            {
                if (string.IsNullOrWhiteSpace(letter) || letter == "_")
                {
                    if (word.Length > 0)
                    {
                        words.Add(word.ToString());
                        word.Clear();
                    }
                }
                else
                {
                    word.Append(letter);
                }
            }
            if (word.Length > 0)
            {
                words.Add(word.ToString());
            }
            return words;
        }
    }
}