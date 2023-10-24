using System.Collections.Generic;

namespace GameLogic
{
    public class Word
    {
        public string Text { get; }
        public List<(int row, int col)> Coordinates { get; }

        public Word(string text, List<(int row, int col)> coordinates)
        {
            Text = text;
            Coordinates = coordinates;
        }
    }
}