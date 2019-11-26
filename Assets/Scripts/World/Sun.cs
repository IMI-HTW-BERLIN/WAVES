using UnityEngine;

namespace World
{
    public class Sun : MonoBehaviour
    {
        [SerializeField] private Transform rotationPoint;

        [Tooltip("Day length in seconds")] [SerializeField]
        private float dayLength;

        [Tooltip("Sun-Angle for defining the start of the night")] [SerializeField]
        private float nightSunAngle;

        public delegate void SunStatus();

        public static event SunStatus SunDown;

        private float _globalLightMaxIntensity;
        private bool _isNight = true;

        private void Update()
        {
            rotationPoint.Rotate(Vector3.forward, 1f / dayLength * Time.deltaTime * 360f);
            if (rotationPoint.eulerAngles.z - nightSunAngle > 0)
            {
                if (_isNight) return;
                _isNight = true;
                SunDown?.Invoke();
            }
            else
                _isNight = false;
        }
    }
}