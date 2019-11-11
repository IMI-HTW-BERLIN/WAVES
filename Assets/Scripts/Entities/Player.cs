using Controls;
using UnityEngine;
using Weapons;

namespace Entities
{
    public class Player : Entity
    {
        [SerializeField] private float jumpForce;
        [SerializeField] private Transform respawnPosition;

        //InputSystem
        private PlayerControls _controls;
        private Vector2 _movementInput;
        private Vector2 _aimDirection;

        //Ground-Check
        [SerializeField] private Transform topLeft;
        [SerializeField] private Transform bottomRight;
        [SerializeField] private LayerMask groundLayer;
        private bool _onGround;

        [SerializeField] private Weapon weapon;
        [SerializeField] private SpriteRenderer spriteRenderer;

        protected override void Awake()
        {
            base.Awake();

            _controls = new PlayerControls();
            //Gamepad
            _controls.Game.MoveStick.performed += value => _movementInput = value.ReadValue<Vector2>();
            _controls.Game.MoveStick.canceled += value => _movementInput = Vector2.zero;
            _controls.Game.Jump.performed += value => Jump();
            _controls.Game.WeaponAimStick.performed += value => _aimDirection = value.ReadValue<Vector2>();
            _controls.Game.Fire.performed += value => weapon.Attack();

            //Keyboard
            _controls.Game.Move.performed += value => _movementInput = new Vector2(value.ReadValue<float>(), 0);
            _controls.Game.WeaponAimMouse.performed += value =>
                _aimDirection = Camera.main.ScreenToWorldPoint(value.ReadValue<Vector2>()) - transform.position;
        }

        private void OnEnable() => _controls.Enable();
        private void OnDisable() => _controls.Disable();

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
            weapon.spriteRenderer.flipY = facingLeft;
        }

        private void Jump()
        {
            if (_onGround)
                Rb.velocity = new Vector2(Rb.velocity.x, jumpForce);
        }

        protected override void OnDeath()
        {
            gameObject.SetActive(false);
            CurrentHealth = maxHealth;
            transform.position = respawnPosition.position;
            UpdateHealthBar();
            Invoke(nameof(Respawn), 3);
        }

        private void Respawn() => gameObject.SetActive(true);
    }
}