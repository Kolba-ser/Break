
using UnityEngine;

namespace Break.Utility
{
    public sealed class DontDestroyOnLoad : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        private void OnDestroy()
        {
            Debug.Log($"{name} was destroyed");
        }
    }
}
