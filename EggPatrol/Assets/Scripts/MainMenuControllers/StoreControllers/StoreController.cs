using System.Collections.Generic;
using MusicSystem;
using SaveSystems;
using SaveSystems.DataTypes;
using TMPro;
using UnityEngine;
using Zenject;

namespace MainMenuControllers.StoreControllers
{
    public class StoreController : MonoBehaviour
    {
        [SerializeField] private List<StoreItem> _storeItems;
        [SerializeField] private TMP_Text _eggsText;
        
        [Inject] private SaveSystem _saveSystem;
        [Inject] private MusicSwitcher _musicSwitcher;

        private int _currentEggs;

        private void Awake()
        {
            InitStore();
            InitStoreItems();
        }

        private void InitStore()
        {
            _currentEggs = _saveSystem.GetData<PlayerSaveData, int>(PlayerSaveData.GUIDEggsCount);
            _eggsText.text = _currentEggs.ToString();
        }

        private void InitStoreItems()
        {
            foreach (var storeItem in _storeItems)
            {
                storeItem.Init(CanBuyItem, BuyItem, _saveSystem);
            }
        }

        private bool CanBuyItem(int price)
        {
            return _currentEggs - price >= 0;
        }

        private void BuyItem(int price)
        {
            _currentEggs -= price;
            _eggsText.text = _currentEggs.ToString();
            _musicSwitcher.PlayPurchaseSound();

            foreach (var storeItem in _storeItems)
            {
                storeItem.UpdateStatus();
            }
            
            SavePlayerData();
        }

        private void SavePlayerData()
        {
            _saveSystem.SaveData<PlayerSaveData, int>(_currentEggs, PlayerSaveData.GUIDEggsCount);
        }
    }
}
