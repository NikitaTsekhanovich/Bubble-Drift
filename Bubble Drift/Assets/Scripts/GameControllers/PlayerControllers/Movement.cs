using UnityEngine;

namespace GameControllers.PlayerControllers
{
    public class Movement : MonoBehaviour
    {
        private const float MovementSpeed = 20f;
        private Vector3 _movePosition;
        
        public void InitMovePosition(Vector3 clickPosition)
        {
            _movePosition = clickPosition;
        }

        private void Update()
        {
            Move();
        }
        
        private void Move()
        {
            transform.position = Vector3.Lerp(transform.position, _movePosition, Time.deltaTime * MovementSpeed);
        }
    }
}

