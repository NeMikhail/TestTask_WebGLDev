using System;

namespace GameCoreModule
{
    public class AudioEventBus
    {
        private Action<SoundID> _onPlaySound;
        private Action<MusicID> _onPlayMusic;
        private Action<AudioCallback> _onGetAudioSettings;
        private Action<float> _onSetMusicVolume;
        private Action<float> _onSetSoundVolume;
        
        public Action<SoundID> OnPlaySound
        { get => _onPlaySound; set => _onPlaySound = value; }
        public Action<MusicID> OnPlayMusic
        { get => _onPlayMusic; set => _onPlayMusic = value; }
        public Action<AudioCallback> OnGetAudioSettings
        { get => _onGetAudioSettings; set => _onGetAudioSettings = value; }
        public Action<float> OnSetMusicVolume
        { get => _onSetMusicVolume; set => _onSetMusicVolume = value; }
        public Action<float> OnSetSoundVolume
        { get => _onSetSoundVolume; set => _onSetSoundVolume = value; }
    }
}