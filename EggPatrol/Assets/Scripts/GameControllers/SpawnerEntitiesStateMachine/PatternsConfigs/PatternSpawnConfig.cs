using UnityEngine;

namespace GameControllers.SpawnerEntitiesStateMachine.PatternsConfigs
{
    [CreateAssetMenu(fileName = "PatternSpawnConfig", menuName = "Configs/SpawnConfigs/PatternSpawnConfig")]
    public class PatternSpawnConfig : ScriptableObject
    {
        [field: SerializeField] public EntitySpawnConfig[] EntitySpawnConfigs { get; private set; }
    }
}
