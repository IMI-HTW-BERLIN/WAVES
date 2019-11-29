using System.Collections.Generic;
using Buildings;
using Entities;
using Managers;
using ScriptableObjects.Towers;
using UnityEngine;
using UnityEngine.InputSystem.UI;
using UnityEngine.UI;

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

        [SerializeField] private MultiplayerEventSystem eventSystem;

        public TowerSelection SelectedTower { get; private set; }
        public bool IsShowing => canvas.gameObject.activeSelf;

        private readonly List<Button> _buttons = new List<Button>();

        private void Awake()
        {
            // Set camera reference
            canvas.worldCamera = Camera.main;
            // Add all towers to build menu
            foreach (TowerData tower in towerPrefabs)
            {
                TowerButton towerButton = Instantiate(buttonPrefab, buttonPanel, false);
                towerButton.TowerIcon.sprite = tower.buildMenuIcon;
                towerButton.NameLabel.text = tower.name;
                towerButton.PriceLabel.text = tower.cost.ToString();
                towerButton.Button.onClick.AddListener(() => SelectTower(tower));
                _buttons.Add(towerButton.Button);
            }

            SetNavigationForButtons();
            eventSystem.firstSelectedGameObject = _buttons[0].gameObject;
        }

        private void SetNavigationForButtons()
        {
            Navigation navigation;
            Selectable previous, current;
            for (int i = 1; i < _buttons.Count; i++)
            {
                previous = _buttons[i - 1];
                current = _buttons[i];

                navigation = previous.navigation;
                navigation.selectOnRight = current;
                previous.navigation = navigation;

                navigation = current.navigation;
                navigation.selectOnLeft = previous;
                current.navigation = navigation;
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
            canvas.gameObject.SetActive(true);
        }


        public void Hide() => canvas.gameObject.SetActive(false);

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