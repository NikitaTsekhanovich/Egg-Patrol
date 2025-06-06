using SaveSystems;
using SaveSystems.DataTypes;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using Zenject;

namespace MusicSystem
{
    public class MusicController
    {
        [Inject] private SaveSystem _saveSystems;

        private readonly AudioMixer _mixer;
        private readonly Sprite _musicOnImage;
        private readonly Sprite _musicOffImage;
        private readonly Sprite _effectsOnImage;
        private readonly Sprite _effectsOffImage;
        
        private const int VolumeOn = 0;
        private const int VolumeOff = -80;
        private const string MusicMixerName = "Music";
        private const string EffectsMixerName = "SoundEffects";
        
        private bool _musicOn;
        private bool _effectsOn;

        private MusicController(
            AudioMixer mixer, 
            Sprite musicOnImage, 
            Sprite musicOffImage, 
            Sprite effectsOnImage, 
            Sprite effectsOffImage)
        {
            _mixer = mixer;
            _musicOnImage = musicOnImage;
            _musicOffImage = musicOffImage;
            _effectsOnImage = effectsOnImage;
            _effectsOffImage = effectsOffImage;
        }
        
        [Inject]
        private void Construct()
        {
            LoadMusicData();
        }

        private void LoadMusicData()
        {
            _musicOn = _saveSystems.GetData<MusicSaveData, bool>(MusicSaveData.GUIDMusicState);
            _effectsOn = _saveSystems.GetData<MusicSaveData, bool>(MusicSaveData.GUIDSoundEffectsState);
        }

        private void ChangeVolume(bool isOn, string mixerName, Image currentImage,
            Sprite onImage, Sprite offImage)
        {
            if (isOn)
            {
                _mixer.SetFloat(mixerName, VolumeOn);
                currentImage.sprite = onImage;
            }
            else
            {
                _mixer.SetFloat(mixerName, VolumeOff);
                currentImage.sprite = offImage;
            }
        }

        public void CheckMusicState(Image currentMusicImage)
        {
            ChangeVolume(_musicOn, MusicMixerName, currentMusicImage,
                _musicOnImage, _musicOffImage);
        }

        public void CheckSoundEffectsState(Image currentEffectsImage)
        {
            ChangeVolume(_effectsOn, EffectsMixerName, currentEffectsImage,
                _effectsOnImage, _effectsOffImage);
        }

        public void ChangeMusicState(Image currentMusicImage)
        {
            _musicOn = !_musicOn;
            CheckMusicState(currentMusicImage);
            _saveSystems.SaveData<MusicSaveData, bool>(_musicOn, MusicSaveData.GUIDMusicState);
        }

        public void ChangeSoundEffectsState(Image currentEffectsImage)
        {
            _effectsOn = !_effectsOn;
            CheckSoundEffectsState(currentEffectsImage);
            _saveSystems.SaveData<MusicSaveData, bool>(_effectsOn, MusicSaveData.GUIDSoundEffectsState);
        }
    }
}