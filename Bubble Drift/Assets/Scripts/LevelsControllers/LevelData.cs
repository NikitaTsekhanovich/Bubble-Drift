using UnityEngine;

namespace LevelsControllers
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "Levels data/ Level")]
    public class LevelData : ScriptableObject
    {
        [Header("Level info")]
        [SerializeField] private int _index;
        
        [Header("Waves info")]
        [SerializeField] private GameWave[] _gameWaves;

        public int Index => _index;
        public GameWave[] GameWaves => _gameWaves;
        
        public TypeStateObject TypeStateLevel =>
            PlayerPrefs.GetInt($"{LevelDataKeys.LevelOpenKey}{_index}") == (int)TypeStateObject.IsClosed ? 
                TypeStateObject.IsClosed : TypeStateObject.IsOpen;
    }
}