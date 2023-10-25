using System;
using DG.Tweening;
using Interfaces.GameLogic;
using Interfaces.Input;
using Interfaces.UI;  // Import the namespace for IUIManager
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Input
{
    public class UserInputHandler : MonoBehaviour, IUserInputHandler
    {
        [SerializeField]
        private TMP_InputField inputField;
        [SerializeField]
        private Button okButton;

        private IWordMatcher _wordMatcher;

        public event Action<string> WordSubmitted;
        
        private void Awake()
        {
            okButton.onClick.AddListener(HandleButtonSubmit);
            inputField.onEndEdit.AddListener(OnSubmit);
        }

        private void OnSubmit(string input)
        {
            WordSubmitted?.Invoke(input);
            inputField.text = string.Empty;  // Clear the input field
        }

        public void ShakeInputField()
        {
            // Parameters for the shake
            float duration = 0.5f;  // Duration of the shake
            Vector3 strength = new Vector3(5f, 0f, 0f);  // Strength of the shake (X axis only)
            int vibrato = 10;       // Amount of shake segments
            float randomness = 0;  // Randomness of shake (0 to 180)

            // Perform the shake
            inputField.transform.DOShakePosition(duration, strength, vibrato, randomness);
        }

        private void HandleButtonSubmit()
        {
            OnSubmit(inputField.text);
        }
    }
}