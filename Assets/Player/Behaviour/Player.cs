using UnityEngine;

namespace Morpho
{
    public class Player : MonoBehaviour
    {
        public PlayerController Controller { get; private set; }

        [Header("Animation controls")]
        public Animator PlayerAnimator;
        public float IdleAnimationSpeed = 0.5f;
        public float MovementAnimationSpeed = 1.0f;


        public void Awake()
        {
            Controller = new DefaultPlayerController(this);
        }
        public void Start()
        {
            Controller.StartController();
        }

        public void Update()
        {
            Controller.UpdateController();
            PlayerAnimator.speed = 0.5f + Mathf.Abs(GameManager.InputManager.GetMovementInput().x) * MovementAnimationSpeed;
        }

        public void FixedUpdate()
        {
            Controller.FixedUpdateController();
        }
    }
}