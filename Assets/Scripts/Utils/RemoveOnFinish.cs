using UnityEngine;

namespace Utils
{
    [RequireComponent(typeof(ParticleSystem))]
    public class RemoveOnFinish : MonoBehaviour
    {
        private ParticleSystem _ps;
        
        private void Awake() => _ps = GetComponent<ParticleSystem>();

        private void Update()
        {
            if (!_ps.IsAlive()) Destroy(gameObject);
        }
    }
}