using GameControllers.Abilities;
using GameControllers.GameEntities.Types;
using UnityEngine;
using UnityEngine.UI;

namespace GameControllers.GameLaunch.Data
{
    public class GameData : MonoBehaviour
    {
        [field: Header("Fish data")]
        [field: SerializeField] public Transform ParentFishes { get; private set; }
        [field: SerializeField] public Transform[] FishSpawnPoints { get; private set; }
        
        [field: Header("Abilities data")]
        [field: SerializeField] public Transform[] AbilitiesSlots { get; private set; }
        [field: SerializeField] public Ability[] Abilities { get; private set; }
        [field: SerializeField] public GameObject AbilityScreen { get; private set; }
        [field: SerializeField] public AudioSource TimerAbilitySound { get; private set; }
        
        [field: Header("Guardians data")]
        [field: SerializeField] public Transform ParentGuardians { get; private set; }
        [field: SerializeField] public Guardian GuardianPrefab { get; private set; }
        
        [field: Header("Time abilities images")]
        [field: SerializeField] public Image[] TimeAbilitiesImages { get; private set; }
    }
}
