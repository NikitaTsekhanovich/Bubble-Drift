using System;

namespace GameControllers.Abilities.Types
{
    public class EnemySizeAbility : Ability
    {
        private const float DecreasingValue = -0.02f;

        public static Action<float> OnDecreaseSize;
        
        public override void ClickUseAbility()
        {
            OnDecreaseSize.Invoke(DecreasingValue);
            base.ClickUseAbility();
        }
    }
}
