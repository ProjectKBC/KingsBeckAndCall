using System.Collections;
using UnityEngine;
using old_0609;

namespace Ria
{
    public enum SOUND_EFFECT
    {
        VERONICA_NORMAL_SHOT = 0,
        VERONICA_UNIQUE_SHOT = 1,
    }

    public enum BACK_GROUND_MUSIC
    {
        GAME_STAGE_DEBUG = 0,
        TITLE = 1,
        MENU = 2,
        GAME_MENU = 3,
        GAME_STAGE01 = 4,
    }

    [System.Serializable]
    public sealed class SoundManager : ChildManager
    {
        [SerializeField]
        private AudioClip[] seClips;
        [SerializeField]
        private AudioClip[] bgmClips;

        public static SoundManager GI { get; private set; }
        private AudioSource bgmSource;
        private AudioSource[] seSources = new AudioSource[16];
        
        protected override void OnInit()
        {
            GI = this;

            bgmSource = gameObject.AddComponent<AudioSource>();
            for (int i = 0; i < seSources.Length; ++i)
            {
                seSources[i] = gameObject.AddComponent<AudioSource>();
            }
            
            bgmSource.loop = true;
        }

        protected override void OnRun()
        {

        }

        // SE系 -----------------------------------------------------------------------

        private int SearchSESouresIndex()
        {
            for (int i = 0; i < seSources.Length; ++i)
            {
                if (!seSources[i].isPlaying)
                {
                    return i;
                }
            }

            return -1;
        }

        public void PlaySE(SOUND_EFFECT _se)
        {
            int index = SearchSESouresIndex();
            if (index != -1)
            {
                seSources[index].PlayOneShot(seClips[(int)_se]);
            }
        }

        // BGM系 ----------------------------------------------------------------------

        public void StopBGM()
        {
            bgmSource.Stop();
        }

        public void PlayBGM(BACK_GROUND_MUSIC _bgm)
        {
            if (bgmSource.isPlaying)
            {
                Debug.Log("BGMがStopされていません。");
            }

            bgmSource.clip = bgmClips[(int)_bgm];
            bgmSource.Play();
        }
    }
}