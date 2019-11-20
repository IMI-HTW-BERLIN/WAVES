﻿using Buildings;
using Enums;
using UnityEngine;

namespace UI
{
    public class UpgradeMenu : MonoBehaviour
    {
        [SerializeField] private Vector2 offset;

        private Building _selectedBuilding;

        public void ShowForBuilding(Building building)
        {
            // Store selected building
            _selectedBuilding = building;

            // Set position relative to building
            transform.position = building.transform.position + Vector3.right * offset.x + Vector3.up * offset.y;

            // Show menu
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            _selectedBuilding = null;
            gameObject.SetActive(false);
        }

        public void ExecuteAction(UpgradeAction action)
        {
            // If no building selected, ignore
            if (!_selectedBuilding) return;

            switch (action)
            {
                case UpgradeAction.Upgrade:
                    if (_selectedBuilding.IsMaxLevel()) return;
                    _selectedBuilding.UpgradeBuilding();
                    break;
                case UpgradeAction.Repair:
                    if (_selectedBuilding.IsMaxHealth()) return;
                    _selectedBuilding.Repair();
                    break;
                case UpgradeAction.Sell:
                    if (_selectedBuilding is Base) return;
                    _selectedBuilding.Sell();
                    break;
            }
        }
    }
}