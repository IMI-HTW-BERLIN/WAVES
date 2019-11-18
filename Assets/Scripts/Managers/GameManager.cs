using Buildings;
using UI;
using UnityEngine;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private GameOver gameOverScreen;
        [SerializeField] private Base playerBase;
        [SerializeField] private Transform enemySpawnPoint;

        public static GameManager Instance;
        public Transform PlayerSpawnPosition => playerBase.transform;
        public Transform EnemySpawnPoint => enemySpawnPoint;
        public Base PlayerBase => playerBase;

        public static int Score => 524; //{ get; private set; }

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
}