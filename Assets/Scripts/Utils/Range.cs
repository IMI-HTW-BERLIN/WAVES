using System;
using System.Runtime.Serialization;

namespace Utils
{
    [Serializable]
    public struct Range
    {
        public float min;
        public float max;

        public float GetRandom() => UnityEngine.Random.Range(min, max);

    }
}
