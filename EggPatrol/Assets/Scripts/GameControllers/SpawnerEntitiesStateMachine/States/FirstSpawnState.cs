using GameControllers.Controllers;
using GameControllers.Factories.Properties;
using GameControllers.Models;
using GameControllers.Models.DataContainers;
using GameControllers.Models.GameEntities.Types;
using GameControllers.Models.GameEntities.Types.EdgedWeapons;
using GameControllers.Models.GameEntities.Types.Haystacks;

namespace GameControllers.SpawnerEntitiesStateMachine.States
{
    public class FirstSpawnState : SpawnState
    {
        private readonly SpawnerStateMachine _spawnerStateMachine;

        public FirstSpawnState(
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
            SpawnConfig = runTimeGameData.FirstSpawnConfig;
        }

        protected override void EndSpawnWave()
        {
            _spawnerStateMachine.EnterIn<SecondSpawnState>();
        }
    }
}
