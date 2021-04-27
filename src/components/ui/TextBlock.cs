using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Abyss_Call
{
    public enum Font
    {
        Human, Ancient
    }

    public class Letter : Component
    {
    }
    public class TextBlock : Component
    {
        public List<List<Entity>> Lines = new List<List<Entity>>();

        private int _cursor = 0;

        private string _content;
        public string Content
        {
            get
            {
                return _content;
            }
            set
            {
                _content = value;
                _cursor = 0;
                determineLetters();
            }
        }

        private Vector2 _area = new Vector2(1000, 1000);
        public Vector2 Area
        {
            get
            {
                return _area;
            }
            set
            {
                _area = value;
                _cursor = 0;
                determineLetters();
            }
        }
        public Point Size { get; private set; } = new Point(0, 0);

        private int _pixelSize = 3;
        public int PixelSize
        {
            get
            {
                return _pixelSize;
            }
            set
            {
                _pixelSize = value;
                _cursor = 0;
                determineLetters();
            }
        }

        private int _spacing = 1;
        public int Spacing
        {
            get
            {
                return _spacing;
            }
            set
            {
                _spacing = value;
                _cursor = 0;
                determineLetters();
            }
        }

        private bool _centered = true;
        public bool Centered
        {
            get
            {
                return _centered;
            }
            set
            {
                _centered = value;
                _cursor = 0;
                determineLetters();
            }
        }

        private bool _animated = false;
        public bool Animated
        {
            get
            {
                return _animated;
            }
            set
            {
                _animated = value;
                _cursor = 0;
                determineLetters();
            }
        }

        private bool _shadow = true;
        public bool Shadow
        {
            get
            {
                return _shadow;
            }
            set
            {
                _shadow = value;
                _cursor = 0;
                determineLetters();
            }
        }

        private Color _color = Color.White;
        public Color Color
        {
            get
            {
                return _color;
            }
            set
            {
                _color = value;
                foreach (Entity e in HostEntity.Entities)
                    if (e.HasComponent<Letter>())
                        e.GetComponent<Drawable>().Color = value;
            }
        }
        public bool AnimationSkipped { get; set; } = false;
        public double ProperTime { get; set; } = 0;
        public double TimePerLetter { get; set; } = 100;
        public TextBlock(Entity host)
        {
            HostEntity = host;
        }
        private void determineLetters()
        {
            ProperTime = 0;
            Lines.Clear();
            Lines.Add(new List<Entity>());

            foreach (Entity e in HostEntity.Entities)
                if (e.HasComponent<Letter>())
                {
                    foreach (Entity ee in e.Entities)
                        ee.RemoveFromSystems();
                    e.RemoveFromSystems();
                }

            HostEntity.Entities.RemoveAll(e => e.HasComponent<Letter>());

            Point currentPosition = new Point(0, 0);

            while(_cursor < Content.Length)
            {
                Point wordSize = new Point(0, 10 * PixelSize);

                int i = _cursor;

                while(i < Content.Length)
                {
                    var c = Content[i];
                    var sourceRect = getSource(c);

                    if (c != ' ' && c != '|')
                    {
                        wordSize = new Point(wordSize.X + (sourceRect.Width + Spacing) * PixelSize, wordSize.Y);
                    } else
                    {
                        break;
                    }

                    i++;
                }

                if (wordSize.X + currentPosition.X > Area.X)
                {
                    currentPosition = new Point(0, currentPosition.Y + 10 * PixelSize);
                    Lines.Add(new List<Entity>());
                }

                if (wordSize.Y + currentPosition.Y < Area.Y)
                {
                    for (int k = _cursor; k <= i && k < Content.Length; k++)
                    {
                        var c = Content[k];

                        if (c == '|')
                        {
                            currentPosition = new Point(0, currentPosition.Y + 10 * PixelSize);
                            Lines.Add(new List<Entity>());
                        }
                        else if (c == ' ')
                        {
                            currentPosition = new Point(currentPosition.X + 4 * PixelSize, currentPosition.Y);
                        }
                        else
                        { 
                            var sourceRect = getSource(c);

                            Entity letter = new Entity() { IsRenderable = !Animated};
                            letter.AddComponent(new Transform()
                            {
                                Position = currentPosition.ToVector2(),
                                XScale = (float)PixelSize / Game.SPS,
                                YScale = (float)PixelSize / Game.SPS
                            });
                            letter.AddComponent(new Drawable()
                            {
                                TexturePosition = sourceRect.Location,
                                TextureSize = sourceRect.Size,
                                TextureTag = "text",
                                LayerDepth = 0.01f
                            });
                            letter.AddComponent(new Letter());

                            if (Shadow)
                            {
                                Entity shadow = new Entity() { IsRenderable = !Animated };
                                shadow.AddComponent(new Transform()
                                {
                                    Position = currentPosition.ToVector2() + new Vector2(PixelSize, PixelSize),
                                    XScale = (float)PixelSize / Game.SPS,
                                    YScale = (float)PixelSize / Game.SPS
                                });
                                shadow.AddComponent(new Drawable()
                                {
                                    TexturePosition = sourceRect.Location,
                                    TextureSize = sourceRect.Size,
                                    TextureTag = "text",
                                    Color = Color.Black,
                                    Alpha = 0.75f,
                                });
                                shadow.AddComponent(new Letter());
                                letter.Entities.Add(shadow);
                            }

                            Lines[^1].Add(letter);
                            HostEntity.Entities.Add(letter);

                            currentPosition = new Point(currentPosition.X + (sourceRect.Width + Spacing) * PixelSize, currentPosition.Y);
                        }
                    }
                    _cursor = i+1;

                }
                else break;
            }

            if (Lines.Count > 0 && Lines[^1].Count == 0) Lines.RemoveAt(Lines.Count - 1);

            int maxSize = 0;

            for (int l = 0; l < Lines.Count; l++)
            {
                var line = Lines[l];
                for (int i = 0; i < line.Count; i++)
                {
                    maxSize = Math.Max(maxSize, (int)(line[i].GetComponent<Transform>().Position.X + line[i].GetComponent<Drawable>().TextureSize.X * PixelSize));
                }
            }

            Size = new Point(maxSize, Lines.Count * 10 * PixelSize);

            
            if (Centered)
            {
                for (int i = 0; i < Lines.Count; i++)
                {
                    var line = Lines[i];
                    if (line.Count == 0)
                        continue;

                    int width = (int)(line[^1].GetComponent<Transform>().Position.X + line[^1].GetComponent<Drawable>().TextureSize.X * PixelSize);

                    for (int k = 0; k < line.Count; k++)
                    {
                        Vector2 p = line[k].GetComponent<Transform>().Position;
                        line[k].GetComponent<Transform>().Position = new Vector2(p.X + (Size.X - width) / 2.0f, p.Y);
                    }
                }
            }
        }

        private Rectangle getSource(char character, Font font = Font.Human)
        {
            if (font == Font.Human)
            {
                string maj = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                string min = "abcdefghijklmnopqrstuvwxyz";
                string num = "0123456789";
                string special = ".!?:&',%()[]/\\*;=-+";
                string decorativeMin = "àâéèêëîïöôòç";
                string decorativeMaj = "É";
                int x = 0, y = 0;
                int width = 5, height = 12;

                if (character == '.' || character == '!' || character == ':' || character == '\'' || character == ';') width = 1;
                else if (character == 'l' || character == 't' || character == ',') width = 2;
                else if (character == '(' || character == ')' || character == '[' || character == ']') width = 2;
                else if (character == 'i' || character == 'î' || character == 'ï' || character == '1') width = 3;
                else if (character == 'k' || character == 'f' || character == '-') width = 4;

                if (maj.Contains(character))
                {
                    x = 6 * maj.IndexOf(character);
                    y = 0;
                }
                else if (min.Contains(character))
                {
                    x = 6 * min.IndexOf(character);
                    y = height;
                }
                else if (num.Contains(character))
                {
                    x = 6 * num.IndexOf(character);
                    y = height * 2;
                }
                else if (special.Contains(character))
                {
                    x = 6 * special.IndexOf(character);
                    y = height * 3;
                }

                else if (decorativeMin.Contains(character))
                {
                    x = 6 * decorativeMin.IndexOf(character);
                    y = height * 4;
                }
                else if (decorativeMaj.Contains(character))
                {
                    x = 6 * decorativeMaj.IndexOf(character);
                    y = height * 5;
                }

                if (character == '[' || character == '(') x += 3;

                return new Rectangle(x, y, width, height);
            }
            return new Rectangle(0, 0, 1, 1);
        }


        public void SkipToNextGroup()
        {
            determineLetters();
            AnimationSkipped = false;
        }

        public void SkipAnimation()
        {
            ProperTime = 10e10;
            AnimationSkipped = true;
        }

        public void ResetAnimation()
        {
            AnimationSkipped = false;
        }

        public bool PartiallyEnded()
        {
            if (AnimationSkipped) return true;

            for (int l = 0; l < Lines.Count; l++)
            {
                var line = Lines[l];
                for (int i = 0; i < line.Count; i++)
                {
                    if (line[i].IsRenderable == false) return false;
                }
            }
            return true;
        }
        public bool TotallyEnded()
        {
            return _cursor >= Content.Length;
        }
    }
}