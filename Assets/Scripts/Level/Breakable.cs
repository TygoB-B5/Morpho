using System.Collections;
using UnityEngine;

namespace Morpho
{
    public class Breakable : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject == GameManager.Player.gameObject)
            {
                bool isTooWeak = GameManager.Player.Controller.GetType() == typeof(SmallPlayerControlller) ||
                    GameManager.Player.Controller.GetType() == typeof(DefaultPlayerController);


                if (!isTooWeak)
                {
                    StartCoroutine(DestroyBreakable());
                }
            }
        }

        private IEnumerator DestroyBreakable()
        {
            yield return new WaitForSeconds(0.25f);
            GameManager.GetCameraMan().PlayCameraShake(0.25f, 0.1f, 0.35f);
            GameManager.GetRadio().PlayThwomp();
            Destroy(gameObject);
        }
    }
}