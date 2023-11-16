using Unity.VisualScripting;
using UnityEngine;

namespace Morpho
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Box : MonoBehaviour
    {
        private float time;
        private bool isFalling;
        private float highestYvel;

        private void OnCollisionStay2D(Collision2D collision)
        {
            if (collision.gameObject == GameManager.Player.gameObject)
            {
                bool isTooWeak = GameManager.Player.Controller.GetType() == typeof(SmallPlayerControlller) ||
                    GameManager.Player.Controller.GetType() == typeof(HeavyPlayerController);

                GetComponent<Rigidbody2D>().mass = isTooWeak ? 9999999999 : 1;

                if(!isTooWeak)
                {
                    time += Time.deltaTime;
                    if(time > 0.15f)
                    {
                        time = 0;
                        GameManager.GetRadio().PlayPush();
                    }
                }
            }
        }

        private void FixedUpdate()
        {
            float currentVelY = GetComponent<Rigidbody2D>().velocity.y;

            if(currentVelY < -12.0f)
            {
                isFalling = true;

                if(currentVelY < highestYvel)
                {
                    highestYvel = currentVelY;
                }
            }

            if(currentVelY == 0 && isFalling)
            {
                isFalling = false;
                GameManager.GetRadio().PlayThwomp();
                GameManager.GetCameraMan().PlayCameraShake(highestYvel * 0.01f, highestYvel * 0.01f);
                GameManager.Player.RigidBody.velocity += Vector2.up * Mathf.Abs(highestYvel * 0.5f);
                highestYvel = 0;
            }
        }
    }
}