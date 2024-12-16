using System;
using GameControllers.Abilities.Types;
using GameControllers.Factories;
using GameControllers.GameEntities.Types;
using GameControllers.GameLaunch.Properties;
using UnityEngine;

namespace GameControllers.Spawners
{
    public class GuardianSpawner : IDisposable
    {
        private Guardian _guardian;
        private GameEntityFactory<Guardian> _guardianFactory;
        
        public static Func<Transform> OnGetPlayerTransform;
        
        public GuardianSpawner(Guardian guardian, Transform parentTransform)
        {
            _guardian = guardian;
            CreateGuardiansFactory(parentTransform);
            
            GuardianAbility.OnSpawnGuardian += SpawnGuardian;
        }

        private void CreateGuardiansFactory(Transform parentTransform)
        {
            _guardianFactory = new GameEntityFactory<Guardian>(
                _guardian,
                parentTransform);
        }

        private void SpawnGuardian()
        {
            _guardianFactory.GetGameEntity(OnGetPlayerTransform.Invoke());
        }

        public void Dispose()
        {
            GuardianAbility.OnSpawnGuardian -= SpawnGuardian;
        }
    }
}
