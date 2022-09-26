using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankGame
{
    public class AudioManager : MonoSingletonGeneric<AudioManager>
    {
        [SerializeField] private AudioSource backgroundAS;
        [SerializeField] private AudioSource gameAS;
        [SerializeField] private AudioSource sfxAS;
        [SerializeField] private List<Sounds> sounds;

        private const string Background_volume = "musicVolume";
        private const string Game_volume = "gameVolumme";
        private const string Sfx_volume = "sfxVolume";

        private void Start()
        {
            ResetSounds();
            PlaySound(SoundType.BackgroundMusic);
        }

        public void PlaySound(SoundType soundType)
        {
            Sounds sound = GetSound(soundType);
            if (sound.audioClip)
            {
                switch (sound.audioSourceType)
                {
                    case AudioSourceType.Sfx:
                        {
                            sfxAS.PlayOneShot(sound.audioClip);
                            break;
                        }
                    case AudioSourceType.background:
                        {
                            backgroundAS.clip = sound.audioClip;
                            backgroundAS.Play();
                            break;
                        }
                    case AudioSourceType.game:
                        {
                            gameAS.clip = sound.audioClip;
                            gameAS.Play();
                            break;
                        }
                }
            }
        }
        private Sounds GetSound(SoundType soundType)
        {
            return sounds.Find(clip => clip.soundType == soundType);
        }

        public void Mute()
        {
            backgroundAS.volume = 0f;
            sfxAS.volume = 0f;
            gameAS.volume = 0f;
        }

        public void StopAudio(AudioSourceType audioType)
        {
            switch (audioType)
            {
                case AudioSourceType.background:
                    {
                        backgroundAS.clip = null;
                        backgroundAS.volume = 0f;
                        break;
                    }
                case AudioSourceType.game:
                    {
                        gameAS.clip = null;
                        gameAS.volume = 0f;
                        break;
                    }
            }
        }
        public void ResetSounds()
        {
            if (PlayerPrefs.HasKey(Background_volume))
            {
                backgroundAS.volume = PlayerPrefs.GetFloat(Background_volume);
            }
            else if (PlayerPrefs.HasKey(Game_volume))
            {
                gameAS.volume = PlayerPrefs.GetFloat(Game_volume);
            }
            else if (PlayerPrefs.HasKey(Sfx_volume))
            {
                sfxAS.volume = PlayerPrefs.GetFloat(Sfx_volume);
            }
        }

        public void SetVolume(AudioSourceType audioSourceType,float _volume)
        {
            switch (audioSourceType)
            {
                case AudioSourceType.Sfx:
                    {
                        sfxAS.volume = _volume;
                        break;
                    }
                case AudioSourceType.background:
                    {
                        backgroundAS.volume = _volume;
                        break;
                    }
                case AudioSourceType.game:
                    {
                        gameAS.volume = _volume;
                        break;
                    }
            }
        }
    }
    [Serializable]
    public struct Sounds
    {
        public SoundType soundType;
        public AudioSourceType audioSourceType;
        public AudioClip audioClip;
    }

    public enum AudioSourceType
    {
        Sfx,
        background,
        game
    }

    public enum SoundType
    {
        BackgroundMusic,
        TankIdel,
        ButtonClick,
        Fire,
        ShellExplode,
        TankExplode,
        Achievement
    }
}

