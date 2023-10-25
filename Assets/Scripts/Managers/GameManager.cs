using System.Threading.Tasks;
using Config.Models;
using Config.Services;
using Controllers;
using GameLogic;
using Handlers;
using Interfaces.GameLogic;
using Interfaces.Input;
using Signals;
using UnityEngine;
using Zenject;

namespace Managers
{
    public class GameManager
    {
        private readonly GridController _gridController;
        private readonly SignalBus _signalBus;
        private readonly IUserInputHandler _userInputHandler;
        private readonly WrongWordHandler _wrongWordHandler;

        private IWordMatcher _wordMatcher;

        [Inject]
        public GameManager(GridController gridController, IUserInputHandler userInputHandler,
            SignalBus signalBus, WrongWordHandler wrongWordHandler)
        {
            _gridController = gridController;
            _userInputHandler = userInputHandler;
            _signalBus = signalBus;
            _wrongWordHandler = wrongWordHandler;

            StartGame();
        }

        private void StartGame()
        {
            StartGameAsync().ContinueWith(t =>
            {
                if (t.IsFaulted)
                    Debug.LogError(t.Exception);
                else
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
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        private async void OpenWordOnGrid(Word matchedWord)
        {
            foreach (var coordinate in matchedWord.Coordinates) await _gridController.OpenCellAsync(coordinate);
        }

        private async Task StartGameAsync()
        {
            var loadGridConfigTask = ConfigurationLoader.LoadConfigAsync<GridConfig>("Assets/Config/grid_config.json");
            var loadBehaviorConfigTask =
                ConfigurationLoader.LoadConfigAsync<AppConfig>("Assets/Config/behavior_config.json");

            await Task.WhenAll(loadGridConfigTask, loadBehaviorConfigTask);

            var gridConfig = loadGridConfigTask.Result;
            var behaviorConfig = loadBehaviorConfigTask.Result;

            var currentGrid = gridConfig.Grid;
            _gridController.PopulateGrid(currentGrid); // Call PopulateGrid with the first grid
            _wrongWordHandler.SetBehaviorConfig(behaviorConfig);
            _wordMatcher = new WordMatcher(currentGrid);
        }
    }
}