using UnityEngine;

namespace Utils
{
    public static class LayerMaskUtil
    {
        public static bool Contains(this LayerMask layerMask, int layer) => layerMask == (layerMask | (1 << layer));
    }
}