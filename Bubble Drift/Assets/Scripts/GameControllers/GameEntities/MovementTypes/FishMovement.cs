using UnityEngine;

namespace GameControllers.GameEntities.MovementTypes
{
    public class FishMovement : MovementTarget
    {
        private bool _isOnGameField;
        
        public override void InitMovementSetting()
        {
            _isOnGameField = false;
        }
        
        private void FixedUpdate()
        {
            if (!_isSlowDown)
                FollowPlayer();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("GameField"))
            {
                if (_isOnGameField)
                    _returnToPool.Invoke();
                
                _isOnGameField = !_isOnGameField;
            }
        }
    }
}
