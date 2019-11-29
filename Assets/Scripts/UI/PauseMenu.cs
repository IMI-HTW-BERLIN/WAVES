using Managers;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class PauseMenu : MonoBehaviour
    {
        [Header("Buttons")] [SerializeField] private Button continueButton;
        [SerializeField] private Button restartButton;
        [SerializeField] private Button quitButton;

        private void Awake()
        {
            continueButton.onClick.AddListener(GameManager.Instance.TogglePause);
            restartButton.onClick.AddListener(Restart);
            quitButton.onClick.AddListener(Quit);
        }

        private void Restart() => GameManager.StartGame();

        private void Quit() => GameManager.QuitGame();
    }
}