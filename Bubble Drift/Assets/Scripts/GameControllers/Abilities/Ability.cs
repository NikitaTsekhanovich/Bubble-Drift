using System;
using UnityEngine;

namespace GameControllers.Abilities
{
    public abstract class Ability : MonoBehaviour
    {
        public static Action OnUseAbility;
        
        public virtual bool GetCanSelectAbility() => true;

        public virtual void ClickUseAbility()
        {
            OnUseAbility.Invoke();
            gameObject.SetActive(false);
        }
    }
}
