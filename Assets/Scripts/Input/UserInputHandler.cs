using System;
using DG.Tweening;
using Interfaces.GameLogic;
using Interfaces.Input;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Import the namespace for IUIManager

namespace Input
{
    public class UserInputHandler : MonoBehaviour, IUserInputHandler
    {
        [SerializeField] private TMP_InputField inputField;

        [SerializeField] private Button okButton;

        private IWordMatcher _wordMatcher;

        private void Awake()
        {
            okButton.onClick.AddListener(HandleButtonSubmit);
            inputField.onEndEdit.AddListener(OnSubmit);
        }

        public event Action<string> WordSubmitted;

        public void ShakeInputField()
        {
            // Parameters for the shake
            var duration = 0.5f; // Duration of the shake
            var strength = new Vector3(5f, 0f, 0f); // Strength of the shake (X axis only)
            var vibrato = 10; // Amount of shake segments
            float randomness = 0; // Randomness of shake (0 to 180)

            // Perform the shake
            inputField.transform.DOShakePosition(duration, strength, vibrato, randomness);
        }

        private void OnSubmit(string input)
        {
            WordSubmitted?.Invoke(input);
            inputField.text = string.Empty; // Clear the input field
        }

        private void HandleButtonSubmit()
        {
            OnSubmit(inputField.text);
        }
    }
}