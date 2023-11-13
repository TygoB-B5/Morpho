namespace Morpho
{
    public abstract class PlayerController
    {
        protected PlayerController(Player player)
        {
            Parent = player;
        }

        public abstract void StartController();
        public abstract void UpdateController();

        protected Player Parent;
    }
}