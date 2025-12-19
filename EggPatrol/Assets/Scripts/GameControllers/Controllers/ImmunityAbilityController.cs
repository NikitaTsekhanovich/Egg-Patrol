using System;
using DG.Tweening;
using GameControllers.Controllers.Properties;
using SaveSystems;
using SaveSystems.DataTypes;
using UnityEngine;

namespace GameControllers.Controllers
{
    public class ImmunityAbilityController
    {
        private readonly Action<bool> _useImmunity;
        private readonly ParticleSystem _immunityEffect;
        private readonly HealthHandler _healthHandler;
        private readonly SaveSystem _saveSystem;
        
        private const float TimeAbility = 15f;
        
        private bool _isCanUse;
        
        public ImmunityAbilityController(
            Action<bool> useImmunity,
            ParticleSystem immunityEffect,
            HealthHandler healthHandler,
            SaveSystem saveSystem)
        {
            _useImmunity = useImmunity;
            _immunityEffect = immunityEffect;
            _healthHandler = healthHandler;
            _saveSystem = saveSystem;
            
            _isCanUse = _saveSystem.GetData<StoreSaveData, int>(StoreSaveData.GUIDHasImmunity) == 1;
        }

        public bool TryUse()
        {
            if (_isCanUse)
            {
                _immunityEffect.Play();
                _useImmunity.Invoke(_isCanUse);
                _isCanUse = false;
                _healthHandler.StopAllEffects();
                Use();
                RequestSave();
                return true;
            }
            
            return false;
        }
        
        private void RequestSave()
        {
            var saveData = _isCanUse ? 1 : 0;
            _saveSystem.SaveData<StoreSaveData, int>(saveData, StoreSaveData.GUIDHasImmunity);
        }

        private void Use()
        {
            DOTween.Sequence()
                .AppendInterval(TimeAbility)
                .AppendCallback(() =>
                {
                    _immunityEffect.Stop();
                    _immunityEffect.Clear();
                    _useImmunity.Invoke(_isCanUse);
                });
        }
    }
}
