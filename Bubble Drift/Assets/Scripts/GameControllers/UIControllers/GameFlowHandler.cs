using System;
using DG.Tweening;
using GameControllers.GameLaunch;
using GameControllers.PlayerControllers;
using LevelsControllers;
using SceneLoaderControllers;
using UnityEngine;

namespace GameControllers.UIControllers
{
    public abstract class GameFlowHandler : MonoBehaviour
    {
        [SerializeField] protected GameObject _screenBlock;

        public static Action<LevelData> OnStashLevelData;
        
        public virtual void OpenScreen()
        {
            InputHandler.OnBlockClick.Invoke(true);
            _screenBlock.SetActive(true);
        }
        
        public void ClickPause()
        {
            OpenScreen();
            InputHandler.OnBlockClick.Invoke(true);
            Time.timeScale = 0f;
        }
        
        public void ClickContinue()
        {
            InputHandler.OnBlockClick.Invoke(false);
            Time.timeScale = 1f;
        }

        public void ClickBackToLevels()
        {
            BackToMenu(ModeLoadMainMenu.Levels);
        }

        public void ClickBackToStore()
        {
            BackToMenu(ModeLoadMainMenu.Store);
        }

        public void ClickExit()
        {
            BackToMenu(ModeLoadMainMenu.MainMenu);
        }

        public void ClickRestart()
        {
            Time.timeScale = 1f;
            StartUpGame.Instance.ClearLevelControllers();
            LoadingScreenController.Instance.ChangeScene("Game");
        }

        private void BackToMenu(ModeLoadMainMenu modeLoadMainMenu)
        {
            Time.timeScale = 1f;
            SceneDataLoader.Instance.StashModeLoadMainMenu(modeLoadMainMenu);
            StartUpGame.Instance.ClearLevelControllers();
            LoadingScreenController.Instance.ChangeScene("MainMenu");
        }
    }
}



