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

        public List<Word> ParseWords()
        {
            var words = new List<Word>();
            words.AddRange(ParseWordsFromRows());
            words.AddRange(ParseWordsFromColumns());
            return words;
        }

        // Other methods remain the same...

        private IEnumerable<Word> ParseWordsFromStringList(List<string> strList, bool isRow, int index)
        {
            var words = new List<Word>();
            var word = new StringBuilder();
            var coordinates = new List<(int row, int col)>();
            for (int i = 0; i < strList.Count; i++)
            {
                var letter = strList[i];
                if (string.IsNullOrWhiteSpace(letter) || letter == "_")
                {
                    if (word.Length > 1)  // Check if the word length is greater than 1
                    {
                        words.Add(new Word(word.ToString(), new List<(int row, int col)>(coordinates)));
                        word.Clear();
                        coordinates.Clear();
                    }
                }
                else
                {
                    word.Append(letter);
                    coordinates.Add(isRow ? (index, i) : (i, index));
                }
            }
            if (word.Length > 1)  // Check if the word length is greater than 1
            {
                words.Add(new Word(word.ToString(), new List<(int row, int col)>(coordinates)));
            }
            return words;
        }

        private IEnumerable<Word> ParseWordsFromRows()
        {
            var words = new List<Word>();
            for (int i = 0; i < _grid.Count; i++)
            {
                words.AddRange(ParseWordsFromStringList(_grid[i], true, i));
            }
            return words;
        }

        private IEnumerable<Word> ParseWordsFromColumns()
        {
            var words = new List<Word>();
            int rowCount = _grid.Count;
            int colCount = _grid[0].Count;

            for (int col = 0; col < colCount; col++)
            {
                var column = new List<string>();
                for (int row = 0; row < rowCount; row++)
                {
                    column.Add(_grid[row][col]);
                }
                words.AddRange(ParseWordsFromStringList(column, false, col));
            }
            return words;
        }
    }
}