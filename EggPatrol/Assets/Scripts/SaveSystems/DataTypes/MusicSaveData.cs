using System;
using System.Collections.Generic;

namespace SaveSystems.DataTypes
{
    public class MusicSaveData : SaveDataHandler
    {
        public const string GUIDMusicState = "MusicState";
        public const string GUIDSoundEffectsState = "SoundEffectsState";
        
        public bool MusicState = true;
        public bool SoundEffectsState = true;
        
        public MusicSaveData()
        {
            TypeClass = typeof(MusicSaveData);
            
            SavedData = new Dictionary<Type, Dictionary<string, object>>
            {
                {
                    typeof(bool), new Dictionary<string, object>
                    {
                        { GUIDMusicState, true },
                        { GUIDSoundEffectsState, true }
                    }
                },
            };
        }

        public override void RefreshDataForSave()
        {
            MusicState = (bool)SavedData[typeof(bool)][GUIDMusicState];
            SoundEffectsState = (bool)SavedData[typeof(bool)][GUIDSoundEffectsState];
        }

        public override void RefreshDataForLoad()
        {
            SavedData[typeof(bool)][GUIDMusicState] = MusicState;
            SavedData[typeof(bool)][GUIDSoundEffectsState] = SoundEffectsState;
        }
    }
}
