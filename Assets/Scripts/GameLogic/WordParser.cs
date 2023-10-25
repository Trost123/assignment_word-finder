using System;
using System.Collections.Generic;
using System.Text;

namespace GameLogic
{
    public class WordParser
    {
        private readonly List<List<string>> _grid;
        private readonly bool[,] _horizontalVisited;
        private readonly bool[,] _verticalVisited;

        public WordParser(List<List<string>> grid)
        {
            _grid = grid ?? throw new ArgumentNullException(nameof(grid));
            int rowCount = _grid.Count;
            int colCount = _grid[0].Count;
            _horizontalVisited = new bool[rowCount, colCount];
            _verticalVisited = new bool[rowCount, colCount];
        }

        public List<Word> ParseWords()
        {
            var words = new List<Word>();
            for (int row = 0; row < _grid.Count; row++)
            {
                for (int col = 0; col < _grid[row].Count; col++)
                {
                    if (!IsNonCharacter(_grid[row][col]))
                    {
                        // Check horizontally
                        var horizontalWord = ExtractWord(row, col, true);
                        if (horizontalWord != null)
                        {
                            words.Add(horizontalWord);
                        }

                        // Check vertically
                        var verticalWord = ExtractWord(row, col, false);
                        if (verticalWord != null)
                        {
                            words.Add(verticalWord);
                        }
                    }
                }
            }

            return words;
        }

        private Word ExtractWord(int row, int col, bool isHorizontal)
        {
            var word = new StringBuilder();
            var coordinates = new List<(int row, int col)>();
            int i = 0;

            while (true)
            {
                int currentRow = isHorizontal ? row : row + i;
                int currentCol = isHorizontal ? col + i : col;

                // Break if out of bounds
                if (currentRow >= _grid.Count || currentCol >= _grid[0].Count)
                {
                    break;
                }

                var letter = _grid[currentRow][currentCol];
                if (IsNonCharacter(letter))
                {
                    break;
                }

                // Skip if this cell has already been visited in the current direction
                if ((isHorizontal && _horizontalVisited[currentRow, currentCol]) ||
                    (!isHorizontal && _verticalVisited[currentRow, currentCol]))
                {
                    break;
                }

                word.Append(letter);
                coordinates.Add((currentRow, currentCol));

                // Mark this cell as visited in the current direction
                if (isHorizontal)
                {
                    _horizontalVisited[currentRow, currentCol] = true;
                }
                else
                {
                    _verticalVisited[currentRow, currentCol] = true;
                }

                i++;
            }

            if (word.Length > 1)
            {
                return new Word(word.ToString(), coordinates);
            }

            return null;
        }

        private bool IsNonCharacter(string value)
        {
            return string.IsNullOrWhiteSpace(value) || value == "_";
        }
    }
}
