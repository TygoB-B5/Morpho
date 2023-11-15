using UnityEngine;

namespace Morpho
{
    public class Player : MonoBehaviour
    {
        public PlayerController Controller { get; private set; }
        public Rigidbody2D RigidBody { get; private set; }
        public SpriteRenderer Sprite { get; private set; }

        private bool controllerEnabled;

        [Header("Animation controls")]
        public Animator PlayerAnimator;
        public float IdleAnimationSpeed = 0.5f;
        public float MovementAnimationSpeed = 1.0f;

        public void EnableController(bool enabled)
        {
            controllerEnabled = enabled;
        }

        public void SetController(PlayerController controller)
        {
            Controller = controller;
            Controller.StartController();
        }

        public void Awake()
        {
            Controller = new DefaultPlayerController(this);
            RigidBody = GetComponent<Rigidbody2D>();
            Sprite = GetComponent<SpriteRenderer>();
            controllerEnabled = true;
        }
        public void Start()
        {
            Controller.StartController();
        }

        public void Update()
        {
            if (controllerEnabled)
            {
                Controller.UpdateController();
                PlayerAnimator.speed = 0.5f + Mathf.Abs(GameManager.InputManager.GetMovementInput().x) * MovementAnimationSpeed;
            }
        }

        public void FixedUpdate()
        {
            if (controllerEnabled)
            {
                Controller.FixedUpdateController();
            }
        }
    }
}