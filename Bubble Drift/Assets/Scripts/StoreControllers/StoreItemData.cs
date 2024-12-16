using UnityEngine;

namespace StoreControllers
{
    [CreateAssetMenu(fileName = "StoreItemData", menuName = "Store item data/ Store item")]
    public class StoreItemData : ScriptableObject
    {
        [SerializeField] private int _index;
        [SerializeField] private Sprite _icon;
        [SerializeField] private int _price;

        public int Index => _index;
        public Sprite Icon => _icon;
        public int Price => _price;
        public TypeItemStore TypeState => (TypeItemStore)PlayerPrefs.GetInt($"{StoreDataKeys.StateItemKey}{_index}");
    }
}
