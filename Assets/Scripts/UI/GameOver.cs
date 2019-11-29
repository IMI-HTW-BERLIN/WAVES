using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class GameOver : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI scoreLabel;
        [SerializeField] private Button btnRestart;
        [SerializeField] private Button btnQuit;

        private void Awake()
        {
            btnRestart.onClick.AddListener(GameManager.StartGame);
            btnQuit.onClick.AddListener(Application.Quit);
        }

        public void Show()
        {
            scoreLabel.SetText($"Score:  {GameManager.Instance.Score.ToString()}");
            gameObject.SetActive(true);
        }
    }
}