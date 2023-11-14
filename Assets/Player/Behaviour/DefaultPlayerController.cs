using UnityEngine;

namespace Morpho
{
    public class DefaultPlayerController : PlayerController
    {
        private float DefaultScale = 7.5f;
        private float DefaultSpeed = 15.0f;

        private float MovementDelta;
        private float MovementSmoothness = 10.0f;

        private float JumpForce = 32.0f;

        public DefaultPlayerController(Player player)
            : base(player)
        {
            Parent.transform.localScale = Vector3.one * DefaultScale;
        }

        public override void StartController()
        {

        }

        public override void UpdateController()
        {
            Debug.Log(GameManager.InputManager.GetMovementInput().x);
            Debug.Log(GameManager.InputManager.GetType());
            MovementDelta = Mathf.Lerp(MovementDelta, GameManager.InputManager.GetMovementInput().x, Time.deltaTime * MovementSmoothness);


            BoxCollider2D col = Parent.GetComponent<BoxCollider2D>();
            bool raycastHitLeft = Physics2D.Raycast(col.bounds.min, Vector2.down, 0.1f, LayerMask.NameToLayer("Player"));
            bool raycastHitRight = Physics2D.Raycast(new Vector2(col.bounds.max.x, col.bounds.min.y), Vector2.down, 0.1f, LayerMask.NameToLayer("Player"));
            bool canJump = GameManager.InputManager.IsJumping() && (raycastHitLeft || raycastHitRight);

            if (canJump) Parent.GetComponent<Rigidbody2D>().velocity = Vector3.up * JumpForce;
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