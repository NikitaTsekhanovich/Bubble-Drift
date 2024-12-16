using GameControllers.GameLaunch;
using LevelsControllers;
using SceneLoaderControllers;
using UnityEngine;

namespace GameControllers.UIControllers
{
    public class LevelCompletedScreen : GameFlowHandler
    {
        [SerializeField] private AudioSource _levelCompletedAudio;
        private int _currentLevelIndex = -1;

        public override void OpenScreen()
        {
            _levelCompletedAudio.Play();
            base.OpenScreen();
        }

        public void InitLevelIndex(int currentLevelIndex)
        {
            _currentLevelIndex = currentLevelIndex;
        }
        
        public void ClickNextLevel()
        {
            Time.timeScale = 1f;
            
            if (LevelDataContainer.LevelsData.Count > _currentLevelIndex + 1)
                OnStashLevelData.Invoke(
                    LevelDataContainer.LevelsData[_currentLevelIndex + 1]);
            
            StartUpGame.Instance.ClearLevelControllers();
            LoadingScreenController.Instance.ChangeScene("Game");
        }
    }
}
