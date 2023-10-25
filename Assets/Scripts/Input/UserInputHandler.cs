using System;
using Interfaces.GameLogic;
using Interfaces.Input;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Input
{
    public class UserInputHandler : MonoBehaviour, IUserInputHandler
    {
        [SerializeField]
        // TextMeshPro input field
        private TMP_InputField inputField;
        [SerializeField]
        private Button okButton;
        private IWordMatcher _wordMatcher;

        public event Action<string> WordSubmitted;

        private void Awake()
        {
            // Assuming you have a method to get the IWordMatcher instance
            // _wordMatcher = GetWordMatcherInstance(); 

            okButton.onClick.AddListener(HandleButtonSubmit);
            inputField.onEndEdit.AddListener(OnSubmit);
        }

        private void OnSubmit(string input)
        {
            WordSubmitted?.Invoke(input);
        }

        private void HandleButtonSubmit()
        {
            OnSubmit(inputField.text);
        }
    }
}