using System.Collections.Generic;
using System.Linq;
using GameLogic;
using NUnit.Framework;

namespace Editor.UnitTests
{
    [TestFixture]
    public class WordParserTests
    {
        [SetUp]
        public void Setup()
        {
            _grid = new List<List<string>>
            {
                new() { "_", "_", "_", "_", "_", "_", "_", "_", "_", "_" },
                new() { "_", "C", "A", "T", "A", "L", "Y", "S", "T", "_" },
                new() { "_", "E", "_", "R", "_", "_", "_", "_", "R", "_" },
                new() { "_", "L", "_", "I", "_", "_", "A", "_", "E", "_" },
                new() { "_", "I", "_", "A", "_", "_", "C", "_", "M", "_" },
                new() { "_", "B", "_", "N", "I", "C", "E", "_", "B", "_" },
                new() { "F", "A", "N", "G", "_", "A", "_", "_", "L", "_" },
                new() { "_", "T", "_", "L", "_", "R", "_", "_", "E", "_" },
                new() { "_", "E", "_", "E", "S", "T", "E", "R", "_", "_" },
                new() { "_", "_", "_", "_", "_", "_", "_", "_", "_", "_" }
            };
            _wordParser = new WordParser(_grid);
            _expectedWords = new List<Word>
            {
                new("FANG", new List<(int, int)> { (6, 0), (6, 1), (6, 2), (6, 3) }),
                new("NICE", new List<(int, int)> { (5, 3), (5, 4), (5, 5), (5, 6) }),
                new("CELIBATE",
                    new List<(int, int)> { (1, 1), (2, 1), (3, 1), (4, 1), (5, 1), (6, 1), (7, 1), (8, 1) }),
                new("CART", new List<(int, int)> { (5, 5), (6, 5), (7, 5), (8, 5) }),
                new("TREMBLE", new List<(int, int)> { (1, 8), (2, 8), (3, 8), (4, 8), (5, 8), (6, 8), (7, 8) }),
                new("CATALYST",
                    new List<(int, int)> { (1, 1), (1, 2), (1, 3), (1, 4), (1, 5), (1, 6), (1, 7), (1, 8) }),
                new("ACE", new List<(int, int)> { (3, 6), (4, 6), (5, 6) }),
                new("TRIANGLE",
                    new List<(int, int)> { (1, 3), (2, 3), (3, 3), (4, 3), (5, 3), (6, 3), (7, 3), (8, 3) }),
                new("ESTER", new List<(int, int)> { (8, 3), (8, 4), (8, 5), (8, 6), (8, 7) })
            };
        }

        private List<List<string>> _grid;
        private WordParser _wordParser;
        private List<Word> _expectedWords;

        [Test]
        public void ParseWords_ShouldReturnCorrectWordAssets()
        {
            // Act
            var parsedWordsTexts = _wordParser.ParseWords().Select(w => w.Text).ToList();

            // Assert
            foreach (var expectedWordText in _expectedWords.Select(w => w.Text))
                Assert.IsTrue(parsedWordsTexts.Contains(expectedWordText),
                    $"Expected to find '{expectedWordText}' in parsed words.");
        }

        [Test]
        public void ParseWords_ShouldReturnCorrectCoordinatesForWords()
        {
            // Act
            var parsedWords = _wordParser.ParseWords();

            // Assert
            foreach (var expectedWord in _expectedWords)
            {
                var parsedWord = parsedWords.FirstOrDefault(w => w.Text == expectedWord.Text);
                Assert.IsNotNull(parsedWord, $"Expected to find word '{expectedWord.Text}' in parsed words.");
                Assert.IsTrue(expectedWord.Coordinates.SequenceEqual(parsedWord.Coordinates),
                    $"Coordinates for '{expectedWord.Text}' do not match.");
            }
        }
    }
}