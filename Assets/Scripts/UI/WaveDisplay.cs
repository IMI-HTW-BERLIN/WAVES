using System.Collections;
using TMPro;
using UnityEngine;

namespace UI
{
    public class WaveDisplay : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI txtWaveDisplay;

        /// <summary>
        /// Shows the current Wave
        /// </summary>
        /// <param name="currentWave">The wave index</param>
        /// <param name="animTime">Defines how long the WaveDisplay will be shown (exclusive the fade-animation)</param>
        /// <param name="fadeAnimTime">Sets the time for the fading</param>
        public void ShowCurrentWave(int currentWave, float animTime, float fadeAnimTime)
        {
            txtWaveDisplay.SetText($"WAVE {currentWave + 1}");
            StartCoroutine(WaveDisplayAnimation());

            IEnumerator WaveDisplayAnimation()
            {
                Color color = txtWaveDisplay.color;
                txtWaveDisplay.gameObject.SetActive(true);
                yield return new WaitForSeconds(animTime);
                int steps = (int) (1 / fadeAnimTime * 0.01f * 255);
                for (int i = 255; i >= 0; i -= steps)
                {
                    color.a = i / 255f;
                    txtWaveDisplay.color = color;
                    yield return new WaitForSeconds(0.01f);
                }

                txtWaveDisplay.gameObject.SetActive(false);
                color.a = 1;
                txtWaveDisplay.color = color;
            }
        }
    }
}