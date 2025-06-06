using System;
using System.Collections.Generic;
using GameControllers.Controllers;
using GameControllers.Factories.Properties;
using GameControllers.Models;
using GameControllers.Models.DataContainers;
using GameControllers.Models.GameEntities.Types;
using GameControllers.Models.GameEntities.Types.EdgedWeapons;
using GameControllers.Models.GameEntities.Types.Haystacks;
using GameControllers.SpawnerEntitiesStateMachine.States;
using GameControllers.StateMachineBasic;

namespace GameControllers.SpawnerEntitiesStateMachine
{
    public class SpawnerStateMachine : StateMachine
    {
        public SpawnerStateMachine(
            ICanGetPoolEntity<Egg> eggPool, 
            ICanGetPoolEntity<GroundHaystack> groundHaystackPool,
            ICanGetPoolEntity<FlyingHaystack> flyingHaystackPool,
            ICanGetPoolEntity<FireGroundHaystack> fireGroundHaystackPool,
            ICanGetPoolEntity<ButcherKnife> butcherKnifePool,
            ICanGetPoolEntity<Knife> knifePool,
            ICanGetPoolEntity<Warning> warningPool,
            RunTimeGameData runTimeGameData,
            ScoreHandler scoreHandler)
        {
            States = new Dictionary<Type, IState>
            {
                [typeof(FirstSpawnState)] = new FirstSpawnState(
                    this, 
                    eggPool, 
                    groundHaystackPool, 
                    flyingHaystackPool, 
                    fireGroundHaystackPool,
                    butcherKnifePool,
                    knifePool,
                    warningPool,
                    runTimeGameData,
                    scoreHandler),
                [typeof(SecondSpawnState)] = new SecondSpawnState(
                    this, 
                    eggPool, 
                    groundHaystackPool, 
                    flyingHaystackPool, 
                    fireGroundHaystackPool,
                    butcherKnifePool,
                    knifePool,
                    warningPool,
                    runTimeGameData,
                    scoreHandler),
                [typeof(ThirdSpawnState)] = new ThirdSpawnState(
                    this, 
                    eggPool, 
                    groundHaystackPool, 
                    flyingHaystackPool, 
                    fireGroundHaystackPool,
                    butcherKnifePool,
                    knifePool,
                    warningPool,
                    runTimeGameData,
                    scoreHandler),
                [typeof(FourthSpawnState)] = new FourthSpawnState(
                    this, 
                    eggPool, 
                    groundHaystackPool, 
                    flyingHaystackPool, 
                    fireGroundHaystackPool,
                    butcherKnifePool,
                    knifePool,
                    warningPool,
                    runTimeGameData,
                    scoreHandler),
                [typeof(FifthSpawnState)] = new FifthSpawnState(
                    this, 
                    eggPool, 
                    groundHaystackPool, 
                    flyingHaystackPool, 
                    fireGroundHaystackPool,
                    butcherKnifePool,
                    knifePool,
                    warningPool,
                    runTimeGameData,
                    scoreHandler),
                [typeof(InfinitySpawnState)] = new InfinitySpawnState(
                    this, 
                    eggPool, 
                    groundHaystackPool, 
                    flyingHaystackPool, 
                    fireGroundHaystackPool,
                    butcherKnifePool,
                    knifePool,
                    warningPool,
                    runTimeGameData,
                    scoreHandler),
                [typeof(BonusSpawnState)] = new BonusSpawnState(
                    this, 
                    eggPool, 
                    runTimeGameData)
            };
            
            EnterIn<FourthSpawnState>();
        }
    }
}
