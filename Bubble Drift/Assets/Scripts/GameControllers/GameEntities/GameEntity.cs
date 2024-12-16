using System;
using GameControllers.GameEntities.Models;
using GameControllers.GameEntities.Properties;
using UnityEngine;

namespace GameControllers.GameEntities
{
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(MovementTarget))]
    public abstract class GameEntity<T> : MonoBehaviour, ICanInitialize<T>
    {
        [SerializeField] private MovementTargetCharacteristicsData _movementTargetCharacteristicsData;
        private Action<GameEntity<T>> _returnAction;
        protected Collider2D _collider;
        protected MovementTarget _movementTarget;

        private void Awake()
        {
            _movementTarget = GetComponent<MovementTarget>();
            _collider = GetComponent<Collider2D>();
        }
        
        public void InitInstantiate(Action<GameEntity<T>> returnAction)
        {
            _returnAction = returnAction;
            
            _movementTarget.Construct(
                _movementTargetCharacteristicsData.Speed,
                _movementTargetCharacteristicsData.MaximumAttractionDistance,
                _movementTargetCharacteristicsData.MaximumSpeed,
                ReturnToPool);
            
            SubscribeActions();
            
            ChangeSize(GameEntityInstantiateSettings.Instance.CurrentChangingSizeValue);
            _movementTarget.ChangeSpeed(GameEntityInstantiateSettings.Instance.CurrentChangingSpeedValue);
        }
        
        public void InitAppearance(Transform startPosition)
        {
            transform.position = startPosition.position;
            _movementTarget.InitMovementSetting();
        }

        protected void ChangeSize(float changeSizeValue)
        {
            if (changeSizeValue == 0) return;

            transform.localScale = new Vector3(
                transform.localScale.x + changeSizeValue, 
                transform.localScale.y + changeSizeValue, 
                transform.localScale.z + changeSizeValue);
        }

        protected virtual void ReturnToPool()
        {
            _returnAction.Invoke(this);
        }
        
        protected abstract void SubscribeActions();
    }
}
