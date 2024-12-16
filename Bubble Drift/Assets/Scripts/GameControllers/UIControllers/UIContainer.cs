using UnityEngine;

namespace GameControllers.UIControllers
{
    public class UIContainer : MonoBehaviour
    {
        [field: SerializeField] public WaveTextUpdater WaveTextUpdater { get; private set; }
        [field: SerializeField] public GameOverScreen GameOverScreen  { get; private set; }
        [field: SerializeField] public LevelCompletedScreen LevelCompletedScreen { get; private set; }
        
        public static UIContainer Instance { get; private set; }

        public void Awake()
        {
            if (Instance == null)
                Instance = this;
            else 
                Destroy(gameObject);
        }
    }
}
