using System;
using System.Collections.Generic;
using SaveSystems.Properties;
using UnityEngine;

namespace SaveSystems
{
    public abstract class SaveDataHandler : ISaveDataHandler
    {
        protected Type TypeClass;
        protected Dictionary<Type, Dictionary<string, object>> SavedData;

        public abstract void RefreshDataForSave();
        
        public abstract void RefreshDataForLoad();

        public void SaveData<TType>(TType data, string guid)
        {
            try
            {
                var typesData = SavedData[typeof(TType)];

                try
                {
                    typesData[guid] = data;
                }
                catch (KeyNotFoundException)
                {
                    Debug.LogError($"{TypeClass}: Key GUID not found");
                }
            }
            catch (KeyNotFoundException)
            {
                Debug.LogError($"{TypeClass}: Incorrect type of data being transmitted");
            }
        }

        public TType GetData<TType>(string guid)
        {
            TType data = default;
            
            try
            {
                var typesData = SavedData[typeof(TType)];

                try
                {
                    data = (TType)typesData[guid];
                }
                catch (KeyNotFoundException)
                {
                    Debug.LogError($"{TypeClass}: Key GUID not found. GUID = {guid}");
                }
                catch (InvalidCastException)
                {
                    Debug.LogError($"{TypeClass}: Invalid data type. GUID = {guid}");
                }
            }
            catch (KeyNotFoundException)
            {
                Debug.LogError($"{TypeClass}: Incorrect type of data being transmitted");
            }

            return data;
        }
    }
}
