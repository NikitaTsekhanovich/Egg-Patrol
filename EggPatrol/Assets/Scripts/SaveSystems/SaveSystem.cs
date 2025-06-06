using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using SaveSystems.DataTypes;
using SaveSystems.Properties;
using UnityEngine;

namespace SaveSystems
{
    public class SaveSystem
    {
        private Dictionary<Type, ISaveDataHandler> _savesData;
        private GameSaveDataContainer _gameSaveDataContainer;
        
        private readonly string _savePath;
        
        private const string SaveFileName = "SaveData.json";
        
        private SaveSystem()
        { 
            #if UNITY_EDITOR
                _savePath = Path.Combine(Application.dataPath, SaveFileName);
            #else 
                _savePath = Path.Combine(Application.persistentDataPath, SaveFileName);
            #endif
            
            if (!File.Exists(_savePath))
            {
                CreateSaveDataContainer();
            }
            else
            {
                LoadData();
            }
        }
        
        private void LoadData()
        {
            var json = File.ReadAllText(_savePath);
            _gameSaveDataContainer = JsonConvert.DeserializeObject<GameSaveDataContainer>(json);
            
            _savesData = new Dictionary<Type, ISaveDataHandler>
            {
                [typeof(MusicSaveData)] = _gameSaveDataContainer.MusicSaveData,
                [typeof(StoreSaveData)] = _gameSaveDataContainer.StoreSaveData,
                [typeof(PlayerSaveData)] = _gameSaveDataContainer.PlayerSaveData,
                [typeof(PlayerGlobalData)] = _gameSaveDataContainer.PlayerGlobalData
            };
            
            foreach (var dataHandler in _savesData.Values)
                dataHandler.RefreshDataForLoad();
        }
        
        private void CreateSaveDataContainer()
        {
            _gameSaveDataContainer = new GameSaveDataContainer();
            
            _savesData = new Dictionary<Type, ISaveDataHandler>
            {
                [typeof(MusicSaveData)] = _gameSaveDataContainer.MusicSaveData,
                [typeof(StoreSaveData)] = _gameSaveDataContainer.StoreSaveData,
                [typeof(PlayerSaveData)] = _gameSaveDataContainer.PlayerSaveData,
                [typeof(PlayerGlobalData)] = _gameSaveDataContainer.PlayerGlobalData
            };
            
            WriteToJson();
        }

        private void WriteToJson()
        {
            foreach (var dataHandler in _savesData.Values)
                dataHandler.RefreshDataForSave();
            
            var json = JsonConvert.SerializeObject(_gameSaveDataContainer, Formatting.Indented);
            File.WriteAllText(_savePath, json);
        }

        public void SaveData<TData, TType>(TType data, string guid)
            where TData : ISaveDataHandler
        {
            _savesData[typeof(TData)].SaveData(data, guid);
            WriteToJson();
        }

        public TType GetData<TData, TType>(string guid)
            where TData : ISaveDataHandler
        {
            return _savesData[typeof(TData)].GetData<TType>(guid);
        }
    }
}
