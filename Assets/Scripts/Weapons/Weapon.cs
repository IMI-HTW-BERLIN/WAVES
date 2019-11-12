using UnityEngine;

namespace Weapons
{
    [RequireComponent(typeof(SpriteRenderer))]
    public abstract class Weapon : MonoBehaviour
    {
        public SpriteRenderer SpriteRenderer { get; private set; }

        private void Awake() => SpriteRenderer = GetComponent<SpriteRenderer>();

        public abstract void Attack();
    }
}
