using UnityEngine;

namespace Morpho
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Box : MonoBehaviour
    {
        private void OnCollisionStay2D(Collision2D collision)
        {
            if (collision.gameObject == GameManager.Player.gameObject)
            {
                bool isTooWeak = GameManager.Player.Controller.GetType() == typeof(SmallPlayerControlller);

                GetComponent<Rigidbody2D>().isKinematic = isTooWeak;
            }
        }
    }
}