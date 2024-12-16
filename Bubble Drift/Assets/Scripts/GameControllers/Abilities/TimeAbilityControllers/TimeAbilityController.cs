using System;
using GameControllers.Abilities.Types;
using GameControllers.GameLaunch.Properties;
using GameControllers.PlayerControllers;
using UnityEngine;
using UnityEngine.UI;

namespace GameControllers.Abilities.TimeAbilityControllers
{
    public class TimeAbilityController : IDisposable, IHaveUpdate
    {
        private readonly Image[] _abilitiesImages; 
        private readonly AudioSource _abilitySound;
        private int _currentAmountAbilities;
        private bool _isSlowDownTime;
        private float _currentAbilityTime;
        private const float SlowDownSpeedValue = 10f;
        private const int MaxAmountAbilities = 3;
        private const float SlowdownActionTime = 5f;

        public static Action<bool, float> OnSlowDown;

        public TimeAbilityController(Image[] abilitiesImages, AudioSource abilitySound)
        {
            _abilitySound = abilitySound;
            _abilitiesImages = abilitiesImages;
            InitAbilities();

            TimeAbility.OnAddAbility += AddAbility;
            TimeAbility.CanAddAbility += CanAddAbility;
            InputHandler.CanUseTimeAbility += UseAbility;
        }

        private void AddAbility()
        {
            _currentAmountAbilities++;
            _abilitiesImages[_currentAmountAbilities - 1].enabled = true;
        }

        public void Update()
        {
            if (_isSlowDownTime)
                SlowDownTime();
        }

        private void InitAbilities()
        {
            foreach (var abilityImage in _abilitiesImages)
            {
                abilityImage.enabled = false;
            }
            
            _abilitiesImages[0].enabled = true;
            _currentAmountAbilities = 1;
        }

        private bool CanAddAbility()
        {
            return _currentAmountAbilities < MaxAmountAbilities;
        }

        private void UseAbility()
        {
            if (_currentAmountAbilities > 0 && !_isSlowDownTime)
            {
                _abilitiesImages[_currentAmountAbilities - 1].enabled = false;
                _currentAmountAbilities--;

                _isSlowDownTime = true;
                _abilitySound.Play();
                OnSlowDown.Invoke(_isSlowDownTime, SlowDownSpeedValue);
            }
        }

        private void SlowDownTime()
        {
            _currentAbilityTime += Time.deltaTime;

            if (_currentAbilityTime >= SlowdownActionTime)
            {
                _isSlowDownTime = false;
                OnSlowDown.Invoke(_isSlowDownTime, SlowDownSpeedValue);
                _currentAbilityTime = 0f;
            }
        }

        public void Dispose()
        {
            TimeAbility.OnAddAbility -= AddAbility;
            TimeAbility.CanAddAbility -= CanAddAbility;
            InputHandler.CanUseTimeAbility -= UseAbility;
        }
    }
}
