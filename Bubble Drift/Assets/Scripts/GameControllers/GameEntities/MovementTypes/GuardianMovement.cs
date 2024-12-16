using DG.Tweening;
using GameControllers.GameEntities.Properties;
using UnityEngine;

namespace GameControllers.GameEntities.MovementTypes
{
    public class GuardianMovement : MovementTarget
    {
        private const float AnimationCollisionDelay = 5f;
        
        public override void InitMovementSetting()
        {
            
        }
        
        private void FixedUpdate()
        {
            FollowPlayer();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out ICanTakeDamage gameEntity))
            {
                StartCollisionAnimation();
                gameEntity.TakeDamage();
            }
        }

        private void StartCollisionAnimation()
        {
            _collider.enabled = false;
            SlowDownMovement(10);
            
            DOTween.Sequence()
                .Append(_spriteRenderer.DOColor(new Color(0f, 0f, 0f, 0f), AnimationCollisionDelay / 10))
                .Append(_spriteRenderer.DOColor(new Color(1f, 1f, 1f, 1f), AnimationCollisionDelay / 10))
                .SetLoops((int)AnimationCollisionDelay, LoopType.Yoyo)
                .OnComplete(() =>
                {
                    _collider.enabled = true;
                    ReturnNormalMovement(10);
                });
        }
    }
}
