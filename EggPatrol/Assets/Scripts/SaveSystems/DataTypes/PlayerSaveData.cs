using System;
using System.Collections.Generic;

namespace SaveSystems.DataTypes
{
    public class PlayerSaveData : SaveDataHandler
    {
        public const string GUIDEggsCount = "EggsCount";
        public const string GUIDBestScore = "BestScore";
        
        public int EggsCount;
        public int BestScore;
        
        public PlayerSaveData()
        {
            TypeClass = typeof(PlayerSaveData);
                
            SavedData = new Dictionary<Type, Dictionary<string, object>>
            {
                {
                    typeof(int), new Dictionary<string, object>
                    {
                        { GUIDEggsCount, 75 },
                        { GUIDBestScore, 0 }
                    }
                },
            };
        }

        public override void RefreshDataForSave()
        {
            EggsCount = (int)SavedData[typeof(int)][GUIDEggsCount];
            BestScore = (int)SavedData[typeof(int)][GUIDBestScore];
        }

        public override void RefreshDataForLoad()
        {
            SavedData[typeof(int)][GUIDEggsCount] = EggsCount;
            SavedData[typeof(int)][GUIDBestScore] = BestScore;
        }
    }
}
