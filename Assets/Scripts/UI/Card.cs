using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class Card : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI letterText;

        [SerializeField] private Image cardImage; // Reference to the Image component

        [SerializeField] private Color frontColor; // Color for the front side

        [SerializeField] private Color backColor; // Color for the back side

        public bool IsOpen { get; private set; }

        private void Start()
        {
            if (cardImage == null) cardImage = GetComponent<Image>();

            // Set the initial color to the backColor as the card is facing away
            cardImage.color = backColor;
            letterText.gameObject.SetActive(false);
            transform.rotation = Quaternion.Euler(0, 180, 0);
            // FlipCard();
        }

        public void SetLetter(string letter)
        {
            if (letter == "_") letter = "";

            letterText.text = letter;
        }

        public void FlipCard()
        {
            if (IsOpen)
            {
                return;
            }
            
            // Create a new sequence
            var sequence = DOTween.Sequence();

            // Add the first rotation to 90 degrees to the sequence
            sequence.Append(transform.DORotate(new Vector3(0, 90, 0), 0.25f).OnComplete(() =>
            {
                // Change color and enable text at half-flip
                cardImage.color = frontColor;
                letterText.gameObject.SetActive(true);
            }));

            // Add the second rotation from 90 to 0 degrees to the sequence
            sequence.Append(transform.DORotate(new Vector3(0, 0, 0), 0.25f));
            
            IsOpen = true;
        }
    }
}