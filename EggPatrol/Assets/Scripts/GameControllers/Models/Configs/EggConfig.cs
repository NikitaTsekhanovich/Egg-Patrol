using UnityEngine;

namespace GameControllers.Models.Configs
{
    [CreateAssetMenu(fileName = "EggConfig", menuName = "Configs/EggConfig")]
    public class EggConfig : ScriptableObject
    {
        [field: SerializeField] public float Speed { get; private set; }
        [field: SerializeField] public int ScoreValue { get; private set; }
    }
}
