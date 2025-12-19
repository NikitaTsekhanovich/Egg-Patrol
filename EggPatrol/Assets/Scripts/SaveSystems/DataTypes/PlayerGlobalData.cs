using System;
using System.Collections.Generic;

namespace SaveSystems.DataTypes
{
    public class PlayerGlobalData : SaveDataHandler
    {
        public const string GUIDFirstLoad = "FirstLoad";

        public bool IsFirstLoad = true;
        
        public PlayerGlobalData()
        {
            TypeClass = typeof(PlayerSaveData);
                
            SavedData = new Dictionary<Type, Dictionary<string, object>>
            {
                {
                    typeof(bool), new Dictionary<string, object>
                    {
                        { GUIDFirstLoad, true }
                    }
                },
            };
        }
        
        public override void RefreshDataForSave()
        {
            IsFirstLoad = (bool)SavedData[typeof(bool)][GUIDFirstLoad];
        }

        public override void RefreshDataForLoad()
        {
            SavedData[typeof(bool)][GUIDFirstLoad] = IsFirstLoad;
        }
    }
}
