using System.Collections.Generic;
using Interfaces;
using UnityEngine;

namespace Utils
{
    [RequireComponent(typeof(Collider2D))]
    public class TargetFinder : MonoBehaviour
    {
        [SerializeField] private LayerMask attackableLayers;
        
        public readonly List<Damageable> Targets = new List<Damageable>();
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            Damageable damageable = other.GetComponent<Damageable>();
            if (!damageable || !attackableLayers.Contains(other.gameObject.layer)) return;
            Targets.Add(damageable);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            Damageable damageable = other.GetComponent<Damageable>();
            if (!damageable) return;
            Targets.Remove(damageable);
        }
    }
}