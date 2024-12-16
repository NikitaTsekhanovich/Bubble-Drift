using System;
using System.Collections.Generic;
using DG.Tweening;
using GameControllers.GameLogic;
using GameControllers.PlayerControllers;
using UnityEngine;
using Random = System.Random;

namespace GameControllers.Abilities
{
    public class AbilitySelectionController : IDisposable
    {
        private readonly Queue<Ability> _abilities;
        private readonly Transform[] _slots;
        private readonly GameObject _abilityScreen;
        private readonly List<Ability> _chosenAbilities = new ();

        public AbilitySelectionController(Ability[] abilities, Transform[] slots, GameObject abilityScreen)
        {
            _abilities = new Queue<Ability>(GetShuffleAbilities(abilities));
            _slots = slots;
            _abilityScreen = abilityScreen;

            Ability.OnUseAbility += OnUsedAbility;
            WaveController.OnEndWave += SelectAbilities;
        }


        private Ability[] GetShuffleAbilities(Ability[] array)
        {
            var random = new Random();
            var arrayLength = array.Length;
            
            for (var i = arrayLength - 1; i > 0; i--)
            {
                var j = random.Next(0, i + 1);
                (array[i], array[j]) = (array[j], array[i]);
            }
            
            return array;
        }

        private void SelectAbilities()
        {
            // _abilityScreen.SetActive(true);
            InputHandler.OnBlockClick.Invoke(true);
            _abilityScreen.transform.DOMove(Vector3.zero, 1f).SetEase(Ease.OutBounce);
            
            var amountSelected = 0;
            
            for (var i = 0; i < _abilities.Count; i++)
            {
                var currentAbility = _abilities.Dequeue();

                if (currentAbility.GetCanSelectAbility())
                {
                    currentAbility.transform.SetParent(_slots[amountSelected]);
                    currentAbility.transform.localPosition = Vector3.zero;
                    
                    currentAbility.gameObject.SetActive(true);
                    _chosenAbilities.Add(currentAbility);
                    amountSelected++;
                }
                
                _abilities.Enqueue(currentAbility);
                
                if (amountSelected >= 2) break;
            }
        }

        private void OnUsedAbility()
        {
            foreach (var chosenAbility in _chosenAbilities)
                chosenAbility.gameObject.SetActive(false);

            _abilityScreen.transform.DOMove(new Vector3(10f, 0f, 0f), 1f);
            InputHandler.OnBlockClick.Invoke(false);
            // _abilityScreen.SetActive(false));
            
            _chosenAbilities.Clear();
        }

        public void Dispose()
        {
            Ability.OnUseAbility -= OnUsedAbility;
            WaveController.OnEndWave -= SelectAbilities;
        }
    }
}
