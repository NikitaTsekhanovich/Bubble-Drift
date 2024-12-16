using System;
using SceneLoaderControllers;
using UnityEngine;
using UnityEngine.UI;

namespace LevelsControllers
{
    public class LevelItem : MonoBehaviour
    {
        [SerializeField] private int _index;
        [SerializeField] private GameObject _openBlock;
        [SerializeField] private GameObject _lockBlock;
        [SerializeField] private Button _playButton;

        public static Action<LevelData> OnStashLevelData;

        public void UpdateLevelItemData()
        {
            var stateLevel = LevelDataContainer.LevelsData[_index].TypeStateLevel;

            if (stateLevel == TypeStateObject.IsOpen)
            {
                _openBlock.SetActive(true);
                _lockBlock.SetActive(false);
            }
            else 
            {
                _openBlock.SetActive(false);
                _lockBlock.SetActive(true);
                _playButton.interactable = false;
            }
        }

        public void PlayGame()
        {
            OnStashLevelData.Invoke(LevelDataContainer.LevelsData[_index]);
            LoadingScreenController.Instance.ChangeScene("Game");
        }
    }
}