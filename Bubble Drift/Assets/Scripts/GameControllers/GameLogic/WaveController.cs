using System;
using GameControllers.Abilities;
using GameControllers.GameEntities.Types;
using GameControllers.GameLaunch.Properties;
using GameControllers.UIControllers;
using LevelsControllers;
using UnityEngine;

namespace GameControllers.GameLogic
{
    public class WaveController : IHaveUpdate, IDisposable
    {
        private readonly int _numberWaves;
        private readonly UIContainer _uiContainer;
        private readonly GameWave[] _gameWaves;
        private readonly int _currentLevelIndex;
        private float _waveDuration;
        private float _delaySpawn;
        private int _currentIndexWave;
        private float _currentDelaySpawnFish;
        private int _numberFish;
        private bool _isStopChooseFish;
        private float _currentTimeWave;

        public static Action<Fish> OnSpawnFish;
        public static Action OnEndWave;
        public static Action<int> OnIncreaseCoins;
        public static Action OnEndGame;
        
        public WaveController(
            GameWave[] wavesData, 
            UIContainer uiContainer,
            int currentLevelIndex)
        {
            _numberWaves = wavesData.Length;
            _uiContainer = uiContainer;
            _gameWaves = wavesData;
            _currentLevelIndex = currentLevelIndex;
            
            _uiContainer.WaveTextUpdater.UpdateNumberWaveText(_currentIndexWave);
            LoadWave(_currentIndexWave);

            Ability.OnUseAbility += ContinueChooseFish;
        }

        private void LoadWave(int indexWave)
        {
            _delaySpawn = _gameWaves[indexWave].DelaySpawn;
            _currentDelaySpawnFish = _delaySpawn;
            _numberFish = _gameWaves[indexWave].NumberFish;
            _waveDuration = _gameWaves[indexWave].WaveDuration;
        }

        public void Update()
        {
            if (_isStopChooseFish) return;
            
            ControlWaveTime();
            ControlDelaySpawnFish();
        }

        private void ControlWaveTime()
        {
            _currentTimeWave += Time.deltaTime;
            _uiContainer.WaveTextUpdater.UpdateTimeWaveText(_waveDuration - _currentTimeWave);

            if (_currentTimeWave >= _waveDuration)
            {
                _currentTimeWave = 0f;
                EndWave();
            }
        }

        private void ControlDelaySpawnFish()
        {
            _currentDelaySpawnFish += Time.deltaTime;

            if (_currentDelaySpawnFish >= _delaySpawn && _numberFish > 0)
            {
                _numberFish--;
                OnSpawnFish.Invoke(GetFish());
                _currentDelaySpawnFish = 0f;
            }
        }

        private Fish GetFish()
        {
            var randomIndex = UnityEngine.Random.Range(0, _gameWaves[_currentIndexWave].Fishes.Length);
            return _gameWaves[_currentIndexWave].Fishes[randomIndex];
        }

        private void ContinueChooseFish()
        {
            OnIncreaseCoins.Invoke(_currentIndexWave);
            LoadWave(_currentIndexWave);
            _isStopChooseFish = false;
        }

        private void EndWave()
        {
            _currentIndexWave++;
            _isStopChooseFish = true;
            

            if (_currentIndexWave < _numberWaves)
            {
                _uiContainer.WaveTextUpdater.UpdateNumberWaveText(_currentIndexWave);
                OnEndWave.Invoke();
            }
            else
            {
                OnIncreaseCoins.Invoke(_currentIndexWave);
                _uiContainer.LevelCompletedScreen.OpenScreen();
                
                OnEndGame.Invoke();
                PlayerPrefs.SetInt($"{LevelDataKeys.LevelOpenKey}{_currentLevelIndex + 1}", (int)TypeStateObject.IsOpen);
            }
        }

        public void Dispose()
        {
            Ability.OnUseAbility -= ContinueChooseFish;
        }
    }
}
