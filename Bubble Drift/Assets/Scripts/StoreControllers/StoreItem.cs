using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace StoreControllers
{
    public class StoreItem : MonoBehaviour
    {
        [SerializeField] private Image _itemIcon;
        [SerializeField] private TMP_Text _priceText;
        [SerializeField] private GameObject _lockBlock;
        [SerializeField] private GameObject _chooseBlock;
        [SerializeField] private GameObject _selectBlock;
        private int _index;
        private Action<int> _onChooseItem;

        public void InitItem(Sprite leafIcon, int priceText, TypeItemStore typeItemStore, int index, Action<int> OnChooseItem)
        {
            _itemIcon.sprite = leafIcon;
            _index = index;
            _onChooseItem = OnChooseItem;
            _priceText.text = $"{priceText}";
            
            ChangeStateItem(typeItemStore);
        }

        public void ChangeStateItem(TypeItemStore typeItemStore)
        {   
            _lockBlock.SetActive(false);
            _chooseBlock.SetActive(false);
            _selectBlock.SetActive(false);
            
            if (typeItemStore == TypeItemStore.Selected)
            {
                _chooseBlock.SetActive(true);
            }
            else if (typeItemStore == TypeItemStore.Bought)
            {
                _selectBlock.SetActive(true);
            }
            else if (typeItemStore == TypeItemStore.NotBought) 
            {
                _lockBlock.SetActive(true);
            }
        }

        public void ChooseItem()
        {
            _onChooseItem.Invoke(_index);
        }
    }
}
