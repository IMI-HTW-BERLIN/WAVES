using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class TowerButton : MonoBehaviour
    {
        [SerializeField] private Image towerIcon;
        [SerializeField] private TextMeshProUGUI nameLabel;
        [SerializeField] private TextMeshProUGUI priceLabel;
        [SerializeField] private Button button;

        public Image TowerIcon => towerIcon;
        public TextMeshProUGUI NameLabel => nameLabel;
        public TextMeshProUGUI PriceLabel => priceLabel;
        public Button Button => button;
    }
}