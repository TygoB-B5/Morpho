using UnityEngine;

namespace Morpho
{
    public class SmallPlayerControlller : PlayerController
    {
        private float DefaultScale = 3.0f;
        private float DefaultSpeed = 7.5f;

        private float MovementDelta;
        private float MovementSmoothness = 7.5f;

        private float JumpForce = 15.0f;

        private float time;

        public SmallPlayerControlller(Player player)
            : base(player)
        {
        }

        public override void StartController()
        {
            Parent.transform.localScale = Vector3.one * DefaultScale;
            Parent.Sprite.color = Color.green;
        }

        public override void UpdateController()
        {
            MovementDelta = Mathf.Lerp(MovementDelta, GameManager.InputManager.GetMovementInput().x, Time.deltaTime * MovementSmoothness);

            BoxCollider2D col = Parent.GetComponent<BoxCollider2D>();
            bool raycastHitLeft = Physics2D.Raycast(col.bounds.min, Vector2.down, 0.1f, LayerMask.NameToLayer("Player"));
            bool raycastHitRight = Physics2D.Raycast(new Vector2(col.bounds.max.x, col.bounds.min.y), Vector2.down, 0.1f, LayerMask.NameToLayer("Player"));
            bool canJump = GameManager.InputManager.IsJumping() && (raycastHitLeft || raycastHitRight);

            if (canJump)
            {
                GameManager.GetRadio().PlayJump();
                Parent.RigidBody.velocity = Vector3.up * JumpForce;
                Debug.Log("Jump");
            }

            time += Time.deltaTime * Mathf.Abs(MovementDelta);

            if(time > 0.5f)
            {
                time = 0;
                GameManager.GetRadio().PlayWalk();
            }
        }

        public override void FixedUpdateController()
        {
            if (MovementDelta != 0)
            {
                Parent.transform.Translate(Vector3.right * Mathf.Abs(MovementDelta) * DefaultSpeed * Time.deltaTime);
                Parent.transform.rotation = Quaternion.Euler(0, MovementDelta > 0 ? 0 : 180, 0);
            }
        }
    }
}