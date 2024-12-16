using System;

namespace GameControllers.Abilities.Types
{
    public class GuardianSizeAbility : Ability
    {
        private const float IncreaseSizeValue = 0.02f;
        
        public static Action<float> OnIncreaseSize;
        
        public override void ClickUseAbility()
        {
            OnIncreaseSize.Invoke(IncreaseSizeValue);
            base.ClickUseAbility();
        }
    }
}
