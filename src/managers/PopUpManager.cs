using System.Collections.Generic;


namespace Abyss_Call
{
    public class PopUpManager
    {
        public List<Entity> PopUps = new List<Entity>();

        public Entity GetPopUp(string name)
        {
            return PopUps.Find(e => e.GetComponent<PopUp>().Name == name);
        }

        public void Switch(string name)
        {
            Entity p = PopUps.Find(e => e.GetComponent<PopUp>().Name == name);
            if (p.IsUpdatable)
            {
                p.IsUpdatable = false;
                p.IsRenderable = false;
            }
            else
            {
                p.IsUpdatable = true;
                p.IsRenderable = true;
            }
        }
        public void Open(string name)
        {
            Entity p = PopUps.Find(e => e.GetComponent<PopUp>().Name == name);
            p.IsUpdatable = true;
            p.IsRenderable = true;
        }

        public void Close(string name)
        {
            Entity p = PopUps.Find(e => e.GetComponent<PopUp>().Name == name);
            p.IsUpdatable = false;
            p.IsRenderable = false;
        }

        public void CloseAll()
        {
            foreach(Entity p in PopUps) 
            {
                p.IsUpdatable = false;
                p.IsRenderable = false;
            }
        }

        public void Update(double gameTime)
        {
            foreach (Entity p in PopUps)
            {
                PopUp pc = p.GetComponent<PopUp>();
                if (Game.ScenesManager.ActualScene == pc.CorrespondingScene && Game.KeyboardManager.IsKeyPressed(pc.Key))
                {
                    if (p.IsUpdatable)
                        Close(pc.Name);
                    else
                        Open(pc.Name);
                }
            }
        }
    }
}
