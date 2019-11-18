using System.Collections.Generic;
using System.Linq;
using Buildings;
using Controls;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Entities
{
    public class Player : Entity
    {
        [SerializeField] private float jumpForce;
        [SerializeField] private Transform respawnPosition;
        
        // Building upgrade menu
        [SerializeField] private LayerMask buildingLayer;
        [SerializeField] private float upgradeMenuToggleRange;

        //InputSystem
        private PlayerControls _controls;
        private Vector2 _movementInput;
        private Vector2 _aimDirection;

        //Ground-Check
        [SerializeField] private Transform topLeft;
        [SerializeField] private Transform bottomRight;
        [SerializeField] private LayerMask groundLayer;
        private bool _onGround;

        [SerializeField] private Blaster blaster;
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
            _controls.Game.Fire.performed += value => blaster.Fire();

            //Keyboard
            _controls.Game.Move.performed += value => _movementInput = new Vector2(value.ReadValue<float>(), 0);
            _controls.Game.WeaponAimMouse.performed += value =>
                _aimDirection = Camera.main.ScreenToWorldPoint(value.ReadValue<Vector2>()) - transform.position;
        }

        public void Update()
        {
            List<Collider2D> results = new List<Collider2D>();
            ContactFilter2D filter = new ContactFilter2D
            {
                layerMask = buildingLayer,
                useLayerMask = true
            };
            // Check if building in range
            if (Physics2D.OverlapCircle(transform.position, upgradeMenuToggleRange, filter, results) == 0)
            {
                GameManager.Instance.HideUpgradeMenu();
                return;
            }
            
            // Building in range, get nearest building
            float minDistance = float.PositiveInfinity;
            Collider2D result = null;
            foreach (var hit in results.Where(hit => Vector2.Distance(transform.position, hit.transform.position) < minDistance))
                result = hit;

            Building selectedBuilding = result.gameObject.GetComponent<Building>();
            if (selectedBuilding == null)
            {
                Debug.LogError($"\"{result.transform.name}\" is missing a Building script");
                return;
            }

            GameManager.Instance.ShowUpgradeMenu(selectedBuilding);
        }

        private void OnUpgrade(InputValue value) => GameManager.Instance.ExecuteUpgradeAction(UpgradeAction.Upgrade);
        private void OnRepair(InputValue value) => GameManager.Instance.ExecuteUpgradeAction(UpgradeAction.Repair);
        private void OnSell(InputValue value) => GameManager.Instance.ExecuteUpgradeAction(UpgradeAction.Sell);

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
            blaster.transform.eulerAngles = new Vector3(0, 0, angle);
            bool facingLeft = angle > 90 || angle <= -90;
            spriteRenderer.flipX = facingLeft;
            blaster.SpriteRenderer.flipY = facingLeft;
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