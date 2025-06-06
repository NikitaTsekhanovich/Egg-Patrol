using UnityEngine;

namespace GameControllers.SpawnerEntitiesStateMachine.PatternsConfigs
{
    [CreateAssetMenu(fileName = "SpawnStateConfig", menuName = "Configs/SpawnConfigs/SpawnStateConfig")]
    public class SpawnStateConfig : ScriptableObject
    {
        [field: SerializeField] public float ScoreIncreaseValue { get; private set; }
        [field: SerializeField] public PatternSpawnConfig[] PatternSpawnConfigs { get; private set; }
    }
}
