using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;


namespace Abyss_Call
{
    public class TextureCapsule
    {
        public bool Preload = false;
        public string Tag = null;
        public string Path = null;
        public Texture2D Texture = null;
        public Texture2D HighlightTexture = null;
    }

    public class TextureManager
    {
        public ContentManager Content { set; get; } = null;
        public List<TextureCapsule> TextureCapsules { set; get; } = new List<TextureCapsule>();

        public void NewTexture(string tag, string path, bool preload = false)
        {
            TextureCapsules.Add(new TextureCapsule() { Tag = tag, Path = path, Preload = preload });
        }
        public void SetContent(ContentManager contentManager)
        {
            Content = contentManager;

            Texture2D black = new Texture2D(Game.Renderer.DeviceManager.GraphicsDevice, 1, 1);
            black.SetData(new Color[] { Color.Black });
            TextureCapsules.Add(new TextureCapsule() { Tag = "black", Texture = black });
        }

        public void LoadContent()
        {
            foreach (TextureCapsule tc in TextureCapsules)
                if (tc.Preload)
                    LoadTexture(tc);
        }

        public Texture2D GetTexture(string tag, bool highlight = false)
        {
            var tc = TextureCapsules.Find(tc => tc.Tag == tag);

            if (tc is null)
            {
                //Debug.WriteLine("Error : tag texture not corresponding to any existing TextureCapsule");
                return null;
            }

            if (!highlight)
                return tc.Texture;
            else
                return tc.HighlightTexture;
        }

        public void LoadTexture(TextureCapsule textureCapsule)
        {
            Texture2D texture;
            Texture2D highlightTexture;

            if (textureCapsule.Texture == null && textureCapsule.Path != null)
            {
                texture = Content.Load<Texture2D>(textureCapsule.Path);
            }
            else
                return;
            textureCapsule.Texture = texture;


            Color[] data = new Color[texture.Width * texture.Height];

            texture.GetData(data);

            for (int i = 0; i < data.Length; i++)
                if (data[i].A != 0)
                    data[i] = Color.White;

            highlightTexture = new Texture2D(texture.GraphicsDevice, texture.Width, texture.Height);
            highlightTexture.SetData(data);

            RenderTarget2D buffer = new RenderTarget2D(Game.Renderer.DeviceManager.GraphicsDevice, Game.Width, Game.Height);
            var rts = Game.Renderer.SpriteBatch.GraphicsDevice.GetRenderTargets();

            Game.Renderer.SpriteBatch.GraphicsDevice.SetRenderTarget(buffer);

            SpriteBatch spriteBatchBuffer = new SpriteBatch(Game.Renderer.SpriteBatch.GraphicsDevice);

            Game.Renderer.SpriteBatch.GraphicsDevice.Clear(Color.Transparent);


            spriteBatchBuffer.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, null);

            spriteBatchBuffer.Draw(highlightTexture, new Rectangle(1, 0, texture.Width, texture.Height), Color.White);
            spriteBatchBuffer.Draw(highlightTexture, new Rectangle(-1, 0, texture.Width, texture.Height), Color.White);
            spriteBatchBuffer.Draw(highlightTexture, new Rectangle(0, 1, texture.Width, texture.Height), Color.White);
            spriteBatchBuffer.Draw(highlightTexture, new Rectangle(0, -1, texture.Width, texture.Height), Color.White);
            

            spriteBatchBuffer.End();

            Game.Renderer.SpriteBatch.GraphicsDevice.SetRenderTargets(rts);

            textureCapsule.HighlightTexture = buffer;
        }

        public void LoadTexture(string tag)
        {
            var tc = TextureCapsules.Find(tc => tc.Tag == tag);
            if (!(tc is null))
                tc.Texture = Content.Load<Texture2D>(tc.Path);
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
