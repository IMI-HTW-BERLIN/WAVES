using System.Collections;
using Managers;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
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

        /// <summary>
        /// Selects and highlights the Continue-Button. Needs to wait one frame after it gets enabled
        /// </summary>
        private void OnEnable() => StartCoroutine(WaitOneFrame(continueButton.Select));

        /// <summary>
        /// This fixes the _bug, where Unity won't select the button (visually)
        /// </summary>
        private void OnDisable() => EventSystem.current.SetSelectedGameObject(null);

        private void Restart() => GameManager.StartGame();

        private void Quit() => GameManager.QuitGame();

        private IEnumerator WaitOneFrame(UnityAction onFinish)
        {
            yield return new WaitForEndOfFrame();
            onFinish?.Invoke();
        }
    }
}