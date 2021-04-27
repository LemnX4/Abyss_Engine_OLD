using System.Collections.Generic;


namespace Abyss_Call
{
    public class MapManager
    {
        public Entity CurrentMap { get; set; }
        public List<Entity> Maps { get; set; } = new List<Entity>();

        public MapManager()
        {
            
        }

        public void Update(double gameTime)
        {
            if (Game.ScenesManager.ActualScene.GetType() != typeof(InGame))
            {
                CurrentMap.IsUpdatable = false;
                CurrentMap.IsRenderable = false;
            } 
            else
            {
                CurrentMap.IsUpdatable = true;
                CurrentMap.IsRenderable = true;
            }
        }
    }
}
