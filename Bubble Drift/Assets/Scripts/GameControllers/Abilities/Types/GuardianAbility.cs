using System;

namespace GameControllers.Abilities.Types
{
    public class GuardianAbility : Ability
    {
        public static Action OnSpawnGuardian;
        
        public override void ClickUseAbility()
        {
            OnSpawnGuardian.Invoke();
            base.ClickUseAbility();
        }
    }
}
