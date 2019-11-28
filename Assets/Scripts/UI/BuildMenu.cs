using Buildings;
using Entities;
using Managers;
using ScriptableObjects.Towers;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class BuildMenu : MonoBehaviour
    {
        [SerializeField] private Transform buttonPanel;
        [SerializeField] private LayerMask blockedLayers;
        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private TowerData[] towerPrefabs;
        [SerializeField] private TowerButton buttonPrefab;
        [SerializeField] private Player player;
        [SerializeField] private Canvas canvas;

        public TowerSelection SelectedTower { get; private set; }
        public bool IsActive => gameObject.activeInHierarchy;

        private void Awake()
        {
            // Set camera reference
            canvas.worldCamera = Camera.main;
            // Add all towers to build menu
            foreach (TowerData tower in towerPrefabs)
            {
                TowerButton button = Instantiate(buttonPrefab, buttonPanel, false);
                button.TowerIcon.sprite = tower.buildMenuIcon;
                button.NameLabel.text = tower.name;
                button.PriceLabel.text = tower.cost.ToString();
                button.Button.onClick.AddListener(() => SelectTower(tower));
            }
        }

        private void SelectTower(TowerData tower)
        {
            if (!ResourceManager.Instance.HasGold(tower.cost)) return;
            Blueprint blueprint = Instantiate(tower.blueprintPrefab, player.transform);
            blueprint.Setup(player, blockedLayers, groundLayer);
            SelectedTower = new TowerSelection(tower, blueprint.GetComponent<Blueprint>());
            Hide();
        }

        public void DeselectTower()
        {
            Destroy(SelectedTower.BlueprintInstance.gameObject);
            SelectedTower = null;
        }

        public void Show()
        {
            if (SelectedTower != null)
                DeselectTower();
            gameObject.SetActive(true);
            EventSystem.current.SetSelectedGameObject(buttonPanel.GetChild(0).gameObject);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
            EventSystem.current.SetSelectedGameObject(null);
        }

        public class TowerSelection
        {
            public TowerData TowerData { get; }
            public Blueprint BlueprintInstance { get; }

            public TowerSelection(TowerData towerData, Blueprint blueprintInstance)
            {
                TowerData = towerData;
                BlueprintInstance = blueprintInstance;
            }
        }
    }
}