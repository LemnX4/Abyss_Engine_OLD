namespace Abyss_Call
{
    public class PopUpOpener : System
    {
        public override bool Requirements(Entity e) => e.HasComponent<Mouseover>() && e.HasComponent<PopUpOpen>();

        protected override void UpdateEntity(Entity entity, double deltaTime)
        {
            Mouseover mo = entity.GetComponent<Mouseover>();

            if (mo.Hovered && Game.MouseManager.IsButtonPressed(MouseButton.Left))
            {
                Game.PopUpManager.Open(entity.GetComponent<PopUpOpen>().ToOpen);
                mo.Hovered = false;

                Game.AudioManager.PlayEffect("mouseover", 0.5f);
            }
        }
    }

    public class PopUpCloser : System
    {
        public override bool Requirements(Entity e) => e.HasComponent<Mouseover>() && e.HasComponent<PopUpClose>();

        protected override void UpdateEntity(Entity entity, double deltaTime)
        {
            Mouseover mo = entity.GetComponent<Mouseover>();

            if (mo.Hovered && Game.MouseManager.IsButtonPressed(MouseButton.Left))
            {
                Game.PopUpManager.Close(entity.GetComponent<PopUpClose>().ToClose);
                mo.Hovered = false;

                Game.AudioManager.PlayEffect("mouseover", 0.5f);
            }
        }
    }

    public class PopUpSwitcher : System
    {
        public override bool Requirements(Entity e) => e.HasComponent<Mouseover>() && e.HasComponent<PopUpSwitch>();

        protected override void UpdateEntity(Entity entity, double deltaTime)
        {
            Mouseover mo = entity.GetComponent<Mouseover>();
            PopUpSwitch ps = entity.GetComponent<PopUpSwitch>();

            if (mo.Hovered && Game.MouseManager.IsButtonPressed(MouseButton.Left))
            {
                Game.PopUpManager.Switch(ps.ToSwitch);
                mo.Hovered = false;

                Game.AudioManager.PlayEffect("mouseover", 0.5f);
            }

            if (Game.PopUpManager.GetPopUp(ps.ToSwitch).IsUpdatable)
                mo.Hovered = true;
        }
    }
}
