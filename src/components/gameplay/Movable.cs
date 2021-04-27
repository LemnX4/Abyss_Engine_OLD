namespace Abyss_Call
{
    class Movable : Component
    {
        public float Speed { get; set; } = 1f;
        public bool Moving { get; set; } = false;
        public bool CanMove { get; set; } = true;
        public bool CanMoveLeft { get; set; } = true;
        public bool CanMoveRight { get; set; } = true;
        public bool CanMoveUp { get; set; } = true;
        public bool CanMoveDown { get; set; } = true;
    }
}
