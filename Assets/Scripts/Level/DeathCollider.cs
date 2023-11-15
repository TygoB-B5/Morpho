using UnityEngine;

namespace Morpho
{
    public class DeathCollider : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject == GameManager.Player.gameObject)
            {
                ParralelTools.RunAfterSeconds(0.2f, () => GameManager.ResetLevel());
                GameManager.GetCameraMan().PlayCameraShake(0.75f, 0.5f);
                GameManager.GetRadio().PlayThwomp();
            }
        }
    }
}