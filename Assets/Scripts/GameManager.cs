using UI;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameOver gameOverScreen;

    public static GameManager Instance;

    public static int Score => 524;//{ get; private set; }

    private void Awake()
    {
        if (Instance)
            Debug.LogError("There is more than one game manager in the scene");
        Instance = this;
    }

    public void GameOver() => gameOverScreen.Show();

    public static void ResetGame()
    {
        Debug.Log("Resetting game...");
        // TODO: Reset game logic
    }
}
