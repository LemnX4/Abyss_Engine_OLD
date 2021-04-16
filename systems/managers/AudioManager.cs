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

        private List<string> _songs = new List<string>()
        {
            
        };

        private List<string> _soundEffects = new List<string>()
        {
            "mouseover"
        };

        private string _actualTitle = "";

        public static readonly float maxVolume = 0.1f;
        private float _musicVolume = 0.001f, _effectsVolume = 0.001f;
        private float _elapsed = 0, _fadingTime = 600;
        private bool _fading = false;

        public AudioManager()
        {
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

        public void Update(GameTime gameTime)
        {
            _elapsed += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            _elapsed = Math.Min(_elapsed, _fadingTime);

            if (_fading)
            {
                if (_actualTitle == "" || _actualTitle == "title" || MediaPlayer.Queue.ActiveSong != SongDictionary[_actualTitle])
                {
                    MediaPlayer.Volume = Math.Max(1 - _elapsed / _fadingTime, 0) * _musicVolume;
                }

                else if (MediaPlayer.Volume < _musicVolume)
                {
                    MediaPlayer.Volume = Math.Min(_elapsed / _fadingTime, 1) * _musicVolume;
                }
            }


            if ((MediaPlayer.Volume == 0 || !_fading) && (_actualTitle == "" || _actualTitle == "title" || MediaPlayer.Queue.ActiveSong != SongDictionary[_actualTitle]))
            {
                _elapsed = 0;

                if (_actualTitle == "") { MediaPlayer.Stop(); }

                foreach (string song in _songs)
                {
                    if (_actualTitle == song)
                    {
                        MediaPlayer.Play(SongDictionary[_actualTitle]);
                        break;
                    }
                }
            }
        }

        public void PlayMusic(string newTitle, bool fading = true, float fadingTime = 500)
        {
            if (newTitle == _actualTitle) return;

            _elapsed = 0;

            _fading = fading;
            _fadingTime = fadingTime;

            _actualTitle = newTitle;
        }

        public void StopMusic(bool fading = true, float fadingTime = 500)
        {
            _actualTitle = "";
            _fading = fading;
            _fadingTime = fadingTime;
        }

        public void SetMusicVolume(float volume)
        {
            _musicVolume = volume;
            MediaPlayer.Volume = _musicVolume;
        }

        public void SetEffectsVolume(float volume)
        {
            _effectsVolume = volume;
        }

        public void PlayEffect(string soundEffect, float pich = 0, float pan = 0, float volume = 1)
        {
            foreach (string effect in _soundEffects)
            {
                if (effect == soundEffect)
                {
                    SoundEffectDictionary[soundEffect].Play(_effectsVolume * volume, pich, pan);
                    break;
                }
            }
        }

        public void PlayEffect3D(string soundEffect, Point from, Point to, float pich = 0, float volume = 1)
        {
            float distance = 0.008f * (float)Math.Sqrt((from.X - to.X) * (from.X - to.X) + (from.Y - to.Y) * (from.Y - to.Y));
            float balance = Math.Min(Math.Abs(to.X - from.X) * 0.005f, 1);
            float vol = Math.Min(_effectsVolume / (distance * distance), _effectsVolume) * volume;

            if (vol / volume <= _effectsVolume * 0.025) return;

            foreach (string effect in _soundEffects)
            {
                if (effect == soundEffect + "_mono")
                {
                    if (to.X > from.X)
                    {
                        SoundEffectDictionary[soundEffect + "_mono"].Play((1 + balance) * vol * 2, pich, 1);
                        SoundEffectDictionary[soundEffect + "_mono"].Play((1 - balance) * vol * 2, pich, -1);
                    }
                    else
                    {
                        SoundEffectDictionary[soundEffect + "_mono"].Play((1 + balance) * vol * 2, pich, -1);
                        SoundEffectDictionary[soundEffect + "_mono"].Play((1 - balance) * vol * 2, pich, 1);
                    }

                    break;
                }
            }
        }


        public float GetMusicVolume()
        {
            return _musicVolume;
        }

        public float GetEffectsVolume()
        {
            return _effectsVolume;
        }

        public void UnloadContent()
        {

        }
    }
}
