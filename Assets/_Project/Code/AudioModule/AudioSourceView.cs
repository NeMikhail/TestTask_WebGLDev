using UnityEngine;

namespace Audio
{
    public class AudioSourceView : MonoBehaviour
    {
        [SerializeField] private AudioClip _musicClip;
        [SerializeField] private AudioClip _clickSoundClip;
        [SerializeField] private AudioClip _clickUIClip;
        [SerializeField] private AudioClip _testSoundClip;
    
        [SerializeField] private AudioSource _musicSource;
        [SerializeField] private AudioSource _soundSource;

        public AudioClip MusicClip => _musicClip;
        public AudioClip ClickSoundClip => _clickSoundClip;
        public AudioClip ClickUIClip => _clickUIClip;
        public AudioClip TestSoundClip => _testSoundClip;
        public AudioSource MusicSource => _musicSource;
        public AudioSource SoundSource => _soundSource;
    }
}

