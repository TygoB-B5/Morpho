using UnityEngine;

namespace Morpho
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class PlayerExit : MonoBehaviour
    {
        private void Awake()
        {
            GetComponent<BoxCollider2D>().isTrigger = true;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            GameManager.ProgressToNextLevel();
        }
    }
}