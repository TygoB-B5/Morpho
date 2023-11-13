using UnityEngine;

namespace Morpho
{
    public class Player : MonoBehaviour
    {
        public void Start()
        {
            Controller.StartController();
        }

        public void Update()
        {
            Controller.UpdateController();
        }

        public PlayerController Controller { get; private set; }
    }
}