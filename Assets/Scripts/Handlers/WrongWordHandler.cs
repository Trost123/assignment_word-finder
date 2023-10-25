using System;
using Signals;
using UnityEngine;
using Zenject;
using Interfaces.UI;
using Interfaces.Audio;
using Interfaces.Input;

namespace Handlers
{
    public class WrongWordHandler : IInitializable, IDisposable
    {
        private readonly SignalBus _signalBus;
        private readonly IUserInputHandler _userInputHandler;
        private readonly IAudioManager _audioManager;

        [Inject]
        public WrongWordHandler(SignalBus signalBus, IUserInputHandler userInputHandler, IAudioManager audioManager)
        {
            _signalBus = signalBus;
            _userInputHandler = userInputHandler;
            _audioManager = audioManager;
        }

        public void Initialize()
        {
            _signalBus.Subscribe<IncorrectWordSignal>(HandleWrongWord);
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<IncorrectWordSignal>(HandleWrongWord);
        }

        private void HandleWrongWord(IncorrectWordSignal signal)
        {
            Debug.Log("Received signal for wrong word.");
            
            // Assuming these methods exist in your IUIManager and IAudioManager interfaces
            _userInputHandler.ShakeInputField();
            _audioManager.PlaySound();
        }
    }
}