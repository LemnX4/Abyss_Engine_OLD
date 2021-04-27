namespace Abyss_Call
{
    public class SceneSwitcher : System
    {
        public override bool Requirements(Entity e) => e.HasComponent<Mouseover>() && e.HasComponent<SceneSwitch>();

        protected override void UpdateEntity(Entity entity, double deltaTime)
        {
            Mouseover mo = entity.GetComponent<Mouseover>();

            if (mo.Hovered && Game.MouseManager.IsButtonPressed(MouseButton.Left))
            {
                Game.Quit = entity.GetComponent<SceneSwitch>().NextScene == "Quit";
                
                Game.ScenesManager.SwitchToScene(entity.GetComponent<SceneSwitch>().NextScene);
                mo.Hovered = false;

                Game.AudioManager.PlayEffect("mouseover", 0.5f);
            }
        }
    }
}
