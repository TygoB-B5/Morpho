using System.Collections;
using UnityEngine;

namespace Morpho
{
    public class BoxButton : MonoBehaviour
    {
        public GameObject ToChange;
        private bool isDying;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.GetComponent<Box>() && !isDying)
            {
                isDying = true;
                StartCoroutine(KillObject());
            }
        }

        private IEnumerator KillObject()
        {
            yield return new WaitForSeconds(0.45f);
            Destroy(gameObject);
            GameManager.GetCameraMan().PlayCameraShake(0.1f, 0.25f, 0.05f);
            ToChange.SetActive(!ToChange.activeSelf);
        }
    }
}