using System;
using System.Collections.Generic;
using Buildings;
using Enums;
using Managers;
using UI;
using UnityEngine;
using UnityEngine.InputSystem;
using Weapons;

namespace Entities
{
    [RequireComponent(typeof(PlayerInput))]
    public class Player : Entity
    {
        [Header("Player")] [SerializeField] private float jumpForce;
        [SerializeField] private Weapon weapon;
        [SerializeField] private GameObject playerContent;
        [SerializeField] private Color color;

        [Header("Player Ground-Check")] [SerializeField]
        private Transform topLeft;

        [SerializeField] private Transform bottomRight;
        [SerializeField] private LayerMask groundLayer;

        // Building upgrade menu
        [Header("Build & Upgrade Menu")] [SerializeField]
        private LayerMask buildingLayer;

        [SerializeField] private float upgradeMenuToggleRange;
        [SerializeField] private float buildMenuToggleRange;

        [SerializeField] private UpgradeMenu upgradeMenu;
        [SerializeField] private BuildMenu buildMenu;

        [Header("Controls")] [SerializeField] private PlayerInput input;

        //InputSystem
        private Vector2 _movementInput;
        private Vector2 _aimDirection;

        private bool _onGround;

        public bool IsFacingLeft { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            spriteRenderer.color = color;
        }

        private void OnEnable() => GameManager.Instance.OnPause += TogglePause;
        private void OnDisable() => GameManager.Instance.OnPause -= TogglePause;

        private void FixedUpdate()
        {
            //Prevent player from moving outside
            if (Camera.main != null)
            {
                float leftBound = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).x;
                float rightBound = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x;
                Vector3 position = transform.position;
                position.x = Mathf.Clamp(position.x, leftBound, rightBound);
                transform.position = position;
            }

            //Ground Check
            _onGround = Physics2D.OverlapArea(topLeft.position, bottomRight.position, groundLayer);
            if (buildMenu.IsShowing)
            {
                Rb.velocity = new Vector2(0, Rb.velocity.y);
                return;
            }

            //Movement
            Rb.velocity = new Vector2(_movementInput.x * baseMovementSpeed, Rb.velocity.y);
            //Aiming
            float angle = Vector2.SignedAngle(Vector2.right, _aimDirection);
            weapon.transform.eulerAngles = new Vector3(0, 0, angle);
            FlipEntity(angle > 90 || angle <= -90);

            // Show upgrade menu if necessary
            Building nearestBuilding = GetNearestBuildingInRange();
            if (nearestBuilding == null)
                upgradeMenu.Hide();
            else
                upgradeMenu.ShowForBuilding(nearestBuilding);
        }

        protected override void FlipEntity(bool facingLeft)
        {
            base.FlipEntity(facingLeft);
            weapon.SpriteRenderer.flipY = facingLeft;
            IsFacingLeft = facingLeft;
        }

        protected override void Destroy()
        {
            TogglePlayer(false);
            CurrentHealth = maxHealth;
            Transform spawnPosition = GameManager.Instance.PlayerSpawnPosition;
            if(spawnPosition == null)
                return;
            transform.position = spawnPosition.position;
            UpdateHealthBar();
            Invoke(nameof(Respawn), 3);
        }

        //Input Messages
        private void OnMovePlayer(InputValue value) => _movementInput = new Vector2(value.Get<float>(), 0);
        private void OnMoveStick(InputValue value) => _movementInput = value.Get<Vector2>();

        private void OnWeaponAimMouse(InputValue value) =>
            _aimDirection = Camera.main.ScreenToWorldPoint(value.Get<Vector2>()) - transform.position;

        private void OnWeaponAimStick(InputValue value) => _aimDirection = value.Get<Vector2>();

        private void OnFire(InputValue value)
        {
            BuildMenu.TowerSelection selectedTower = buildMenu.SelectedTower;
            if (selectedTower != null && selectedTower.BlueprintInstance.IsBuildable)
            {
                if (ResourceManager.Instance.RemoveGold(selectedTower.TowerData.cost))
                {
                    selectedTower.BlueprintInstance.Build(selectedTower.TowerData.prefab);
                    buildMenu.DeselectTower();
                    return;
                }
            }

            if (buildMenu.IsShowing || !enabled || GameManager.Instance.isPaused) return;
            weapon.Attack();
        }

        private void OnJump(InputValue value)
        {
            if (buildMenu.IsShowing) return;
            Jump();
        }

        private void OnDeviceLost() => Destroy(this.gameObject);
        private void OnUpgrade(InputValue value) => upgradeMenu.ExecuteAction(UpgradeAction.Upgrade);

        private void OnRepair(InputValue value)
        {
            upgradeMenu.ExecuteAction(UpgradeAction.Repair);
        }

        private void OnSell(InputValue value)
        {
            if (buildMenu.IsShowing) return;
            if (buildMenu.SelectedTower != null)
            {
                buildMenu.DeselectTower();
                return;
            }

            upgradeMenu.ExecuteAction(UpgradeAction.Sell);
        }

        private void OnBuildMenu(InputValue value) => ToggleBuildMenu();

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
            enabled = show;
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
            foreach (Collider2D hit in results)
            {
                float distance = Vector2.Distance(transform.position, hit.transform.position);
                if (distance > minDistance) continue;
                result = hit;
                minDistance = distance;
            }

            return result.gameObject.GetComponent<Building>();
        }

        private void ToggleBuildMenu()
        {
            // If build menu already shown, hide it
            if (buildMenu.IsShowing)
            {
                buildMenu.Hide();
                return;
            }

            // If build menu not shown, check distance to base
            if (!(Vector2.Distance(GameManager.Instance.PlayerBase.transform.position, transform.position) <
                  buildMenuToggleRange)) return;

            buildMenu.Show();
        }

        private void TogglePause(bool paused) => buildMenu.Hide();
    }
}