using System;
using GameControllers.Models.GameEntities.Types;
using GameControllers.Models.GameEntities.Types.EdgedWeapons;
using GameControllers.Models.GameEntities.Types.Haystacks;
using UnityEngine;

namespace GameControllers.Models.DataContainers
{
    [Serializable]
    public struct LoadGameData
    {
        [field: SerializeField] public Transform PlayerSpawnTransform { get; private set; }
        
        [field: Header("Prefabs")]
        [field: SerializeField] public Player PlayerPrefab { get; private set; }
        [field: SerializeField] public Egg EggPrefab { get; private set; }
        [field: SerializeField] public GroundHaystack GroundHaystackPrefab { get; private set; }
        [field: SerializeField] public FireGroundHaystack FireGroundHaystackPrefab { get; private set; }
        [field: SerializeField] public FlyingHaystack FlyingHaystackPrefab { get; private set; }
        [field: SerializeField] public ButcherKnife ButcherKnifePrefab { get; private set; }
        [field: SerializeField] public Knife KnifePrefab { get; private set; }
        [field: SerializeField] public Warning WarningPrefab { get; private set; }
    }
}
