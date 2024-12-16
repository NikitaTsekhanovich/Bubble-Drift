using System;

namespace GameControllers.Abilities.Types
{
    public class PlayerSizeAbility : Ability
    {
        private const float DecreaseSizeValue = -0.02f;
        
        public static Action<float> OnDecreasePlayerSize;
        
        public override void ClickUseAbility()
        {
            OnDecreasePlayerSize.Invoke(DecreaseSizeValue);
            base.ClickUseAbility();
        }
    }
}
