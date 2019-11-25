using Buildings;
using Managers;
using ScriptableObjects.Towers;
using UnityEngine;

namespace UI
{
    public class BuildMenu : MonoBehaviour
    {
        [SerializeField] private GameObject buttonPanel;
        [SerializeField] private LayerMask blockedLayers;
        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private TowerData[] towerPrefabs;
        [SerializeField] private TowerButton buttonPrefab;

        public TowerSelection SelectedTower { get; private set; }
        public bool IsActive => gameObject.activeInHierarchy;

        private void Awake()
        {
            foreach (TowerData tower in towerPrefabs)
            {
                TowerButton button = Instantiate(buttonPrefab, buttonPanel.transform, false);
                button.TowerIcon.sprite = tower.buildMenuIcon;
                button.NameLabel.text = tower.name;
                button.PriceLabel.text = tower.cost.ToString();
                button.Button.onClick.AddListener(() => SelectTower(tower));
            }
        }

        private void SelectTower(TowerData tower)
        {
            if (!ResourceManager.Instance.HasGold(tower.cost)) return;
            Blueprint blueprint = Instantiate(tower.blueprintPrefab, transform.parent);
            blueprint.Setup(blockedLayers, groundLayer);
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
        }

        public void Hide() => gameObject.SetActive(false);

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