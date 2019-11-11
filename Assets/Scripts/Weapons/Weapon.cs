using UnityEngine;

namespace Weapons
{
    [RequireComponent(typeof(SpriteRenderer))]
    public abstract class Weapon : MonoBehaviour
    {
        public SpriteRenderer spriteRenderer;
    
        public abstract void Attack();
    }
}