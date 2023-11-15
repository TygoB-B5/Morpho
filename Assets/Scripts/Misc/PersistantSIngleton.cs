using UnityEngine;


/// <summary>
/// PersistentSingleton class helper for singletons that need to stay alive during scene changes.
/// Use this instaed of Singleton for objects that need to stay alive during the entirety of the game.
/// </summary>
/// <typeparam name="T">Object type</typeparam>
public class PersistentSingleton<T> : MonoBehaviour where T : Component
{
    private static T _instance;
    private static GameObject _obj;

    /// <summary>
    /// Singleton design pattern.
    /// </summary>
    /// <value>The instance.</value>
    public static T Instance
    {
        get
        {
            if (_obj == null)
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<T>();

                    if (_instance == null)
                    {
                        GameObject obj = new GameObject(typeof(T).ToString());
                        _instance = obj.AddComponent<T>();
                    }
                }
            }
            else
            {
                _instance = _obj.GetComponent<T>();
            }
            return _instance;
        }
    }

    /// <summary>
    /// On awake, we check if there's already a copy of the object in the scene. If there's one, we destroy it.
    /// </summary>
    protected virtual void Awake()
    {
        if (!Application.isPlaying)
        {
            return;
        }

        if (_instance == null)
        {
            //If I am the first instance, make me the Singleton
            _instance = this as T;
            _obj = _instance.gameObject;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            //If a Singleton already exists and you find
            //another reference in scene, destroy it!
            Debug.LogWarning($"deleted {gameObject.name}!");
            Destroy(gameObject);
        }
    }
}