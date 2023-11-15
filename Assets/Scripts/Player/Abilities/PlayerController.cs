namespace Morpho
{
    public abstract class PlayerController
    {
        protected Player Parent;

        protected PlayerController(Player player)
        {
            Parent = player;
        }

        public abstract void StartController();
        public abstract void UpdateController();
        public abstract void FixedUpdateController();
    }
}