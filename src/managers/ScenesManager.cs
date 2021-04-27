using System.Collections.Generic;


namespace Abyss_Call
{
    public class ScenesManager
    {
        public Entity ActualScene = null;
        public Entity NextScene = null;

        public List<Entity> Scenes = new List<Entity>();
        public void SwitchToScene(string name)
        {
            NextScene = Scenes.Find(e => e.GetComponent<Scene>().Name == name);

            if (ActualScene.HasComponent<SceneFading>())
                ActualScene.GetComponent<SceneFading>().Style = FadingStyle.FadeOut;
            else
                ActualScene.AddComponent(new SceneFading() { Style = FadingStyle.FadeOut });

            ActualScene.IsUpdatable = false;
        }
    }
}
