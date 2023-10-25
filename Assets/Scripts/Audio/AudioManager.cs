using Interfaces.Audio;
using UnityEngine;
using Zenject;

namespace Audio
{
    public class AudioManager : IAudioManager
    {
        private readonly AudioSource _audioSource;

        [Inject]
        public AudioManager(AudioSource audioSource)
        {
            _audioSource = audioSource;
        }

        public void PlaySound()
        {
            if (_audioSource.isPlaying) _audioSource.Stop();
            _audioSource.Play();
        }
    }
}