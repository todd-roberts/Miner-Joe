using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    private static SceneManager _instance;

    [SerializeField] private Fade _fade;
    [SerializeField] private GameObject _loadingScreen;

    private void Awake()
    {
        HandleSingleton();
        UnityEngine.SceneManagement.SceneManager.sceneLoaded += BroadCastSceneLoaded;
    }

    private void HandleSingleton()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static async void ChangeScene(int sceneIndex, bool showLoadingScreen = false)
    {
        _instance._fade.gameObject.SetActive(true);
        await _instance._fade.FadeOut();

        _instance._loadingScreen.SetActive(showLoadingScreen);

         UnityEngine.SceneManagement.SceneManager.LoadScene(sceneIndex);
    }

    private static async void BroadCastSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        GameManager.Broadcast("OnSceneLoaded", scene);

        _instance._loadingScreen.SetActive(false);

        await _instance._fade.FadeIn();

        _instance._fade.gameObject.SetActive(false);

        GameManager.Broadcast("OnSceneReady", scene);
    }
}
