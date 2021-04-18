using System.Collections.Generic;
using System.Diagnostics;


namespace Abyss_Call
{
    public class ScenesManager
    {
        public Entity ActualScene = null;

        public List<Entity> Scenes = new List<Entity>();
        public void SwitchToScene(string name)
        {
            if (ActualScene.HasComponent<Fading>())
                ActualScene.GetComponent<Fading>().Style = FadingStyle.FadeOut;
            else
                ActualScene.AddComponent(new Fading() { Style = FadingStyle.FadeOut});

            ActualScene.IsUpdatable = false;
            
            Entity next = Scenes.Find(e => e.GetComponent<Scene>().Name == name);

            if (next.HasComponent<Fading>())
                next.GetComponent<Fading>().Style = FadingStyle.FadeIn;
            else
                next.AddComponent(new Fading() { Style = FadingStyle.FadeIn });

            next.IsUpdatable = true;
            next.IsRenderable = true;

            ActualScene = next;
        }
    }
}
