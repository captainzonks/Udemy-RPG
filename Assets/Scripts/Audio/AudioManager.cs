using System;
using UnityEngine;

namespace Audio
{
    public class AudioManager : MonoBehaviour
    {
        public AudioSource[] sfx;
        public AudioSource[] bgm;

        public static AudioManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                if (Instance != this)
                {
                    Destroy(gameObject);
                }
            }
        }

        private void Start()
        {
            DontDestroyOnLoad(gameObject);
        }

        public void PlaySFX(int soundToPlay)
        {
            if (soundToPlay < sfx.Length)
                sfx[soundToPlay].Play();
        }

        public void PlayBGM(int musicToPlay)
        {
            if (bgm[musicToPlay].isPlaying) return;
            StopMusic();

            if (musicToPlay < bgm.Length)
            {
                bgm[musicToPlay].Play();
            }
        }

        public void StopMusic()
        {
            foreach (var music in bgm)
                music.Stop();
        }
    }
}
