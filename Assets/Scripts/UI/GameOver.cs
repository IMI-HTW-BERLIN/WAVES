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

        private void Start()
        {
            retryButton.onClick.AddListener(GameManager.StartGame);
            quitButton.onClick.AddListener(Application.Quit);
        }
    }
}