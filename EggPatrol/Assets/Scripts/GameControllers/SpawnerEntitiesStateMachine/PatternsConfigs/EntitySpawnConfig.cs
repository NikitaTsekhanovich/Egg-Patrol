using System;
using GameControllers.Models.GameEntities;
using UnityEngine;

namespace GameControllers.SpawnerEntitiesStateMachine.PatternsConfigs
{
    [Serializable]
    public struct EntitySpawnConfig
    {
        [field: SerializeField] private Entity _entity;

        [field: SerializeField] public float StartTime { get; private set; }
        [field: SerializeField] public int IndexSpawn { get; private set; }
        
        public Type TypeEntity => _entity.GetType();
    }
}
