using UnityEngine;

namespace Morpho
{
    public class PlayerStart : MonoBehaviour
    {
        private void Start()
        {
            GameManager.Player.transform.position = transform.position;

            GameManager.Player.SetController(new DefaultPlayerController(GameManager.Player));
        }
    }
}
