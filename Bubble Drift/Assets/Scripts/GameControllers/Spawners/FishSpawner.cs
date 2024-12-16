using System;
using System.Collections.Generic;
using GameControllers.Factories;
using GameControllers.GameEntities.Types;
using GameControllers.GameLogic;
using LevelsControllers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GameControllers.Spawners
{
    public class FishSpawner : IDisposable
    {
        private readonly Transform[] _spawnPoints;
        private Dictionary<Fish, GameEntityFactory<Fish>> _fishFactories = new();

        public FishSpawner(
            GameWave[] wavesData, 
            Transform[] spawnPoint,
            Transform parentFishes)
        {
            WaveController.OnSpawnFish += SpawnFish;
            _spawnPoints = spawnPoint;

            CreateFishFactories(wavesData, parentFishes);
        }
        
        private void CreateFishFactories(
            GameWave[] wavesData, 
            Transform fishParent)
        {
            foreach (var waveData in wavesData)
            {
                foreach (var fish in waveData.Fishes)
                {
                    if (!_fishFactories.ContainsKey(fish))
                    {
                        var factoryFish = new GameEntityFactory<Fish>(fish, fishParent);
                        _fishFactories[fish] = factoryFish;
                    }
                }
            }
        }

        private void SpawnFish(Fish fish)
        {
            var randomIndexPoint = Random.Range(0, _spawnPoints.Length);
            var spawnPoint = _spawnPoints[randomIndexPoint];
                
            _fishFactories[fish].GetGameEntity(spawnPoint);
        }

        public void Dispose()
        {
            WaveController.OnSpawnFish -= SpawnFish;
        }
    }
}
