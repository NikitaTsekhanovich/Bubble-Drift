using System;

namespace GameControllers.Abilities.Types
{
    public class HealthAbility : Ability
    {
        public static Action OnIncreaseHealth;
        public static Func<bool> CanAddHealth;
        
        public override void ClickUseAbility()
        {
            OnIncreaseHealth.Invoke();
            base.ClickUseAbility();
        }
        
        public override bool GetCanSelectAbility()
        {
            return CanAddHealth.Invoke();
        }
    }
}
