using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;
using System;

namespace Abyss_Call
{
    public class AudioManager
    {
        public Dictionary<string, Song> SongDictionary = new Dictionary<string, Song>();
        public Dictionary<string, SoundEffect> SoundEffectDictionary = new Dictionary<string, SoundEffect>();

        public AudioListener Listener = new AudioListener();
        public AudioEmitter Emitter = new AudioEmitter();
        public SoundEffectInstance Instance;

        private readonly List<string> _songs = new List<string>()
        {
            
        };

        private readonly List<string> _soundEffects = new List<string>()
        {
            "mouseover"
        };

        public string ActualTitle { get; set; } = null;

        public readonly float MaxVolume = 0.1f;

        private float _musicVolume;
        public float MusicVolume
        {
            get
            {
                return _musicVolume;
            }
            set
            {
                _musicVolume = value;
                MediaPlayer.Volume = value;
            }
        }
        public float EffectsVolume { get; set; } = 0.001f;
        public double Elapsed { get; set; } = 0;
        public double FadingTime { get; set; } = 600;
        public bool Fading { get; set; } = false;

        public AudioManager()
        {
            _musicVolume = 0.001f;
            MediaPlayer.Volume = 0;
        }

        public void LoadContent(ContentManager content)
        {
            foreach (string song in _songs)
            {
                SongDictionary.Add(song, content.Load<Song>("Songs\\" + song));
            }

            foreach (string effects in _soundEffects)
            {
                SoundEffectDictionary.Add(effects, content.Load<SoundEffect>("sounds\\" + effects));
            }
        }

        public void Update(double gameTime)
        {
            Elapsed += gameTime;
            Elapsed = Math.Min(Elapsed, FadingTime);

            if (Fading)
            {
                if (ActualTitle is null || ActualTitle == "title" || MediaPlayer.Queue.ActiveSong != SongDictionary[ActualTitle])
                {
                    MediaPlayer.Volume = (float)(Math.Max(1 - Elapsed / FadingTime, 0) * MusicVolume);
                }

                else if (MediaPlayer.Volume < MusicVolume)
                {
                    MediaPlayer.Volume = (float)(Math.Min(Elapsed / FadingTime, 1) * MusicVolume);
                }
            }

            if ((MediaPlayer.Volume == 0 || !Fading) && (ActualTitle is null || ActualTitle == "title" || MediaPlayer.Queue.ActiveSong != SongDictionary[ActualTitle]))
            {
                Elapsed = 0;

                if (ActualTitle is null) { MediaPlayer.Stop(); }

                foreach (string song in _songs)
                {
                    if (ActualTitle == song)
                    {
                        MediaPlayer.Play(SongDictionary[ActualTitle]);
                        break;
                    }
                }
            }
        }

        public void PlayMusic(string newTitle, bool fading = true, double fadingTime = 500)
        {
            if (newTitle == ActualTitle) return;

            Elapsed = 0;

            Fading = fading;
            FadingTime = fadingTime;

            ActualTitle = newTitle;
        }

        public void StopMusic(bool fading = true, double fadingTime = 500)
        {
            ActualTitle = null;

            Fading = fading;
            FadingTime = fadingTime;
        }

        public void PlayEffect(string soundEffect, float pich = 0, float pan = 0, float volume = 1)
        {
            var effect = _soundEffects.Find(e => e == soundEffect);

            if (effect is null)
                return;

            SoundEffectDictionary[effect].Play(EffectsVolume * volume, pich, pan);
        }

        public void PlayEffect3D(string soundEffect, Point from, Point to, float pich = 0, float volume = 1)
        {
            float distance = 0.008f * (float)Math.Sqrt((from.X - to.X) * (from.X - to.X) + (from.Y - to.Y) * (from.Y - to.Y));
            float balance = Math.Min(Math.Abs(to.X - from.X) * 0.005f, 1);
            float vol = Math.Min(EffectsVolume / (distance * distance), EffectsVolume) * volume;

            if (vol / volume <= EffectsVolume * 0.025) return;

            var effect = _soundEffects.Find(e => e == soundEffect + "_mono");

            if (effect is null)
                return;

            if (to.X > from.X)
            {
                SoundEffectDictionary[effect].Play((1 + balance) * vol * 2, pich, 1);
                SoundEffectDictionary[effect].Play((1 - balance) * vol * 2, pich, -1);
            }
            else
            {
                SoundEffectDictionary[effect].Play((1 + balance) * vol * 2, pich, -1);
                SoundEffectDictionary[effect].Play((1 - balance) * vol * 2, pich, 1);
            }
        }
    }
}
