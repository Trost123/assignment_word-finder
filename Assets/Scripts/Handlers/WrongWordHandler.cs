﻿using System;
using Config.Models;
using Interfaces.Audio;
using Interfaces.Input;
using Signals;
using Zenject;

namespace Handlers
{
    public class WrongWordHandler : IInitializable, IDisposable
    {
        private readonly IAudioManager _audioManager;
        private readonly SignalBus _signalBus;
        private readonly IUserInputHandler _userInputHandler;

        private AppConfig _appConfig;

        [Inject]
        public WrongWordHandler(SignalBus signalBus, IUserInputHandler userInputHandler, IAudioManager audioManager)
        {
            _signalBus = signalBus;
            _userInputHandler = userInputHandler;
            _audioManager = audioManager;
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<IncorrectWordSignal>(HandleWrongWord);
        }

        public void Initialize()
        {
            _signalBus.Subscribe<IncorrectWordSignal>(HandleWrongWord);
        }

        public void SetBehaviorConfig(AppConfig appConfig)
        {
            _appConfig = appConfig;
        }

        private void HandleWrongWord(IncorrectWordSignal signal)
        {
            if (_appConfig.IncorrectInputBehavior.Shake) _userInputHandler.ShakeInputField();
            if (_appConfig.IncorrectInputBehavior.Sound) _audioManager.PlaySound();
        }
    }
}