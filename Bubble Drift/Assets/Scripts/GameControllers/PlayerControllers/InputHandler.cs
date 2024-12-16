using System;
using UnityEngine;

namespace GameControllers.PlayerControllers
{
    public class InputHandler : MonoBehaviour
    {
        [SerializeField] private Movement _movement;
        private Camera _camera;
        private bool _isBlockClick;

        public static Action CanUseTimeAbility;
        public static Action<bool> OnBlockClick;

        private void OnEnable()
        {
            OnBlockClick += ChangeStateBlockClick;
        }

        private void OnDisable()
        {
            OnBlockClick -= ChangeStateBlockClick;
        }

        private void Awake()
        {
            _camera = Camera.main;
        }

        private void ChangeStateBlockClick(bool isBlockClick) => _isBlockClick = isBlockClick;

        private void Update()
        {
            if (_isBlockClick) return;
            
            if (Input.GetMouseButton(0))
            {
                var mouseScreenPosition = Input.mousePosition;
                var mouseWorldPosition = _camera.ScreenToWorldPoint(mouseScreenPosition);
                mouseWorldPosition = new Vector3(mouseWorldPosition.x, mouseWorldPosition.y, 0f);

                var hits = Physics2D.RaycastAll(mouseWorldPosition, Vector2.zero);

                foreach (var hit in hits)
                {
                    if (hit.collider != null && hit.collider.CompareTag("ClickField"))
                    {
                        _movement.InitMovePosition(mouseWorldPosition);;
                    }
                }
            }
        }

        public void ClickUseTimeAbility()
        {
            CanUseTimeAbility.Invoke();
        }
    }
}

