using GameControllers.Views;
using SaveSystems;
using SaveSystems.DataTypes;

namespace GameControllers.Controllers
{
    public class DestroyAbility 
    {
        private readonly SaveSystem _saveSystem;
        private readonly DestroyAbilityView _destroyAbilityView;

        private int _currentCountAbilities;

        public DestroyAbility(SaveSystem saveSystem, DestroyAbilityView destroyAbilityView)
        {
            _saveSystem = saveSystem;
            _destroyAbilityView = destroyAbilityView;

            _currentCountAbilities = _saveSystem.GetData<StoreSaveData, int>(StoreSaveData.GUIDDestroyObjectCount);
            _destroyAbilityView.SetAbilities(_currentCountAbilities);
        }

        public bool TryUseAbility()
        {
            return _currentCountAbilities > 0;
        }

        public void UseAbility()
        {
            _currentCountAbilities--;
            _destroyAbilityView.UpdateCountAbilities(_currentCountAbilities);
            
            _saveSystem.SaveData<StoreSaveData, int>(_currentCountAbilities, StoreSaveData.GUIDDestroyObjectCount);
        }
    }
}
