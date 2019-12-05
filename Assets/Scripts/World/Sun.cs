using UnityEngine;
using Utils;

namespace World
{
    public class Sun : Singleton<Sun>
    {
        [SerializeField] private Transform rotationPoint;

        [Tooltip("Day length in seconds")] [SerializeField]
        private float dayLength;

        [Tooltip("Sun-Angle for defining the start of the night")] [SerializeField]
        private float nightSunAngle;

        public delegate void SunStatus();
        public event SunStatus SunDown;

        public int NumberOfSpeedUps => _numberOfSpeedUps;

        private float _globalLightMaxIntensity;
        private bool _isNight = true;
        private int _numberOfSpeedUps;

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

        public void SpeedUpDayTime()
        {
            _numberOfSpeedUps++;
            dayLength /= 2f;
        }
    }
}