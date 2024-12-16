using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace StoreControllers
{
    public class SkinsDataContainer : MonoBehaviour
    {
        public static List<StoreItemData> SkinsData { get; private set; }

        public static void LoadSkinsData()
        {
            SkinsData = Resources.LoadAll<StoreItemData>("ScriptableObjectData/StoreItemsData")
                .OrderBy(x => x.Index)
                .ToList();
        }
    }
}
