using System.Diagnostics;


namespace Abyss_Call
{
    class SceneFader : System
    {
        public override bool Requirements(Entity e) => e.HasComponent<Scene>() && e.HasComponent<SceneFading>();
        protected override void UpdateEntity(Entity entity, double deltaTime)
        {
            SceneFading f = entity.GetComponent<SceneFading>();

            f.ProperTime += deltaTime;
            
            if (f.LifePercent == 1)
            {
                entity.RemoveComponent<SceneFading>();

                if (!(Game.ScenesManager.NextScene is null))
                {
                    Game.ScenesManager.NextScene.AddComponent(new SceneFading());
                    Game.ScenesManager.ActualScene.IsRenderable = false;
                    Game.ScenesManager.ActualScene = Game.ScenesManager.NextScene;
                    Game.ScenesManager.NextScene = null;

                    Game.ScenesManager.ActualScene.IsUpdatable = true;
                    Game.ScenesManager.ActualScene.IsRenderable = true;
                    Game.PopUpManager.CloseAll();
                }
            }

            if (f.Style == FadingStyle.FadeOut)
                Game.BlackScreen.GetComponent<Drawable>().Alpha = (float)f.LifePercent;
            if (f.Style == FadingStyle.FadeIn)
                Game.BlackScreen.GetComponent<Drawable>().Alpha = 1-(float)f.LifePercent;
        }
    }
}
