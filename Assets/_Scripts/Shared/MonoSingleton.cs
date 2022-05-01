
using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{

    [SerializeField] private bool dontDestroyOnLoad;
    
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
            LateAwake();
            if (dontDestroyOnLoad)
                DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (dontDestroyOnLoad)
                Destroy(this);
            else
                Debug.LogError($"Duplicated Singletone {typeof(T)}");
        }
    }

    /// <summary>
    /// Вызывается при первой инициализации.<br/>
    /// Если объект помечен как DontDestroyOnLoad, LateAWake не вызовется второй раз
    /// </summary>

    protected virtual void LateAwake(){}

}

