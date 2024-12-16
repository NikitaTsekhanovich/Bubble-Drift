using GameControllers.GameEntities;
using GameControllers.GameEntities.Properties;
using GameControllers.GameEntitiesPool;
using UnityEngine;

namespace GameControllers.Factories
{
    public class GameEntityFactory<T>
        where T : MonoBehaviour, ICanInitialize<T>
    {
        private readonly GameEntity<T> _gameEntity;
        private readonly Transform _parentTransform;
        private readonly PoolBase<GameEntity<T>> _gameEntitiesPool;
        private const int GameEntityPreloadCount = 5;

        public GameEntityFactory(GameEntity<T> gameEntity, Transform parentTransform)
        {
            _gameEntity = gameEntity;
            _parentTransform = parentTransform;
            _gameEntitiesPool = new PoolBase<GameEntity<T>>(Preload, GetGameEntityAction, ReturnGameEntityAction, GameEntityPreloadCount);
        }

        public void GetGameEntity(Transform spawnPoint)
        {
            var newGameEntity = _gameEntitiesPool.Get();
            newGameEntity.InitAppearance(spawnPoint);
        }

        private void ReturnGameEntity(GameEntity<T> gameEntity) => _gameEntitiesPool.Return(gameEntity);

        private GameEntity<T> Preload()
        {
            var newGameEntity = 
                Object.Instantiate(_gameEntity, Vector3.zero, Quaternion.identity, _parentTransform);
            newGameEntity.transform.localPosition = new Vector3(20, 0, 0);
            newGameEntity.InitInstantiate(ReturnGameEntity);
            
            return newGameEntity;
        }

        private void GetGameEntityAction(GameEntity<T> gameEntity)
        {
            gameEntity.gameObject.SetActive(true);
        }

        private void ReturnGameEntityAction(GameEntity<T> gameEntity)
        {
            gameEntity.transform.SetParent(_parentTransform);
            gameEntity.gameObject.SetActive(false);
        } 
    }
}