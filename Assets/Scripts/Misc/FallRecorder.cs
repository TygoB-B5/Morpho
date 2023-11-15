using UnityEngine;

namespace Morpho
{
    public class FallRecorder : MonoBehaviour
    {
        private float maxVelocity;

        private void FixedUpdate()
        {
            float vel = GetComponent<Rigidbody2D>().velocity.y;
            if (vel < maxVelocity)
            {
                maxVelocity = vel;
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.GetContact(0).normal == new Vector2(0, 1))
            {
                if (maxVelocity < -10)
                {
                    GameManager.GetRadio().PlayThwomp();
                    GameManager.GetCameraMan().PlayCameraShake(0.0f, 0.1f, maxVelocity * 0.05f);
                    maxVelocity = 0;
                }
            }
        }
    }
}