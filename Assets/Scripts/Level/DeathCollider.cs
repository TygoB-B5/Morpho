using UnityEngine;

namespace Morpho
{
    public class DeathCollider : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            ParralelTools.RunAfterSeconds(0.2f, () => GameManager.ResetLevel());
        }
    }
}