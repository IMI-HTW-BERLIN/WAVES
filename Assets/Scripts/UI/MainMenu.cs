using Managers;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private Button playButton;
        [SerializeField] private Button quitButton;

        private void Awake()
        {
            playButton.onClick.AddListener(Play);
            quitButton.onClick.AddListener(Quit);
        }

        private void Start() => playButton.Select();

        private void Play() => GameManager.StartGame();

        private void Quit() => GameManager.QuitGame();
    }
}