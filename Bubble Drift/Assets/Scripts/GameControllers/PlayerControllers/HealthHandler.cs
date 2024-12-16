using System;
using System.Collections;
using DG.Tweening;
using GameControllers.Abilities.Types;
using GameControllers.GameLogic;
using UnityEngine;
using UnityEngine.UI;

namespace GameControllers.PlayerControllers
{
    public class HealthHandler : MonoBehaviour
    {
        [SerializeField] private Image[] _heartsImages;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Collider2D _collider;
        [SerializeField] private Material _material;
        [SerializeField] private AudioSource _damageSound;
        private int _currentHealth = StartHealth;
        private const int StartHealth = 3;
        private const float AnimationPlayerDelay = 3f;
        private const float AnimationHeartDelay = 0.01f;
        private const string CompressionSpeedKey = "_CompressionSpeed";
        private const string TransparencyKey = "_Transparency";
        
        public static Action OnDeathPlayer;
        
        private static readonly int CompressionSpeed = Shader.PropertyToID(CompressionSpeedKey);
        private static readonly int Transparency = Shader.PropertyToID(TransparencyKey);

        private void Awake()
        {
            _material.SetFloat(CompressionSpeed, 0);
            _material.SetFloat(Transparency, 1);
        }

        private void OnEnable()
        {
            HealthAbility.OnIncreaseHealth += IncreaseHealth;
            HealthAbility.CanAddHealth += CheckAmountHealth;
            WaveController.OnEndGame += OffCollision;
        }

        private void OnDisable()
        {
            HealthAbility.OnIncreaseHealth -= IncreaseHealth;
            HealthAbility.CanAddHealth -= CheckAmountHealth;
            WaveController.OnEndGame -= OffCollision;
        }
        
        private void OffCollision() => _collider.enabled = false;

        private bool CheckAmountHealth() => _currentHealth < StartHealth;

        private void IncreaseHealth()
        {
            _heartsImages[_currentHealth].fillAmount = 1f;
            _currentHealth++;
        }

        public void GetDamage()
        {
            _damageSound.Play();
            _currentHealth--;

            switch (_currentHealth)
            {
                case 2:
                    StartCoroutine(LoseHeartAnimation(_heartsImages[_currentHealth]));
                    StartGetDamageAnimation();
                    _heartsImages[_currentHealth].fillAmount = 0f;
                    break;
                case 1:
                    StartCoroutine(LoseHeartAnimation(_heartsImages[_currentHealth]));
                    StartGetDamageAnimation();
                    _heartsImages[_currentHealth].fillAmount = 0f;
                    break;
                case 0:
                    _collider.enabled = false;
                    StartCoroutine(LoseHeartAnimation(_heartsImages[_currentHealth]));
                    OnDeathPlayer.Invoke();
                    _heartsImages[_currentHealth].fillAmount = 0f;
                    break;
            }
        }

        private IEnumerator LoseHeartAnimation(Image heartImage)
        {
            var currentTime = 1f;
            
            while (currentTime >= 0)
            {
                currentTime -= AnimationHeartDelay;
                heartImage.fillAmount = currentTime;
                yield return new WaitForSeconds(AnimationHeartDelay);
            }
        }

        private void StartGetDamageAnimation()
        {
            _collider.enabled = false;
            
            _material.SetFloat(CompressionSpeed, 40f);
            _material.DOFloat(0f, CompressionSpeed, AnimationPlayerDelay);
            
            DOTween.Sequence()
                .Append(_material.DOFloat(0.5f, Transparency, AnimationPlayerDelay / 10))
                .Append(_material.DOFloat(1f, Transparency, AnimationPlayerDelay / 10))
                .SetLoops((int)AnimationPlayerDelay, LoopType.Yoyo)
                .OnComplete(() =>
                {
                    _collider.enabled = true;
                });
        }
    }
}
