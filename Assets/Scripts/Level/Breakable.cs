using UnityEngine;

namespace Morpho
{
    public class Breakable : MonoBehaviour
    {
        private void OnCollisionStay2D(Collision2D collision)
        {
            if (collision.gameObject == GameManager.Player.gameObject)
            {
                bool isTooWeak = GameManager.Player.Controller.GetType() == typeof(SmallPlayerControlller);

                if (!isTooWeak)
                {
                    GameManager.GetRadio().PlayThwomp();
                    Destroy(gameObject);
                }
            }
        }
    }
}