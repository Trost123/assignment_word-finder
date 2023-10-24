using System.Collections.Generic;
using UI;
using UnityEngine;

namespace Controllers
{
    public class GridController : MonoBehaviour
    {
        [SerializeField] private GameObject cardPrefab;

        [SerializeField] private Transform gridPanel;

        public void PopulateGrid(List<List<string>> gridData)
        {
            foreach (var row in gridData)
            foreach (var letter in row)
            {
                var card = Instantiate(cardPrefab, gridPanel);
                // Assume Card is a script to handle individual card logic
                var cardScript = card.GetComponent<Card>();
                cardScript.SetLetter(letter);
            }
        }
    }
}