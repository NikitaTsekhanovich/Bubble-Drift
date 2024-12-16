using LevelsControllers;
using StoreControllers;

namespace SceneLoaderControllers
{
    public class LevelConfig
    {
        public LevelData LevelData { get; private set; }
        public StoreItemData StoreItemData { get; private set; }

        public LevelConfig(LevelData levelData, StoreItemData storeItemData)
        {
            LevelData = levelData;
            StoreItemData = storeItemData;
        }
    }
}
