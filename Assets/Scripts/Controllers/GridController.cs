using System.Collections.Generic;
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

        public void OpenCell((int row, int col) coordinate)
        {
            var (row, col) = coordinate;
            if (row >= 0 && row < _gridCards.Count && col >= 0 && col < _gridCards[row].Count)
            {
                var card = _gridCards[row][col];
                card.FlipCard();  // Assume FlipCard is the method to open/flip the card
            }
            else
            {
                Debug.LogWarning($"Invalid coordinates: {coordinate}");
            }
        }
    }
}