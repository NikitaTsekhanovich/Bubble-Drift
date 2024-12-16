using SceneLoaderControllers;
using UnityEngine;

namespace MainMenu
{
    public class MainMenuHandler : MonoBehaviour
    {
        [SerializeField] private GameObject _storeScreen;
        [SerializeField] private GameObject _levelsScreen;
        
        public static MainMenuHandler Instance;
        
        private void Awake() 
        {        
            if (Instance == null) 
                Instance = this; 
            else 
                Destroy(this);  
        }

        public void OpenMainMenu(ModeLoadMainMenu modeLoadMainMenu)
        {
            switch (modeLoadMainMenu)
            {
                case ModeLoadMainMenu.Levels:
                    _levelsScreen.SetActive(true);
                    break;
                case ModeLoadMainMenu.Store:
                    _storeScreen.SetActive(true);
                    break;
            }
        }
    }
}
