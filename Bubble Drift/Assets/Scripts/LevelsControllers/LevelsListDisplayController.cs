using SceneLoaderControllers;
using UnityEngine;

namespace LevelsControllers
{
    public class LevelsListDisplayController : MonoBehaviour
    {
        [SerializeField] private LevelItem[] _levelsItems;

        private void OnEnable()
        {
            SceneDataLoader.OnLoadLevelsData += InitLevelsData;
        }

        private void OnDisable()
        {
            SceneDataLoader.OnLoadLevelsData -= InitLevelsData;
        }

        private void InitLevelsData()
        {
            foreach (var levelItem in _levelsItems)
                levelItem.UpdateLevelItemData();
        }
    }
}