using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    private void Awake()
    {
        _instance = this;
    }

    public static void Broadcast<TData>(string message, TData data)
    {
        _instance.BroadcastMessage(message, data);
    }

    public void VisitPlanet()
    {
        SceneManager.ChangeScene(0, true);
    }
}
