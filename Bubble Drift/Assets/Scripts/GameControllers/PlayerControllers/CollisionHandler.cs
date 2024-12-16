using UnityEngine;

namespace GameControllers.PlayerControllers
{
    public class CollisionHandler : MonoBehaviour
    {
        [SerializeField] private HealthHandler _healthHandler;
        
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("GameField"))
            {
                _healthHandler.GetDamage();
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Enemy"))
            {
                _healthHandler.GetDamage();
            }
        }
    }
}
