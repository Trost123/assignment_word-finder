using System.Collections.Generic;
using System.Threading.Tasks;
using UI;
using UnityEngine;

namespace Controllers
{
    public class GridController : MonoBehaviour
    {
        [SerializeField] private GameObject cardPrefab;
        [SerializeField] private Transform gridPanel;
        
        private readonly List<List<Card>> _gridCards = new();

        public void PopulateGrid(List<List<string>> gridData)
        {
            _gridCards.Clear();  // Clear any existing data
            foreach (var row in gridData)
            {
                var cardRow = new List<Card>();
                foreach (var letter in row)
                {
                    var card = Instantiate(cardPrefab, gridPanel);
                    var cardScript = card.GetComponent<Card>();
                    cardScript.SetLetter(letter);
                    cardRow.Add(cardScript);
                }
                _gridCards.Add(cardRow);
            }
        }

        public async Task OpenCellAsync((int row, int col) coordinate)
        {
            var card = _gridCards[coordinate.row][coordinate.col];
            if (card != null && !card.IsOpen)
            {
                card.FlipCard();
                await Task.Delay(500);  // 0.5 seconds delay, adjust as needed
            }
        }
    }
}