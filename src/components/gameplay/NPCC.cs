namespace Abyss_Call
{
    public class NPCC : Component
    {
        public Entity LookAt { get; set; }
        public int NativeDirection { get; set; } = 0;
        public bool CanTalk { get; set; } = false;
    }
}
