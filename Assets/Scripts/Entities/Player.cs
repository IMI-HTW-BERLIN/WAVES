using System.Collections.Generic;
using System.Linq;
using Buildings;
using Enums;
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
        [SerializeField] private GameObject playerContent;
        [SerializeField] private Color[] colors;

        [Header("Player Ground-Check")] [SerializeField]
        private Transform topLeft;

        [SerializeField] private Transform bottomRight;
        [SerializeField] private LayerMask groundLayer;

        // Building upgrade menu
        [SerializeField] private LayerMask buildingLayer;
        [SerializeField] private float upgradeMenuToggleRange;

        //InputSystem
        private Vector2 _movementInput;
        private Vector2 _aimDirection;

        private bool _onGround;

        protected override void Awake()
        {
            base.Awake();
            spriteRenderer.color = colors[Random.Range(0, colors.Length - 1)];
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
            FlipEntity(angle > 90 || angle <= -90);


            // Show upgrade menu if necessary
            Building nearestBuilding = GetNearestBuildingInRange();
            if (nearestBuilding == null)
                GameManager.Instance.HideUpgradeMenu();
            else
                GameManager.Instance.ShowUpgradeMenu(nearestBuilding);
        }

        protected override void FlipEntity(bool facingLeft)
        {
            base.FlipEntity(facingLeft);
            weapon.SpriteRenderer.flipY = facingLeft;
        }

        protected override void OnDeath()
        {
            TogglePlayer(false);
            CurrentHealth = maxHealth;
            transform.position = GameManager.Instance.PlayerSpawnPosition.position;
            UpdateHealthBar();
            Invoke(nameof(Respawn), 3);
        }

        //Input Messages
        private void OnMove(InputValue value) => _movementInput = new Vector2(value.Get<float>(), 0);
        private void OnMoveStick(InputValue value) => _movementInput = value.Get<Vector2>();

        private void OnWeaponAimMouse(InputValue value) =>
            _aimDirection = Camera.main.ScreenToWorldPoint(value.Get<Vector2>()) - transform.position;

        private void OnWeaponAimStick(InputValue value) => _aimDirection = value.Get<Vector2>();
        private void OnFire(InputValue value) => weapon.Attack();
        private void OnJump(InputValue value) => Jump();
        private void OnDeviceLost() => Destroy(this.gameObject);
        private void OnUpgrade(InputValue value) => GameManager.Instance.ExecuteUpgradeAction(UpgradeAction.Upgrade);
        private void OnRepair(InputValue value) => GameManager.Instance.ExecuteUpgradeAction(UpgradeAction.Repair);
        private void OnSell(InputValue value) => GameManager.Instance.ExecuteUpgradeAction(UpgradeAction.Sell);
        private void OnPause(InputValue value) => GameManager.Instance.TogglePause();

        private void Jump()
        {
            if (_onGround)
                Rb.velocity = new Vector2(Rb.velocity.x, jumpForce);
        }

        private void Respawn() => TogglePlayer(true);

        private void TogglePlayer(bool show)
        {
            Rb.simulated = show;
            this.enabled = show;
            spriteRenderer.enabled = show;
            playerContent.SetActive(show);
        }

        private Building GetNearestBuildingInRange()
        {
            List<Collider2D> results = new List<Collider2D>();
            ContactFilter2D filter = new ContactFilter2D
            {
                layerMask = buildingLayer,
                useLayerMask = true
            };
            // Check if building in range
            if (Physics2D.OverlapCircle(transform.position, upgradeMenuToggleRange, filter, results) == 0)
                return null;

            // Building in range, get nearest building
            float minDistance = float.PositiveInfinity;
            Collider2D result = null;
            foreach (Collider2D hit in results.Where(hit =>
                Vector2.Distance(transform.position, hit.transform.position) < minDistance))
                result = hit;

            return result.gameObject.GetComponent<Building>();
        }
    }
}