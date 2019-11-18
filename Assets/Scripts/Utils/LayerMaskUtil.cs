using UnityEngine;

namespace Utils
{
    public static class LayerMaskUtil
    {
        public static bool HasLayer(LayerMask layerMask, int layer) => layerMask == (layerMask | (1 << layer));
    }
}