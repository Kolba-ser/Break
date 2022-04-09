
using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{
    private  static T instance;

    public static T Instance
    {
        get
        {
            if (!instance)
            {
                instance = FindObjectOfType<T>();
                if (!instance)
                {
                    Debug.LogError($"Singleton of type {typeof(T)} not contains in scene");
                    return null;
                }
            }

            return instance;
        }
    }

    private void Awake()
    {
        if (!instance)
        {
            instance = GetComponent<T>();
        }
        else
        {
            Debug.LogError($"Duplicated Singletone {typeof(T)}");
        }
    }



}

