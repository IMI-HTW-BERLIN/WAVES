using System.Collections;
using Managers;
using TMPro;
using UnityEngine;

namespace UI
{
    public class ResourceUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI txtCurrentGold;

        [Header("Not Enough Gold Animation")] [SerializeField]
        private int animSteps;

        [SerializeField] private float animStepsDelay;

        private void OnEnable()
        {
            ResourceManager.Instance.OnGoldChanged += UpdateText;
            ResourceManager.Instance.OnNotEnoughGold += NotEnoughGold;
        }

        private void OnDisable()
        {
            ResourceManager.Instance.OnGoldChanged -= UpdateText;
            ResourceManager.Instance.OnNotEnoughGold -= NotEnoughGold;
        }

        /// <summary>
        /// Animates the Gold UI from red to white to indicate that the player does not have enough money.
        /// </summary>
        private void NotEnoughGold()
        {
            StopAllCoroutines();
            StartCoroutine(NotEnoughGoldAnimation(animSteps, animStepsDelay));

            IEnumerator NotEnoughGoldAnimation(int steps, float timeBetweenSteps)
            {
                Color txtColor = Color.red;
                for (int i = 0; i <= 255; i += steps)
                {
                    txtColor.g = i / 255f;
                    txtColor.b = i / 255f;
                    txtCurrentGold.color = txtColor;
                    yield return new WaitForSeconds(timeBetweenSteps);
                }
            }
        }

        private void UpdateText(int gold) => txtCurrentGold.SetText($"Gold: {gold}");
    }
}