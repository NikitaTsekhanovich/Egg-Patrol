using System;
using GameControllers.Controllers.Properties;
using GameControllers.EntitiesStateMachine;
using GameControllers.EntitiesStateMachine.States;
using GameControllers.Models.Enums;
using GameControllers.Views;
using SaveSystems;
using SaveSystems.DataTypes;
using UnityEngine;

namespace GameControllers.Controllers
{
    public class HealthHandler 
    {
        private readonly HealthView _healthView;
        private readonly int _maxHealth;
        private readonly Action<DamageType> _playerDeath;
        private readonly ParticleSystem _combustionEffect;
        private readonly ParticleSystem _bleedParticleEffect;
        private readonly PlayerStateMachine _playerStateMachine;
        private readonly SaveSystem _saveSystem;
        private readonly AudioSource _hitDamageSound;
        private readonly AudioSource _periodicDamageSound;
        private readonly AudioSource _resurrectSound;
        
        private int _currentHealth;
        private int _currentHearts;
        private float _currentPeriodicTime;
        private int _periodicDamage;
        private float _periodicDamageDuration;
        private float _timeInterval;
        private bool _isTakePeriodicDamage;
        private DamageType _periodicDamageType;
        private bool _hasInvulnerable;
        
        public HealthHandler(
            HealthView healthView,
            int maxHealth,
            SaveSystem saveSystem,
            Action<DamageType> playerDeath,
            ParticleSystem combustionEffect,
            ParticleSystem bleedParticleEffect,
            PlayerStateMachine playerStateMachine,
            AudioSource hitDamageSound,
            AudioSource periodicDamageSound,
            AudioSource resurrectSound,
            bool hasInvulnerable)
        {
            _healthView = healthView;
            _maxHealth = maxHealth;
            _playerDeath = playerDeath;
            _combustionEffect = combustionEffect;
            _bleedParticleEffect = bleedParticleEffect;
            _playerStateMachine = playerStateMachine;
            _hitDamageSound = hitDamageSound;
            _periodicDamageSound = periodicDamageSound;
            _resurrectSound = resurrectSound;
            _hasInvulnerable = hasInvulnerable;
            
            _currentHealth = _maxHealth;
            _saveSystem = saveSystem;

            _currentHearts = _saveSystem.GetData<StoreSaveData, int>(StoreSaveData.GUIDLivesCount);
            _healthView.SetHearts(_currentHearts);
        }

        public void TakeDamage(DamageType damageType, int damage)
        {
            CheckTypeDamage(damageType);
            
            if (_hasInvulnerable)
                return;
            
            _currentHealth -= damage;
            _healthView.UpdateHealth(_currentHealth / (float)_maxHealth);

            if (_currentHealth <= 0)
            {
                if (TryResurrect())
                {
                    _currentHealth = _maxHealth;
                    _healthView.UpdateHealth(_currentHealth / (float)_maxHealth);
                }
                else
                {
                    _healthView.UpdateHealth(0f);
                    _playerDeath.Invoke(damageType);
                    StopAllEffects();
                }
            }
        }

        public void TakePeriodicDamage()
        {
            if (!_isTakePeriodicDamage) return;
            
            _currentPeriodicTime -= Time.deltaTime;

            if (_currentPeriodicTime <= 0f)
            {
                _currentPeriodicTime = _timeInterval;
                _periodicDamageDuration -= _timeInterval;
                TakeDamage(_periodicDamageType, _periodicDamage);
            }

            if (_periodicDamageDuration <= 0f)
            {
                _currentPeriodicTime = 0f;
                _isTakePeriodicDamage = false;
                _combustionEffect.Stop();
            }
        }

        public void SetPeriodicDamage(DamageType damageType, int damage, float timeInterval, float periodicDamageDuration)
        {
            _periodicDamageType = damageType;
            _periodicDamage = damage;
            _timeInterval = timeInterval;
            _periodicDamageDuration = periodicDamageDuration;
            
            _isTakePeriodicDamage = true;
        }

        public void StopAllEffects()
        {
            _combustionEffect.Stop();
            _bleedParticleEffect.Stop();
            _periodicDamageDuration = 0f;
        }

        private void CheckTypeDamage(DamageType damageType)
        {
            switch (damageType)
            {
                case DamageType.Hit:
                    _hitDamageSound.Play();
                    Stun();
                    break;
                case DamageType.Cut:
                    _hitDamageSound.Play();
                    Bleed();
                    break;
                case DamageType.Combustion:
                    _periodicDamageSound.Play();
                    Burn();
                    break;
                case DamageType.Electricity:
                    break;
            }
        }

        private void Stun()
        {
            _playerStateMachine?.EnterIn<StunState>();
        }

        private void Burn()
        {
            _combustionEffect.Play();
        }
        
        private void Bleed()
        {
            _bleedParticleEffect.Play();
        }

        private bool TryResurrect()
        {
            if (_currentHearts - 1 >= 0)
            {
                _resurrectSound.Play();
                _currentHearts--;
                _saveSystem.SaveData<StoreSaveData, int>(_currentHearts, StoreSaveData.GUIDLivesCount);
                _healthView.UpdateHearts(_currentHearts);
                return true;
            }

            return false;
        }
    }
}
