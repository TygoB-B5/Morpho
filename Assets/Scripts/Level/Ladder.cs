using UnityEngine;

namespace Morpho
{
    public class Ladder : MonoBehaviour
    {
        public float Speed = 7;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            GameManager.Player.transform.position += Vector3.up * 0.05f;
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.gameObject == GameManager.Player.gameObject)
            {
                if(GameManager.Player.RigidBody.velocity.y < 0)
                {
                    GameManager.Player.RigidBody.velocity = new Vector2(GameManager.Player.RigidBody.velocity.x, 0);

                    GameManager.Player.transform.position += Vector3.up * GameManager.InputManager.GetMovementInput().y * Speed * Time.deltaTime;
                }

            }
        }
    }
}