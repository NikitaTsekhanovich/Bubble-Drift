using DG.Tweening;
using GameControllers.Abilities.TimeAbilityControllers;
using GameControllers.Abilities.Types;
using GameControllers.GameEntities.Properties;
using GameControllers.GameLogic;
using UnityEngine;

namespace GameControllers.GameEntities.Types
{
    public class Fish : GameEntity<Fish>, ICanTakeDamage
    {
        [SerializeField] private ParticleSystem _deathParticleSystem;
        [SerializeField] private AudioSource _deathAudio;
        private bool _deathStarted;

        protected override void SubscribeActions()
        {
            EnemySizeAbility.OnDecreaseSize += ChangeSize;
            EnemySpeedAbility.OnDecreaseSpeed += _movementTarget.ChangeSpeed;
            TimeAbilityController.OnSlowDown += ChangeMovementState;
            WaveController.OnEndWave += ReturnToPool;
        }
        
        private void OnDestroy()
        {
            EnemySizeAbility.OnDecreaseSize -= ChangeSize;
            EnemySpeedAbility.OnDecreaseSpeed -= _movementTarget.ChangeSpeed;
            TimeAbilityController.OnSlowDown -= ChangeMovementState;
            WaveController.OnEndWave -= ReturnToPool;
        }

        private void ChangeMovementState(bool isSlowDown, float slowDownSpeedValue)
        {
            if (isSlowDown)
                _movementTarget.SlowDownMovement(slowDownSpeedValue);
            else
                _movementTarget.ReturnNormalMovement(slowDownSpeedValue);
        }

        public void TakeDamage()
        {
            ReturnToPool();
        }

        protected override void ReturnToPool()
        {
            if (!gameObject.activeSelf || _deathStarted) return;
            
            _deathStarted = true;
            _collider.enabled = false;
            var scale = transform.localScale;

            DOTween.Sequence()
                .Append(transform.DOScale(Vector3.zero, 0.5f))
                .AppendCallback(() =>
                {
                    _deathParticleSystem.Play();
                    _deathAudio.Play();
                })
                .AppendInterval(2f)
                .AppendCallback(() =>
                {
                    base.ReturnToPool();
                    _collider.enabled = true;
                    transform.localScale = scale;
                    _deathStarted = false;
                });
        }
    }
}
