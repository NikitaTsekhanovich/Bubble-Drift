using System;
using GameControllers.Abilities.TimeAbilityControllers;
using GameControllers.Abilities.Types;

namespace GameControllers.GameEntities
{
    public class GameEntityInstantiateSettings : IDisposable
    {
        public float CurrentChangingSizeValue { get; private set; }
        public float CurrentChangingSpeedValue { get; private set; }
        
        public static GameEntityInstantiateSettings Instance { get; private set; }
        
        public GameEntityInstantiateSettings()
        {
            Instance ??= this;
            
            EnemySizeAbility.OnDecreaseSize += SetSize;
            EnemySpeedAbility.OnDecreaseSpeed += SetSpeed;
        }

        private void SetSize(float decreaseValue)
        {
            CurrentChangingSizeValue -= decreaseValue;
        }
        
        private void SetSpeed(float decreaseValue)
        {
            CurrentChangingSpeedValue -= decreaseValue;
        }

        public void Dispose()
        {
            EnemySizeAbility.OnDecreaseSize -= SetSize;
            EnemySpeedAbility.OnDecreaseSpeed -= SetSpeed;
        }
    }
}
