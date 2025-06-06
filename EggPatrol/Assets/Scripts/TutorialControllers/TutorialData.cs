using System;
using UnityEngine;

namespace TutorialControllers
{
    [Serializable]
    public struct TutorialData
    {
        [field: SerializeField] public ParticleSystem ClickParticle { get; private set; }
        [field: SerializeField] public HandleAnimator HandleAnimator { get; private set; }
        [field: SerializeField] public Transform EggSpawnPoint { get; private set; }
        [field: SerializeField] public Transform GroundHaystackSpawnPoint { get; private set; }
        [field: SerializeField] public Transform FlyingHaystackSpawnPoint { get; private set; }
        [field: SerializeField] public Transform KnifeSpawnPoint { get; private set; }
        [field: SerializeField] public Transform ButcherKnifeSpawnPoint { get; private set; }
    }
}
