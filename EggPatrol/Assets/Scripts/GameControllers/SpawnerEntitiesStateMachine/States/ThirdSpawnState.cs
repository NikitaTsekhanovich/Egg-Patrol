using GameControllers.Controllers;
using GameControllers.Factories.Properties;
using GameControllers.Models;
using GameControllers.Models.DataContainers;
using GameControllers.Models.GameEntities.Types;
using GameControllers.Models.GameEntities.Types.EdgedWeapons;
using GameControllers.Models.GameEntities.Types.Haystacks;

namespace GameControllers.SpawnerEntitiesStateMachine.States
{
    public class ThirdSpawnState : SpawnState
    {
        private readonly SpawnerStateMachine _spawnerStateMachine;
        
        public ThirdSpawnState(
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
            SpawnConfig = runTimeGameData.ThirdSpawnConfig;
        }

        protected override void EndSpawnWave()
        {
            _spawnerStateMachine.EnterIn<FourthSpawnState>();
        }
    }
}
