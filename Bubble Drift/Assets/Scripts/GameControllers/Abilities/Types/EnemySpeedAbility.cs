using System;

namespace GameControllers.Abilities.Types
{
    public class EnemySpeedAbility : Ability
    {
        private const float DecreasingValue = -0.3f;

        public static Action<float> OnDecreaseSpeed;
        
        public override void ClickUseAbility()
        {
            OnDecreaseSpeed.Invoke(DecreasingValue);
            base.ClickUseAbility();
        }
    }
}
