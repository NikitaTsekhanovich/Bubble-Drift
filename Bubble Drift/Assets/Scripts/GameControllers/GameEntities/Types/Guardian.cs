using GameControllers.Abilities.Types;

namespace GameControllers.GameEntities.Types
{
    public class Guardian : GameEntity<Guardian>
    {
        protected override void SubscribeActions()
        {
            GuardianSizeAbility.OnIncreaseSize += ChangeSize;
        }
        
        private void OnDestroy()
        {
            GuardianSizeAbility.OnIncreaseSize -= ChangeSize;
        }
    }
}
