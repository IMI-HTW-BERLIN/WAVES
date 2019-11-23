using Buildings;
using Enums;
using UI;
using UnityEngine;
using Utils;

namespace Managers
{
    public class GameManager : Singleton<GameManager>
    {
        [SerializeField] private GameOver gameOverScreen;
        [SerializeField] private Base playerBase;
        [SerializeField] private UpgradeMenu upgradeMenu;
        public Transform PlayerSpawnPosition => playerBase.transform;
        public Base PlayerBase => playerBase;

        public static int Score => 524; //{ get; private set; }

        public void ShowUpgradeMenu(Building building) => upgradeMenu.ShowForBuilding(building);

        public void HideUpgradeMenu() => upgradeMenu.Hide();

        public void ExecuteUpgradeAction(UpgradeAction action) => upgradeMenu.ExecuteAction(action);

        public void GameOver() => gameOverScreen.Show();

        public static void ResetGame()
        {
            Debug.Log("Resetting game...");
            // TODO: Reset game logic
        }
    }
}