using UnityEngine;

namespace GameControllers.GameEntities.Models
{
    [CreateAssetMenu(fileName = "MovementTargetCharacteristicsData", menuName = "Movement Target Characteristics Data/ Characteristics Data")]
    public class MovementTargetCharacteristicsData : ScriptableObject
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _maximumAttractionDistance;
        [SerializeField] private float _maximumSpeed;
        
        public float Speed => _speed;
        public float MaximumAttractionDistance => _maximumAttractionDistance;
        public float MaximumSpeed => _maximumSpeed;
    }
}
