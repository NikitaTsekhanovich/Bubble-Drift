using System;
using GameControllers.UIControllers;
using LevelsControllers;
using MainMenu;
using PlayerData;
using StoreControllers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SceneLoaderControllers
{
    public class SceneDataLoader : MonoBehaviour
    {
        private LevelData _levelData;
        private ModeLoadMainMenu _modeLoadMainMenu;

        public static Action OnLoadStoreSkinsData;
        public static Action<LevelConfig> OnLevelLoaded;
        public static Action OnLoadLevelsData;
        public static SceneDataLoader Instance;

        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
            LevelItem.OnStashLevelData += StashLevelData;
            GameFlowHandler.OnStashLevelData += StashLevelData;
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            LevelItem.OnStashLevelData -= StashLevelData;
            GameFlowHandler.OnStashLevelData -= StashLevelData;
        }

        private void Start() 
        {        
            if (Instance == null) 
                Instance = this; 
            else 
                Destroy(this);  
        }

        public void StashModeLoadMainMenu(ModeLoadMainMenu modeLoadMainMenu)
        {
            _modeLoadMainMenu = modeLoadMainMenu;
        }

        private void StashLevelData(LevelData levelData)
        {
            _levelData = levelData;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
        {
            if (scene.name == "MainMenu")
            {
                CheckFirstLaunch();
                SkinsDataContainer.LoadSkinsData();
                OnLoadStoreSkinsData.Invoke();
                MainMenuHandler.Instance.OpenMainMenu(_modeLoadMainMenu);
                LevelDataContainer.LoadLevelData();
                OnLoadLevelsData.Invoke();
            }
            else if (scene.name == "Game")
            {
                OnLevelLoaded.Invoke(
                    new LevelConfig(
                        _levelData, 
                        SkinsDataContainer.SkinsData[PlayerPrefs.GetInt(StoreDataKeys.IndexChosenItemKey)]));
            }
        }

        private void CheckFirstLaunch()
        {
            if (PlayerPrefs.GetInt(PlayerDataKeys.IsFirstLaunchKey) == (int)ModeLoadScene.IsFirstLaunch)
            {
                PlayerPrefs.SetInt(StoreDataKeys.IndexChosenItemKey, 0);
                PlayerPrefs.SetInt($"{StoreDataKeys.StateItemKey}{0}", (int)TypeItemStore.Selected);
                PlayerPrefs.SetInt(PlayerDataKeys.IsFirstLaunchKey, (int)ModeLoadScene.IsNotFirstLaunch);
                PlayerPrefs.SetInt($"{LevelDataKeys.LevelOpenKey}{0}", (int)TypeStateObject.IsOpen);
            }
        }
    }
}