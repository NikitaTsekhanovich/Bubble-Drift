using System;
using GameControllers.GameEntities;
using GameControllers.GameEntities.Types;
using UnityEngine;

namespace LevelsControllers
{
    [Serializable]
    public class GameWave
    {
        [field: SerializeField] public float WaveDuration { get; private set; }
        [field: SerializeField] public int NumberFish { get; private set; }
        [field: SerializeField] public int DelaySpawn { get; private set; }
        [field: SerializeField] public Fish[] Fishes { get; private set; }
    }
}
