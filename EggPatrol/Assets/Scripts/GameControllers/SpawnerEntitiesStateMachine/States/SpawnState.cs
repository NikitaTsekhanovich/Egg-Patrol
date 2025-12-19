using System;
using System.Collections.Generic;
using DG.Tweening;
using GameControllers.Controllers;
using GameControllers.Controllers.Properties;
using GameControllers.Factories.Properties;
using GameControllers.Models;
using GameControllers.Models.DataContainers;
using GameControllers.Models.GameEntities.Properties;
using GameControllers.Models.GameEntities.Types;
using GameControllers.Models.GameEntities.Types.EdgedWeapons;
using GameControllers.Models.GameEntities.Types.Haystacks;
using GameControllers.SpawnerEntitiesStateMachine.PatternsConfigs;
using GameControllers.StateMachineBasic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GameControllers.SpawnerEntitiesStateMachine.States
{
    public abstract class SpawnState : IState, IHaveUpdate
    {
        private readonly ICanGetPoolEntity<Egg> _eggPool;
        private readonly ICanGetPoolEntity<GroundHaystack> _groundHaystackPool;
        private readonly ICanGetPoolEntity<FlyingHaystack> _flyingHaystackPool;
        private readonly ICanGetPoolEntity<FireGroundHaystack> _fireGroundHaystackPool;
        private readonly ICanGetPoolEntity<ButcherKnife> _butcherKnifePool;
        private readonly ICanGetPoolEntity<Knife> _knifePool;
        private readonly ICanGetPoolEntity<Warning> _warningPool;
        private readonly Transform[] _eggsSpawnPoints;
        private readonly Transform[] _groundHaystackSpawnPoints;
        private readonly Transform[] _fireGroundHaystackSpawnPoints;
        private readonly Transform[] _flyingHaystackSpawnPoints;
        private readonly Transform[] _edgedWeaponSpawnPoints;
        private readonly ScoreHandler _scoreHandler;

        private float _currentWaveTime;
        private Queue<EntitySpawnConfig> _patternSpawnConfigs;
        private EntitySpawnConfig _currentPatternSpawnConfig;
        private bool _isEnd;
        
        protected SpawnStateConfig SpawnConfig;
        
        protected SpawnState(
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
            _eggPool = eggPool;
            _groundHaystackPool = groundHaystackPool;
            _flyingHaystackPool = flyingHaystackPool;
            _fireGroundHaystackPool = fireGroundHaystackPool;
            _butcherKnifePool = butcherKnifePool;
            _knifePool = knifePool;
            _warningPool = warningPool;
            _eggsSpawnPoints = runTimeGameData.EggsSpawnPoints;
            _groundHaystackSpawnPoints = runTimeGameData.GroundHaystackSpawnPoints;
            _fireGroundHaystackSpawnPoints = runTimeGameData.FireGroundHaystackSpawnPoints;
            _flyingHaystackSpawnPoints = runTimeGameData.FlyingHaystackSpawnPoints;
            _edgedWeaponSpawnPoints = runTimeGameData.EdgedWeaponSpawnPoints;
            _scoreHandler = scoreHandler;
        }
        
        public void Enter()
        {
            _isEnd = false;
            _currentWaveTime = 0f;
            _scoreHandler.SetScoreIncreaser(SpawnConfig.ScoreIncreaseValue);
            
            var randomStateConfig = SpawnConfig.PatternSpawnConfigs[Random.Range(0, SpawnConfig.PatternSpawnConfigs.Length)];
            _patternSpawnConfigs = new Queue<EntitySpawnConfig>(randomStateConfig.EntitySpawnConfigs);
            ChoosePattern();
        }

        public void UpdateSystem()
        {
            if (_isEnd)
                return;
            
            _scoreHandler?.UpdateScore();
            
            _currentWaveTime += Time.deltaTime;

            if (_currentWaveTime >= _currentPatternSpawnConfig.StartTime)
            {
                ChooseSpawnEntity(_currentPatternSpawnConfig.TypeEntity, _currentPatternSpawnConfig.IndexSpawn);
                ChoosePattern();
            }
        }
        
        public void Exit()
        {
            
        }

        private void ChoosePattern()
        {
            if (_patternSpawnConfigs.Count > 0)
            {
                _currentPatternSpawnConfig = _patternSpawnConfigs.Dequeue();
            }
            else
            {
                _isEnd = true;
                EndSpawnWave();
            }
        }

        private void ChooseSpawnEntity(Type typeEntity, int indexSpawnPoint)
        {
            if (typeEntity == typeof(Egg))
            {
                SpawnEntity(_eggPool, _eggsSpawnPoints, indexSpawnPoint);
            }
            else if (typeEntity == typeof(GroundHaystack))
            {
                SpawnAttackEntity(_groundHaystackPool, _groundHaystackSpawnPoints, indexSpawnPoint);
            }
            else if (typeEntity == typeof(FlyingHaystack))
            {
                SpawnAttackEntity(_flyingHaystackPool, _flyingHaystackSpawnPoints, indexSpawnPoint);
            }
            else if (typeEntity == typeof(FireGroundHaystack))
            {
                SpawnAttackEntity(_fireGroundHaystackPool, _fireGroundHaystackSpawnPoints, indexSpawnPoint);
            }
            else if (typeEntity == typeof(Knife))
            {
                SpawnAttackEntity(_knifePool, _edgedWeaponSpawnPoints, indexSpawnPoint);
            }
            else if (typeEntity == typeof(ButcherKnife))
            {
                SpawnAttackEntity(_butcherKnifePool, _edgedWeaponSpawnPoints, indexSpawnPoint);
            }
        }

        private void SpawnAttackEntity<T>(ICanGetPoolEntity<T> poolEntity, Transform[] spawnPoints, int indexSpawnPoint)
            where T : IEntity
        {
            var spawnTransform = GetSpawnPosition(spawnPoints, indexSpawnPoint);
            _warningPool.GetPoolEntity(spawnTransform);
            
            DOTween.Sequence()
                .AppendInterval(Warning.WarningDelay)
                .AppendCallback(() => poolEntity.GetPoolEntity(spawnTransform));
        }

        private void SpawnEntity<T>(ICanGetPoolEntity<T> poolEntity, Transform[] spawnPoints, int indexSpawnPoint)
            where T : IEntity
        {
            var spawnTransform = GetSpawnPosition(spawnPoints, indexSpawnPoint);
            poolEntity.GetPoolEntity(spawnTransform);
        }
        
        private Transform GetSpawnPosition(Transform[] spawnPoints, int index)
        {
            if (index != -1)
                return spawnPoints[index];
            
            return spawnPoints[Random.Range(0, spawnPoints.Length)];
        }

        protected abstract void EndSpawnWave();
    }
}
