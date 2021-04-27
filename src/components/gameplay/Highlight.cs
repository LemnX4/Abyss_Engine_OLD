namespace Abyss_Call
{
    public class Highlight : Component
    {
        public Entity EntityToHighlight { get; set; }
        public double ProperTime { get; set; } = 0;
        public double Period { get; set; } = 1500;
        public Highlight(Entity entityToHighlight)
        {
            EntityToHighlight = entityToHighlight;
        }
    }
}
