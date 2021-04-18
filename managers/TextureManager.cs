using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Diagnostics;

namespace Abyss_Call
{
    public class TextureManager
    {
        public class TextureCapsule
        {
            public string Tag = null;
            public string Path = null;
            public Texture2D Texture = null;
        }
        public ContentManager Content { set; get; } = null;
        public List<TextureCapsule> TextureCapsules { set; get; } = new List<TextureCapsule>();
        public TextureManager()
        {
            TextureCapsules.Add(new TextureCapsule() { Tag = "text", Path = "font" });
            TextureCapsules.Add(new TextureCapsule() { Tag = "player", Path = "player" });
            TextureCapsules.Add(new TextureCapsule() { Tag = "bar", Path = "bar" });
        }
        public void SetContent(ContentManager contentManager)
        {
            Content = contentManager;
            foreach (TextureCapsule tc in TextureCapsules)
            {
                if (tc.Tag == "text")
                    LoadTexture(tc);
            }
        }

        public Texture2D GetTexture(string tag)
        {
            var tc = TextureCapsules.Find(tc => tc.Tag == tag);

            if (tc == null)
            {
                //Debug.WriteLine("Error : tag texture not corresponding to any existing TextureCapsule");
                return null;
            }

            return tc.Texture;
        }

        public void LoadTexture(TextureCapsule textureCapsule)
        {
            if (textureCapsule.Texture == null && textureCapsule.Path != null)
                textureCapsule.Texture = Content.Load<Texture2D>(textureCapsule.Path);
        }

        public void UnloadTexture(TextureCapsule textureCapsule)
        {
            if (textureCapsule.Texture != null)
                textureCapsule.Texture.Dispose();

            textureCapsule.Texture = null;
        }

        public void LoadEntity(Entity entity)
        {
            foreach (Entity e in entity.Entities)
                LoadEntity(e);

            if (entity.HasComponent<Drawable>())
            {
                var tc = TextureCapsules.Find(tc => tc.Tag == entity.GetComponent<Drawable>().TextureTag);

                if (tc != null)
                    LoadTexture(tc);
            }
        }
    }
}
