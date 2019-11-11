using UnityEngine;
using UnityEngine.UI;

namespace Buildings
{
    public class HealthBarBuilding : MonoBehaviour
    {
        [SerializeField] private Image healthBar;

        public void SetHealthBar(int currentHealth, int maxHealth)
        {
            float percentage = (float) currentHealth / maxHealth;
            healthBar.fillAmount = percentage;
        }
    }
}