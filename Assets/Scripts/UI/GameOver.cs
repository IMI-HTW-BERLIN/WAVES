using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class GameOver : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI scoreLabel;
        [SerializeField] private Button retryButton;
        [SerializeField] private Button quitButton;

        public void Show()
        {
            scoreLabel.SetText(GameManager.Score.ToString());
            gameObject.SetActive(true);
        }

        private void Hide() => gameObject.SetActive(false);

        private void Start()
        {
            retryButton.onClick.AddListener(GameManager.ResetGame);
            retryButton.onClick.AddListener(Hide);
            quitButton.onClick.AddListener(Application.Quit);
            quitButton.onClick.AddListener(() => Debug.Log("Quitting..."));
        }
    }
}