using GameControllers.PlayerControllers;
using UnityEngine;

namespace GameControllers.UIControllers
{
    public class GameOverScreen : GameFlowHandler
    {
        [SerializeField] private AudioSource _gameOverSound;
        
        private void OnEnable()
        {
            HealthHandler.OnDeathPlayer += GameOver;
        }

        private void OnDisable()
        {
            HealthHandler.OnDeathPlayer -= GameOver;
        }

        private void GameOver()
        {
            _gameOverSound.Play();
            ClickPause();
            OpenScreen();
        }
    }
}
