using GameControllers.Abilities.Types;
using GameControllers.GameEntities;
using GameControllers.GameLogic;
using GameControllers.Spawners;
using UnityEngine;

namespace GameControllers.PlayerControllers
{
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(InputHandler))]
    [RequireComponent(typeof(Movement))]
    [RequireComponent(typeof(HealthHandler))]
    [RequireComponent(typeof(CollisionHandler))]
    public class Player : MonoBehaviour
    {
        private void OnEnable()
        {
            MovementTarget.OnGetTargetPosition += GetPosition;
            GuardianSpawner.OnGetPlayerTransform += GetTransform;
            PlayerSizeAbility.OnDecreasePlayerSize += ChangeSize;
        }

        private void OnDestroy()
        {
            MovementTarget.OnGetTargetPosition -= GetPosition;
            GuardianSpawner.OnGetPlayerTransform -= GetTransform;
            PlayerSizeAbility.OnDecreasePlayerSize -= ChangeSize;
        }

        public void InitSprite(Sprite sprite)
        {
            GetComponent<SpriteRenderer>().sprite = sprite;
        }

        private void ChangeSize(float newSize)
        {
            transform.localScale = new Vector3(
                transform.localScale.x + newSize, 
                transform.localScale.y + newSize, 
                transform.localScale.z + newSize);
        }

        private Vector3 GetPosition() => transform.position;
        private Transform GetTransform() => transform;
    }
}
