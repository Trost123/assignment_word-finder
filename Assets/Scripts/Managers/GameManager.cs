using System;
using System.Threading.Tasks;
using Config.Models;
using Config.Services;
using Controllers;
using UnityEngine;
using Zenject;

namespace Managers
{
    public class GameManager
    {
        private readonly GridController _gridController;
        private readonly ConfigurationLoader _configurationLoader;
    
        [Inject]
        public GameManager(GridController gridController, ConfigurationLoader configurationLoader)
        {
            _gridController = gridController;
            _configurationLoader = configurationLoader;

            var startGameAsync = StartGameAsync();
        }

        private async Task StartGameAsync()
        {
            try
            {
                GridConfig gridConfig = await _configurationLoader.LoadConfigurationAsync("Assets/Config/grid_config.json");
                _gridController.PopulateGrid(gridConfig.grids[1].grid);  // Call PopulateGrid with the first grid
            }
            catch (Exception ex)
            {
                Debug.LogError(ex);
            }
        }
    }
}