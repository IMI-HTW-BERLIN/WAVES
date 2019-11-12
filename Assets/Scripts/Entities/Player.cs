using Managers;
using UnityEngine;
using UnityEngine.InputSystem;
using Weapons;

namespace Entities
{
    public class Player : Entity
    {
        [Header("Player")] [SerializeField] private float jumpForce;
        [SerializeField] private Weapon weapon;
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private GameObject playerContent;

        [Header("Player Ground-Check")] [SerializeField]
        private Transform topLeft;

        [SerializeField] private Transform bottomRight;
        [SerializeField] private LayerMask groundLayer;

        //InputSystem
        private Vector2 _movementInput;
        private Vector2 _aimDirection;

        private bool _onGround;

        protected override void Awake()
        {
            base.Awake();
            spriteRenderer.color = Random.ColorHSV();
        }

        private void FixedUpdate()
        {
            //Ground Check
            _onGround = Physics2D.OverlapArea(topLeft.position, bottomRight.position, groundLayer);
            //Movement
            Rb.velocity = new Vector2(_movementInput.x * baseMovementSpeed, Rb.velocity.y);
            //Aiming
            float angle = Vector2.SignedAngle(Vector2.right, _aimDirection);
            weapon.transform.eulerAngles = new Vector3(0, 0, angle);
            bool facingLeft = angle > 90 || angle <= -90;
            spriteRenderer.flipX = facingLeft;
            weapon.SpriteRenderer.flipY = facingLeft;
        }

        //Input Messages
        public void OnMove(InputValue value) => _movementInput = new Vector2(value.Get<float>(), 0);
        public void OnMoveStick(InputValue value) => _movementInput = value.Get<Vector2>();

        public void OnWeaponAimMouse(InputValue value) =>
            _aimDirection = Camera.main.ScreenToWorldPoint(value.Get<Vector2>()) - transform.position;

        public void OnWeaponAimStick(InputValue value) => _aimDirection = value.Get<Vector2>();
        public void OnFire(InputValue value) => weapon.Attack();
        public void OnJump(InputValue value) => Jump();
        public void OnDeviceLost() => Destroy(this.gameObject);

        public void TogglePlayer(bool show)
        {
            Rb.simulated = show;
            this.enabled = show;
            spriteRenderer.enabled = show;
            playerContent.SetActive(show);
        }

        private void Jump()
        {
            if (_onGround)
                Rb.velocity = new Vector2(Rb.velocity.x, jumpForce);
        }

        protected override void OnDeath()
        {
            TogglePlayer(false);
            CurrentHealth = maxHealth;
            transform.position = GameManager.Instance.PlayerSpawnPosition.position;
            UpdateHealthBar();
            Invoke(nameof(Respawn), 3);
        }

        private void Respawn() => TogglePlayer(true);
    }
}