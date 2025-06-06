using System;
using GameControllers.SpawnerEntitiesStateMachine.PatternsConfigs;
using UnityEngine;

namespace GameControllers.Models.DataContainers
{
    [Serializable]
    public struct RunTimeGameData
    {
        [field: SerializeField] public ParticleSystem ClickParticle { get; private set; }
        
        [field: Header("Spawn points")]
        [field: SerializeField] public Transform[] EggsSpawnPoints { get; private set; }
        [field: SerializeField] public Transform[] GroundHaystackSpawnPoints { get; private set; }
        [field: SerializeField] public Transform[] FireGroundHaystackSpawnPoints { get; private set; }
        [field: SerializeField] public Transform[] FlyingHaystackSpawnPoints { get; private set; }
        [field: SerializeField] public Transform[] EdgedWeaponSpawnPoints { get; private set; }
        
        [field: Header("Spawn configs")]
        [field: SerializeField] public SpawnStateConfig FirstSpawnConfig { get; private set; }
        [field: SerializeField] public SpawnStateConfig SecondSpawnConfig { get; private set; }
        [field: SerializeField] public SpawnStateConfig ThirdSpawnConfig { get; private set; }
        [field: SerializeField] public SpawnStateConfig FourthSpawnConfig { get; private set; }
        [field: SerializeField] public SpawnStateConfig FifthSpawnConfig { get; private set; }
        [field: SerializeField] public SpawnStateConfig InfinitySpawnConfig { get; private set; }
    }
}
