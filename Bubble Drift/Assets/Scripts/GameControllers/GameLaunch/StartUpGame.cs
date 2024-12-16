using System;
using System.Collections.Generic;
using GameControllers.Abilities;
using GameControllers.Abilities.TimeAbilityControllers;
using GameControllers.GameEntities;
using GameControllers.GameLaunch.Data;
using GameControllers.GameLaunch.Properties;
using GameControllers.GameLogic;
using GameControllers.PlayerControllers;
using GameControllers.Spawners;
using GameControllers.UIControllers;
using SceneLoaderControllers;
using UnityEngine;

namespace GameControllers.GameLaunch
{
    public class StartUpGame : MonoBehaviour
    {
        [SerializeField] private ConfigData _configData;
        [SerializeField] private GameData _gameData;
        [SerializeField] private UIContainer _uiContainer;
        [SerializeField] private Player _player;
        [SerializeField] private LevelCompletedScreen _levelCompletedScreen;
        private readonly List<IDisposable> _gameControllers = new(); 
        private readonly List<IHaveUpdate> _updatesSystems = new();
        
        public static StartUpGame Instance { get; private set; }
        
        private void OnEnable()
        {
            SceneDataLoader.OnLevelLoaded += StartLevel;
        }

        private void OnDisable()
        {
            SceneDataLoader.OnLevelLoaded -= StartLevel;
        }

        private void CreateInstance()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(this);
        }

        private void StartLevel(LevelConfig levelConfig)
        {
            ClearLevelControllers();
            _levelCompletedScreen.InitLevelIndex(levelConfig.LevelData.Index);
            _player.InitSprite(levelConfig.StoreItemData.Icon);
            CreateInstance();
            
            var fishInstantiateSettings = new GameEntityInstantiateSettings();
            _gameControllers.Add(fishInstantiateSettings);

            var timeAbilityController = new TimeAbilityController(
                _gameData.TimeAbilitiesImages,
                _gameData.TimerAbilitySound);
            _gameControllers.Add(timeAbilityController);

            var guardianSpawner = new GuardianSpawner(
                _gameData.GuardianPrefab,
                _gameData.ParentGuardians);
            _gameControllers.Add(guardianSpawner);

            var waveController = new WaveController(
                levelConfig.LevelData.GameWaves, 
                UIContainer.Instance,
                levelConfig.LevelData.Index);
            _gameControllers.Add(waveController);
            
            var fishSpawner = new FishSpawner(
                levelConfig.LevelData.GameWaves, 
                _gameData.FishSpawnPoints,
                _gameData.ParentFishes);
            _gameControllers.Add(fishSpawner);

            var abilitySelectionController = new AbilitySelectionController(
                _gameData.Abilities,
                _gameData.AbilitiesSlots,
                _gameData.AbilityScreen);
            _gameControllers.Add(abilitySelectionController);
            
            _updatesSystems.Add(waveController);
            _updatesSystems.Add(timeAbilityController);
        }

        private void Update()
        {
            foreach (var updateSystem in _updatesSystems)
                updateSystem.Update();
        }

        public void ClearLevelControllers()
        {
            _updatesSystems.Clear();

            foreach (var gameController in _gameControllers)
                gameController.Dispose();

            _gameControllers.Clear();
        }
    }
}
