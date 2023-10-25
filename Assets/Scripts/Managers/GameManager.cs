using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Config.Services;
using Controllers;
using GameLogic;
using Interfaces.GameLogic;
using Interfaces.Input;
using Signals;
using UnityEngine;
using Zenject;

namespace Managers
{
    public class GameManager
    {
        private readonly ConfigurationLoader _configurationLoader;
        private readonly GridController _gridController;
        private readonly IUserInputHandler _userInputHandler;
        private  readonly SignalBus _signalBus;
        
        private IWordMatcher _wordMatcher;

        [Inject]
        public GameManager(GridController gridController, ConfigurationLoader configurationLoader, IUserInputHandler userInputHandler, SignalBus signalBus)
        {
            _gridController = gridController;
            _configurationLoader = configurationLoader;
            _userInputHandler = userInputHandler;
            _signalBus = signalBus;
            
            StartGame();
        }

        private void StartGame()
        {
            StartGameAsync().ContinueWith(t => 
            {
                if (t.IsFaulted)
                {
                    Debug.LogError(t.Exception);
                }
                else
                {
                    _userInputHandler.WordSubmitted += s =>
                    {
                        var matchedWord = _wordMatcher.MatchAndRemoveWord(s);
                        
                        if (matchedWord == null)
                        {
                            _signalBus.Fire(new IncorrectWordSignal());
                            return;
                        }
                        
                        OpenWordOnGrid(matchedWord);
                    };
                }
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        private async void OpenWordOnGrid(Word matchedWord)
        {
            foreach (var coordinate in matchedWord.Coordinates)
            {
                await _gridController.OpenCellAsync(coordinate);
            }
        }

        private async Task StartGameAsync()
        {
            var gridConfig = await _configurationLoader.LoadConfigurationAsync("Assets/Config/grid_config.json");
            _gridController.PopulateGrid(gridConfig.grids[0].grid);  // Call PopulateGrid with the first grid
            _wordMatcher = new WordMatcher(gridConfig);
        }
    }
}