using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OctopusPathfinder.Manager
{
    public enum SongBackground
    {
        songInMenu,
        songInGame
    }

    public enum Sounds
    {
        Click,
        OnButton,
        Load
    }

    
    public class SoundManager
    {
        public static bool bIsSongInGame = false;

        const float VOLSONG = 0.7f;
        const float VOLSOUND = 1;
        static Song songInMenu, songInGame;
        static SoundEffect seClick, seOnButton, seLoad;
        static bool bMuteSong = false;
        static bool bMuteSound = false;
        static float fSongVolume = VOLSONG, fSoundVolume = VOLSOUND;


        public static void LoadContent(ContentManager Content)
        {
            songInMenu = Content.Load<Song>(@"Sound\Menu");
            songInGame = Content.Load<Song>(@"Sound\InGame");
            seClick = Content.Load<SoundEffect>(@"Sound\Click");
            seOnButton = Content.Load<SoundEffect>(@"Sound\On");
            seLoad = Content.Load<SoundEffect>(@"Sound\Load");
        }

        public static bool MuteSong
        {
            get
            {
                return bMuteSong;
            }

            set
            {
                bMuteSong = value;
                MediaPlayer.IsMuted = bMuteSong;

                if (bMuteSong == false)
                {
                    fSongVolume = VOLSONG;
                }
            }
        }

        public static bool MuteSound
        {
            get
            {
                return bMuteSound;
            }

            set
            {
                bMuteSound = value;

                if (bMuteSound == false)
                {
                    fSoundVolume = VOLSOUND;
                }
                else
                {
                    fSoundVolume = 0;
                }
            }
        }

        public static void PlaySong(SongBackground songID)
        {
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = fSongVolume;

            switch (songID)
            {
                case SongBackground.songInMenu:
                    if (songInMenu != null)
                    {
                        MediaPlayer.Play(songInMenu);
                        bIsSongInGame = false;
                    }

                    return;


                case SongBackground.songInGame:
                    if (songInGame != null)
                    {
                        MediaPlayer.Play(songInGame);
                        bIsSongInGame = true;
                    }

                    return;

                //default:
                //    return;
            }
        }

        public static void PlaySound(Sounds soundID)
        {
            SoundEffect.MasterVolume = fSoundVolume;

            switch (soundID)
            {
                case Sounds.Click:
                    if (seClick != null)
                    {
                        seClick.Play();
                    }

                    return;


                case Sounds.OnButton:
                    if (seOnButton != null)
                    {
                        seOnButton.Play();
                    }

                    return;

                case Sounds.Load:
                    if (seLoad != null)
                    {
                        seLoad.Play();
                    }

                    return;
            }
        }

        public static void StopSong()
        {
            MediaPlayer.Stop();
        }
    }
}
