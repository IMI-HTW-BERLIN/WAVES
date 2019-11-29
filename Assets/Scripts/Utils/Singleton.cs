using UnityEngine;

namespace Utils
{
    public class Singleton<T> : MonoBehaviour where T : class
    {
        public static T Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null && Instance == this as T)
                Destroy(gameObject);
            else
                Instance = this as T;
        }
    }
}