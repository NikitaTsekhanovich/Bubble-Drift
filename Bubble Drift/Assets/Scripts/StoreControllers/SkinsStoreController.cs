using System.Collections.Generic;
using PlayerData;
using SceneLoaderControllers;
using TMPro;
using UnityEngine;

namespace StoreControllers
{
    public class SkinsStoreController : MonoBehaviour
    {
        [SerializeField] private AudioSource _purchaseSound;
        [SerializeField] private List<StoreItem> _storeItems = new();
        [SerializeField] private TMP_Text _currentCoinsText;
        private int _currentCoins;

        private void OnEnable()
        {   
            SceneDataLoader.OnLoadStoreSkinsData += InitStoreData;
        }

        private void OnDisable()
        {
            SceneDataLoader.OnLoadStoreSkinsData -= InitStoreData;
        }

        private void InitStoreData()
        {
            UpdateCoinsText();
                
            foreach (var skinData in SkinsDataContainer.SkinsData)
                _storeItems[skinData.Index].InitItem(
                    skinData.Icon, 
                    skinData.Price, 
                    skinData.TypeState, 
                    skinData.Index,
                    BuyOrSelectItem);
        }

        public void UpdateCoinsText()
        {
            var coins = PlayerPrefs.GetInt(PlayerDataKeys.CoinsAmountKey);
            _currentCoinsText.text = $"{coins}";
            _currentCoins = coins;
        }

        public void BuyOrSelectItem(int index)
        {
            if (SkinsDataContainer.SkinsData[index].TypeState == TypeItemStore.Bought ||
                SkinsDataContainer.SkinsData[index].TypeState == TypeItemStore.Selected)
            {
                SelectItem(index);
            }
            else if (SkinsDataContainer.SkinsData[index].TypeState == TypeItemStore.NotBought && 
                     _currentCoins - SkinsDataContainer.SkinsData[index].Price >= 0) 
            {
                _purchaseSound.Play();
                _currentCoins -= SkinsDataContainer.SkinsData[index].Price;
                PlayerPrefs.SetInt(PlayerDataKeys.CoinsAmountKey, _currentCoins);
                _currentCoinsText.text = $"{_currentCoins}";
                SelectItem(index);
            }
        }

        private void SelectItem(int index)
        {
            var chosenItemIndex = PlayerPrefs.GetInt(StoreDataKeys.IndexChosenItemKey);
            PlayerPrefs.SetInt($"{StoreDataKeys.StateItemKey}{chosenItemIndex}", (int)TypeItemStore.Bought);
            _storeItems[chosenItemIndex].ChangeStateItem(SkinsDataContainer.SkinsData[chosenItemIndex].TypeState);

            PlayerPrefs.SetInt(StoreDataKeys.IndexChosenItemKey, index);
            PlayerPrefs.SetInt($"{StoreDataKeys.StateItemKey}{index}", (int)TypeItemStore.Selected);
            _storeItems[index].ChangeStateItem(SkinsDataContainer.SkinsData[index].TypeState);
        }
    }
}
