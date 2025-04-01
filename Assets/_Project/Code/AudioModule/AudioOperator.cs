using GameCoreModule;
using MAEngine;
using MAEngine.Extention;
using UnityEngine;
using Zenject;

namespace Audio
{
    public class AudioOperator : IAction, IInitialisation, ICleanUp, IFixedExecute
    {
        private const string KEY_MUSIC_VOLUME = "musicVolume";
        private const string KEY_SOUND_VOLUME = "soundVolume";
        
        private AudioSourceView _sourceView;
        private AudioEventBus _audioEventBus;

        private float _currentPauseTime;
        private Timer _musicPauseTimer;

        [Inject]
        public void Construct(AudioSourceView sourceView, AudioEventBus audioEventBus)
        {
            _sourceView = sourceView;
            _audioEventBus = audioEventBus;
        }
        
        public void Initialisation()
        {
            _audioEventBus.OnPlayMusic += PlayMusic;
            _audioEventBus.OnPlaySound += PlaySound;
            _audioEventBus.OnGetAudioSettings += GetAudioSettings;
            _audioEventBus.OnSetMusicVolume += SetMusicVolume;
            _audioEventBus.OnSetSoundVolume += SetSoundVolume;
            LoadAudioSettings();
            _audioEventBus.OnPlayMusic?.Invoke(MusicID.MusicAmbient1);
        }

        public void Cleanup()
        {
            _audioEventBus.OnPlayMusic -= PlayMusic;
            _audioEventBus.OnPlaySound -= PlaySound;
            _audioEventBus.OnGetAudioSettings -= GetAudioSettings;
            _audioEventBus.OnSetMusicVolume -= SetMusicVolume;
            _audioEventBus.OnSetSoundVolume -= SetSoundVolume;
        }
        
        public void FixedExecute(float deltaTime)
        {
            if (_musicPauseTimer != null)
            {
                if (_musicPauseTimer.Wait())
                {
                    ResumeMusic();
                    _musicPauseTimer = null;
                }
            }
        }
        
        private void LoadAudioSettings()
        {
            if (PlayerPrefs.HasKey(KEY_MUSIC_VOLUME))
            {
                _sourceView.MusicSource.volume = PlayerPrefs.GetFloat(KEY_MUSIC_VOLUME);
            }

            if (PlayerPrefs.HasKey(KEY_SOUND_VOLUME))
            {
                _sourceView.SoundSource.volume = PlayerPrefs.GetFloat(KEY_SOUND_VOLUME);
            }
        }

        private void SaveAudioSettings()
        {
            PlayerPrefs.SetFloat(KEY_MUSIC_VOLUME, _sourceView.MusicSource.volume);
            PlayerPrefs.SetFloat(KEY_SOUND_VOLUME, _sourceView.SoundSource.volume);
        }

        private void PlaySound(SoundID id)
        {
            switch (id)
            {
                case SoundID.TestSound:
                    _sourceView.SoundSource.PlayOneShot(_sourceView.TestSoundClip);
                    break;
                case SoundID.SoundClick1:
                    _sourceView.SoundSource.PlayOneShot(_sourceView.ClickSoundClip);
                    break;
                case SoundID.SoundClickUI:
                    _sourceView.SoundSource.PlayOneShot(_sourceView.ClickUIClip);
                    break;
                default:
                    Debug.LogWarning($"No sound clip found for ID: {id}");
                    break;
            }
        }

        private void PlayMusic(MusicID id)
        {
            switch (id)
            {
                case MusicID.TestSound:
                    PauseMusic(1f);
                    _sourceView.MusicSource.PlayOneShot(_sourceView.TestSoundClip);
                    break;
                case MusicID.MusicAmbient1:
                    _sourceView.MusicSource.Stop();
                    _sourceView.MusicSource.clip = _sourceView.MusicClip;
                    _sourceView.MusicSource.Play();
                    break;
                default:
                    Debug.LogWarning($"No music clip found for ID: {id}");
                    break;
            }
        }

        private void PauseMusic(float pauseTime)
        {
            _currentPauseTime = _sourceView.MusicSource.time;
            _musicPauseTimer = new Timer(pauseTime);
            _sourceView.MusicSource.Stop();
        }
        
        private void ResumeMusic()
        {
            _sourceView.MusicSource.time = _currentPauseTime;
            _sourceView.MusicSource.Play();
            _sourceView.MusicSource.time = _currentPauseTime;
        }
        
        private void GetAudioSettings(AudioCallback callback)
        {
            callback.MusicVolume = _sourceView.MusicSource.volume;
            callback.SoundVolume = _sourceView.SoundSource.volume;
        }
        
        private void SetMusicVolume(float volume)
        {
            _sourceView.MusicSource.volume = volume;
            SaveAudioSettings();
        }

        private void SetSoundVolume(float volume)
        {
            _sourceView.SoundSource.volume = volume;
            SaveAudioSettings();
        }
    }
}