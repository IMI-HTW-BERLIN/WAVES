using System;

namespace Utils
{
    [Serializable]
    public struct Range
    {
        public float min;
        public float max;

        public float GetRandom() => UnityEngine.Random.Range(min, max);

        public override string ToString()
        {
            return "min: " + min + " | max: " + max;
        }
    }
}