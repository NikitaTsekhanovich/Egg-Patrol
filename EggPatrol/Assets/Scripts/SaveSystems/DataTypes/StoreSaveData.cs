using System;
using System.Collections.Generic;

namespace SaveSystems.DataTypes
{
    public class StoreSaveData : SaveDataHandler
    {
        public const string GUIDDestroyObjectCount = "DestroyObjectCount";
        public const string GUIDLivesCount = "LivesCount";
        public const string GUIDHasImmunity = "HasImmunity";
        
        public int DestroyObjectCount;
        public int LivesCount;
        public int HasImmunity;

        public StoreSaveData()
        {
            TypeClass = typeof(StoreSaveData);
            
            SavedData = new Dictionary<Type, Dictionary<string, object>>
            {
                {
                    typeof(int), new Dictionary<string, object>
                    {
                        { GUIDDestroyObjectCount, 0 },
                        { GUIDLivesCount, 0 },
                        { GUIDHasImmunity, 0 }
                    }
                },
            };
        }

        public override void RefreshDataForSave()
        {
            DestroyObjectCount = (int)SavedData[typeof(int)][GUIDDestroyObjectCount];
            LivesCount = (int)SavedData[typeof(int)][GUIDLivesCount];
            HasImmunity = (int)SavedData[typeof(int)][GUIDHasImmunity];
        }

        public override void RefreshDataForLoad()
        {
            SavedData[typeof(int)][GUIDDestroyObjectCount] = DestroyObjectCount;
            SavedData[typeof(int)][GUIDLivesCount] = LivesCount;
            SavedData[typeof(int)][GUIDHasImmunity] = HasImmunity;
        }
    }
}
