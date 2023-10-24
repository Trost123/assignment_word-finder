using System;
using System.Threading.Tasks;
using Config.Models;
using Config.Services;
using Controllers;
using GameLogic;
using Input;
using Interfaces.Input;
using UnityEngine;
using Zenject;

namespace Managers
{
    public class GameManager
    {
        private readonly ConfigurationLoader _configurationLoader;
        private readonly GridController _gridController;
        private readonly IUserInputHandler _userInputHandler;
        
        private WordMatcher _wordMatcher;

        public event Action GameStarted;

        [Inject]
        public GameManager(GridController gridController, ConfigurationLoader configurationLoader, IUserInputHandler userInputHandler)
        {
            _gridController = gridController;
            _configurationLoader = configurationLoader;
            _userInputHandler = userInputHandler;
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
                    GameStarted?.Invoke();
                    _userInputHandler.WordSubmitted += s => _wordMatcher.MatchWord(s);
                }
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        private async Task StartGameAsync()
        {
            var gridConfig = await _configurationLoader.LoadConfigurationAsync("Assets/Config/grid_config.json");
            _gridController.PopulateGrid(gridConfig.grids[1].grid);  // Call PopulateGrid with the first grid
            _wordMatcher = new WordMatcher(gridConfig);
        }
    }
}