using System;
using SaveSystems.DataTypes;

namespace SaveSystems
{
    [Serializable]
    public class GameSaveDataContainer
    {
        public MusicSaveData MusicSaveData { get; private set; }
        public StoreSaveData StoreSaveData { get; private set; }
        public PlayerSaveData PlayerSaveData { get; private set; }
        public PlayerGlobalData PlayerGlobalData { get; private set; }
        
        public GameSaveDataContainer()
        {
            MusicSaveData = new MusicSaveData();
            StoreSaveData = new StoreSaveData();
            PlayerSaveData = new PlayerSaveData();
            PlayerGlobalData = new PlayerGlobalData();
        }
    }
}
