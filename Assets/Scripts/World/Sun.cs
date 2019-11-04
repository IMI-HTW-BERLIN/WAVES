using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;
using Utils;

namespace World
{
    public class Sun : MonoBehaviour
    {
        [SerializeField] private Transform rotationPoint;
        [SerializeField] private Light2D globalLight;
        [SerializeField] private float dayLengthInS;
        [SerializeField] private Range dayLightFallOff;

        public delegate void SunStatus();

        public static event SunStatus SunDown;

        private float _globalLightMaxIntensity;
        private bool _isNight;

        private void Awake() => _globalLightMaxIntensity = globalLight.intensity;

        private void Update()
        {
            rotationPoint.Rotate(Vector3.forward, 1f / dayLengthInS * Time.deltaTime * 360f);
            float angle = Mathf.Abs(rotationPoint.rotation.z);
            float newIntensity = (angle - dayLightFallOff.min) /
                                 ((dayLightFallOff.max - dayLightFallOff.min) / _globalLightMaxIntensity);
            globalLight.intensity = Mathf.Clamp(_globalLightMaxIntensity - newIntensity, 0, _globalLightMaxIntensity);
            if (globalLight.intensity < 0.01f && !_isNight)
            {
                if (_isNight)
                    return;

                _isNight = true;
                SunDown?.Invoke();
            }
            else if (globalLight.intensity >= 0.01f)
                _isNight = false;
        }
    }
}