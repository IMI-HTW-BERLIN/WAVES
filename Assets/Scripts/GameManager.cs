using Buildings;
using DefaultNamespace;
using UI;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameOver gameOverScreen;
    [SerializeField] private UpgradeMenu upgradeMenu;

    public static GameManager Instance;

    public static int Score => 524;//{ get; private set; }

    private void Awake()
    {
        if (Instance)
            Debug.LogError("There is more than one game manager in the scene");
        Instance = this;
    }

    public void GameOver() => gameOverScreen.Show();

    public void ShowUpgradeMenu(Building building) => upgradeMenu.ShowForBuilding(building);

    public void HideUpgradeMenu() => upgradeMenu.Hide();

    public void ExecuteUpgradeAction(UpgradeAction action) => upgradeMenu.ExecuteAction(action);

    public static void ResetGame()
    {
        Debug.Log("Resetting game...");
        // TODO: Reset game logic
    }
}
