using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Image healthBar;

        public void SetHealthBar(int currentHealth, int maxHealth)
        {
            float percentage = (float) currentHealth / maxHealth;
            healthBar.fillAmount = percentage;
        }
    }
}