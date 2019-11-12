using Buildings;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UpgradeMenu : MonoBehaviour
    {
        [SerializeField] private Button upgradeButton;
        [SerializeField] private Button repairButton;
        [SerializeField] private Button sellButton;
        [SerializeField] private Vector2 offset;

        public void ShowForBuilding(Building building)
        {
            // Set position relative to building
            transform.position = building.transform.position + Vector3.right * offset.x + Vector3.up * offset.y;
            
            // Remove previous click actions
            upgradeButton.onClick.RemoveAllListeners();
            repairButton.onClick.RemoveAllListeners();
            sellButton.onClick.RemoveAllListeners();
            
            // Register upgrade button click actions for building
            if (building.IsMaxLevel())
                upgradeButton.interactable = false;
            else
            {
                upgradeButton.interactable = true;
                upgradeButton.onClick.AddListener(building.Upgrade);
            }
            // Register repair button click actions for building
            if (building.IsMaxHealth())
                repairButton.interactable = false;
            else
            {
                repairButton.interactable = true;
                repairButton.onClick.AddListener(building.Repair);
                repairButton.onClick.AddListener(Hide);
            }
            // Register sell button click actions for building
            sellButton.onClick.AddListener(building.Sell);
            sellButton.onClick.AddListener(Hide);
            
            // If building is base, don't allow selling it
            sellButton.interactable = !(building is Base);

            // Show menu
            gameObject.SetActive(true);
        }
        
        public void Hide() => gameObject.SetActive(false);
    }
}
