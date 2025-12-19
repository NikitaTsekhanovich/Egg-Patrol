using GameControllers.Controllers.Properties;
using GameControllers.Factories.Properties;
using GameControllers.Models.DataContainers;
using GameControllers.Models.GameEntities.Types;
using GameControllers.StateMachineBasic;
using UnityEngine;

namespace GameControllers.SpawnerEntitiesStateMachine.States
{
    public class BonusSpawnState : IState, IHaveUpdate
    {
        private readonly SpawnerStateMachine _spawnerStateMachine;
        private readonly Transform[] _spawnPoints;
        private readonly ICanGetPoolEntity<Egg> _eggPool;
        
        private const float MaxSpawnTime = 15f;
        private const float MinSpawnTime = 10f;
        private const float DelaySpawnTime = 1f;
        
        private float _currentSpawnTime;
        private float _currentDelaySpawn;
        private float _timeSpawn;
        
        public BonusSpawnState(
            SpawnerStateMachine spawnerStateMachine,
            ICanGetPoolEntity<Egg> eggPool,
            RunTimeGameData runTimeGameData)
        {
            _spawnerStateMachine = spawnerStateMachine;
            _eggPool = eggPool;
            _spawnPoints = runTimeGameData.EggsSpawnPoints;
        }

        public void Enter()
        {
            _currentSpawnTime = 0f;
            _currentDelaySpawn = 0f;
            _timeSpawn = Random.Range(MinSpawnTime, MaxSpawnTime);
        }

        public void Exit()
        {
            
        }

        public void UpdateSystem()
        {
            _currentSpawnTime += Time.deltaTime;
            _currentDelaySpawn += Time.deltaTime;

            if (_currentDelaySpawn >= DelaySpawnTime)
            {
                _currentDelaySpawn = 0f;
                var randomPoint = _spawnPoints[Random.Range(0, _spawnPoints.Length)];
                _eggPool.GetPoolEntity(randomPoint);
            }

            if (_currentSpawnTime >= _timeSpawn)
            {
                _currentSpawnTime = 0f;
                EndSpawnWave();
            }
        }
        
        private void EndSpawnWave()
        {
            _spawnerStateMachine.EnterIn<InfinitySpawnState>();
        }
    }
}
