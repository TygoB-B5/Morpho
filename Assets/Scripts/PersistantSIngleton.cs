using UnityEngine;

public class PersistentSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;
    protected static T Instance
    {
        get
        {
            if(_instance == null)
            {
                GameObject go = new GameObject(typeof(T).Name);
                T comp = go.AddComponent<T>();
                _instance = comp;
                DontDestroyOnLoad(go);
            }
            return _instance;
        }

        set 
        {
            _instance = value;
        }
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = GetComponent<T>();
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Debug.LogWarning($"Destroying secondary instance: {GetType().Name}");
            DestroyImmediate(gameObject);
        }
    }
}
