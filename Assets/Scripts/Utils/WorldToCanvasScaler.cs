using UnityEngine;

namespace Utils
{
    [ExecuteInEditMode]
    public class WorldToCanvasScaler : MonoBehaviour
    {
        // Update is called once per frame
        private void Update()
        {
            if (!transform.parent) return;
            Vector3 localScale = transform.parent.localScale;
            transform.localScale = new Vector3(1f / localScale.x, 1f / localScale.y, 1f / localScale.z);
        }
    }
}