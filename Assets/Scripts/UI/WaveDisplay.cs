using TMPro;
using UnityEngine;
using World;

namespace UI
{
    public class WaveDisplay : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI txtWaveDisplay;
        [SerializeField] private GameObject txtAnimator;
        private static readonly int Fade = Animator.StringToHash("fade");

        /// <summary>
        /// Shows the current Wave
        /// </summary>
        /// <param name="currentWave">The wave index</param>
        public void ShowCurrentWave(int currentWave)
        {
            string newText = $"WAVE {currentWave + 1}";
            newText += new string('?', Sun.Instance.NumberOfSpeedUps);
            txtWaveDisplay.SetText(newText);
            txtAnimator.GetComponent<Animator>().SetTrigger(Fade);
        }
    }
}