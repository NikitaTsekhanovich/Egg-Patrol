using UnityEngine;

namespace GameControllers.Models.Configs
{
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "Configs/PlayerConfig")]
    public class PlayerConfig : ScriptableObject
    {
        [field: Header("Health data")]
        [field: SerializeField] public int MaxHealth { get; private set; }
        
        [field: Header("Movement data")]
        [field: SerializeField] public float Speed { get; private set; }
        [field: SerializeField] public float JumpForce { get; private set; }
        [field: SerializeField] public float SlipTime { get; private set; }
        [field: SerializeField] public float LinerDamping { get; private set; }
        [field: SerializeField] public float SlipForce { get; private set; }
        [field: SerializeField] public float MaxVelocity { get; private set; }
    }
}
