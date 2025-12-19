using GameControllers.Controllers;
using GameControllers.Factories.Properties;
using GameControllers.Models;
using GameControllers.Models.DataContainers;
using GameControllers.Models.GameEntities.Types;
using GameControllers.Models.GameEntities.Types.EdgedWeapons;
using GameControllers.Models.GameEntities.Types.Haystacks;
using UnityEngine;

namespace GameControllers.SpawnerEntitiesStateMachine.States
{
    public class InfinitySpawnState : SpawnState
    {
        private readonly SpawnerStateMachine _spawnerStateMachine;
        
        public InfinitySpawnState(
            SpawnerStateMachine spawnerStateMachine,
            ICanGetPoolEntity<Egg> eggPool,
            ICanGetPoolEntity<GroundHaystack> groundHaystackPool,
            ICanGetPoolEntity<FlyingHaystack> flyingHaystackPool,
            ICanGetPoolEntity<FireGroundHaystack> fireGroundHaystackPool,
            ICanGetPoolEntity<ButcherKnife> butcherKnifePool,
            ICanGetPoolEntity<Knife> knifePool,
            ICanGetPoolEntity<Warning> warningPool,
            RunTimeGameData runTimeGameData,
            ScoreHandler scoreHandler) : 
            base(eggPool, 
                groundHaystackPool,
                flyingHaystackPool,
                fireGroundHaystackPool,
                butcherKnifePool,
                knifePool,
                warningPool,
                runTimeGameData,
                scoreHandler)
        {
            _spawnerStateMachine = spawnerStateMachine;
            SpawnConfig = runTimeGameData.InfinitySpawnConfig;
        }

        protected override void EndSpawnWave()
        {
            var randomValue = Random.Range(0, 10);
            
            if (randomValue == 0)
                _spawnerStateMachine.EnterIn<BonusSpawnState>();
            else
                _spawnerStateMachine.EnterIn<InfinitySpawnState>();
        }
    }
}
