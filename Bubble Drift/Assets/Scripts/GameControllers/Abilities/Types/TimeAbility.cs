using System;

namespace GameControllers.Abilities.Types
{
    public class TimeAbility : Ability
    {
        public static Func<bool> CanAddAbility;
        public static Action OnAddAbility;

        public override void ClickUseAbility()
        {
            OnAddAbility.Invoke();
            base.ClickUseAbility();
        }

        public override bool GetCanSelectAbility()
        {
            return CanAddAbility.Invoke();
        }
    }
}
