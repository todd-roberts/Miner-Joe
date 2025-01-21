using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    [SerializeField] private Fade _fade;
    [SerializeField] private GameObject _loadingScreen;

    private void Awake()
    {
        _instance = this;
        SceneManager.sceneLoaded += BroadCastSceneLoaded;
    }

    public static async void ChangeScene(int sceneIndex, bool showLoadingScreen = false)
    {
        await _instance._fade.FadeOut();

        _instance._loadingScreen.SetActive(showLoadingScreen);

        SceneManager.LoadScene(sceneIndex);
    }

    private static async void BroadCastSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        _instance.BroadcastMessage("OnSceneLoaded", scene);

        _instance._loadingScreen.SetActive(false);

        await _instance._fade.FadeIn();

        _instance.BroadcastMessage("OnSceneReady", scene);
    }


    public static void Broadcast<TData>(string message, TData data)
    {
        _instance.BroadcastMessage(message, data);
    }

    public void VisitPlanet()
    {
        ChangeScene(0, true);
    }
}
