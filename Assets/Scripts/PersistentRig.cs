using UnityEngine;
using UnityEngine.SceneManagement;

public class PersistentRig : MonoBehaviour
{
    private static PersistentRig _instance;
    private void Awake()
    {
       HandleSingleton();
       SceneManager.sceneLoaded += BroadCastSceneLoaded;
    }

    private void HandleSingleton() {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    private static void BroadCastSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        _instance.BroadcastMessage("OnSceneLoaded", scene);
    }
}
