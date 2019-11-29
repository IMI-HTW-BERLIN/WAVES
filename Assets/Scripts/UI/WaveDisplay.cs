using TMPro;
using UnityEngine;

namespace UI
{
    public class WaveDisplay : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI txtWaveDisplay;
        [SerializeField] private Animator txtAnimator;
        private static readonly int Fade = Animator.StringToHash("fade");

        /// <summary>
        /// Shows the current Wave
        /// </summary>
        /// <param name="currentWave">The wave index</param>
        public void ShowCurrentWave(int currentWave)
        {
            txtWaveDisplay.SetText($"WAVE {currentWave + 1}");
            txtAnimator.SetTrigger(Fade);
        }
    }
}