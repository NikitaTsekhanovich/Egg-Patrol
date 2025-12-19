using System;
using SaveSystems;
using SaveSystems.DataTypes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MainMenuControllers.StoreControllers
{
    public class StoreItem : MonoBehaviour
    {
        [SerializeField] private StoreItemConfig _storeItemConfig;
        [SerializeField] private TMP_Text _priceText;
        [SerializeField] private TMP_Text _statusText;
        [SerializeField] private Button _buyButton;

        private Func<int, bool> _canBuyItem;
        private Action<int> _buyItem;
        private int _price;
        private int _maxCount;
        private SaveSystem _saveSystem;
        private int _currentCount;

        public void Init(Func<int, bool> canBuyItem, Action<int> buyItem, SaveSystem saveSystem)
        {
            _canBuyItem = canBuyItem;
            _buyItem = buyItem;
            _saveSystem = saveSystem;
            _currentCount = _saveSystem.GetData<StoreSaveData, int>(_storeItemConfig.GUID);
            
            _price = _storeItemConfig.Price;
            _maxCount = _storeItemConfig.MaxCount;
            
            _priceText.text = _price.ToString();
            UpdateStatus();
        }

        public void ClickBuyItem()
        {
            _currentCount++;
            _buyItem.Invoke(_price);
            _saveSystem.SaveData<StoreSaveData, int>(_currentCount, _storeItemConfig.GUID);
            UpdateStatus();
        }
        
        public void UpdateStatus()
        {
            _statusText.text = $"Buy {_currentCount}/{_maxCount}";
            _buyButton.interactable = _currentCount < _maxCount && _canBuyItem.Invoke(_price);
        }
    }
}
