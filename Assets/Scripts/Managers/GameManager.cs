using Buildings;
using Enums;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;

namespace Managers
{
    public class GameManager : Singleton<GameManager>
    {
        [SerializeField] private GameOver gameOverScreen;
        [SerializeField] private PauseMenu pauseMenu;
        [SerializeField] private Base playerBase;
        [SerializeField] private UpgradeMenu upgradeMenu;
        public Transform PlayerSpawnPosition => playerBase.transform;
        public Base PlayerBase => playerBase;

        public static int Score => 524; //{ get; private set; }

        private bool _isPaused;

        public void ShowUpgradeMenu(Building building) => upgradeMenu.ShowForBuilding(building);

        public void HideUpgradeMenu() => upgradeMenu.Hide();

        public void ExecuteUpgradeAction(UpgradeAction action) => upgradeMenu.ExecuteAction(action);

        /// <summary>
        /// Freezes the time and shows the GameOverScreen
        /// </summary>
        public void GameOver()
        {
            Time.timeScale = 0f;
            gameOverScreen.Show();
        }

        /// <summary>
        /// Shows/Hides the PauseMenu and sets the TimeScale respectively
        /// </summary>
        public void TogglePause()
        {
            _isPaused = !_isPaused;
            pauseMenu.gameObject.SetActive(_isPaused);
            Time.timeScale = _isPaused ? 0 : 1;
        }

        /// <summary>
        /// Starts the game by loading the GameScene. Can be used to restart the game as well
        /// </summary>
        public static void StartGame()
        {
            SceneManager.LoadScene(Consts.SCENE_GAME_SCENE);
            Time.timeScale = 1f;
        }

        /// <summary>
        /// Exits the game
        /// </summary>
        public static void QuitGame()
        {
            Application.Quit();
#if UNITY_EDITOR //Exits the play-mode
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }
    }
}