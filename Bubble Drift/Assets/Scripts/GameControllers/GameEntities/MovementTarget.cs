using System;
using UnityEngine;

namespace GameControllers.GameEntities
{
    public abstract class MovementTarget : MonoBehaviour
    {
        private float _maximumAttractionDistance;
        private float _maximumSpeed;
        private float _speed;
        private Rigidbody2D _rigidbody;
        protected Collider2D _collider;
        protected SpriteRenderer _spriteRenderer;
        protected Action _returnToPool;
        protected bool _isSlowDown;
        
        public static Func<Vector3> OnGetTargetPosition;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _collider  = GetComponent<Collider2D>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public abstract void InitMovementSetting();

        public void Construct(
            float speed, 
            float maximumAttractionDistance, 
            float maximumSpeed,
            Action returnToPool)
        {
            _speed = speed;
            _maximumAttractionDistance = maximumAttractionDistance;
            _maximumSpeed = maximumSpeed;
            _returnToPool = returnToPool;
        }
        
        protected void FollowPlayer()
        {
            var target = OnGetTargetPosition.Invoke();
            
            var direction = (target - transform.position).normalized;
            var distance = Vector2.Distance(transform.position, target);
            var attractionForce = direction * _speed * Mathf.Clamp01(distance / _maximumAttractionDistance);

            _rigidbody.AddForce(attractionForce, ForceMode2D.Force);
            _rigidbody.velocity = Vector2.ClampMagnitude(_rigidbody.velocity, _maximumSpeed);
            
            var angle = Mathf.Atan2(_rigidbody.velocity.y, _rigidbody.velocity.x) * Mathf.Rad2Deg;
            _rigidbody.rotation = angle - 180;
        }

        public void ChangeSpeed(float changeSpeedValue)
        {
            if (changeSpeedValue == 0) return;
            
            _speed += changeSpeedValue;
            _rigidbody.velocity = Vector2.zero;
        }
        
        public void SlowDownMovement(float slowDownSpeedValue)
        {
            _isSlowDown = true;
            _speed /= slowDownSpeedValue;
            _rigidbody.velocity /= slowDownSpeedValue;
        }

        public void ReturnNormalMovement(float slowDownSpeedValue)
        {
            _isSlowDown = false;
            _speed *= slowDownSpeedValue;
        }
    }
}
