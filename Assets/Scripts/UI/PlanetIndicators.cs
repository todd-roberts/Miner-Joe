using UnityEngine;

public class PlanetIndicators : MonoBehaviour
{
    [SerializeField] private GameObject _up;
    [SerializeField] private GameObject _down;
    [SerializeField] private GameObject _left;
    [SerializeField] private GameObject _right;

    public void Toggle(Vector2 direction)
    {
        _up.SetActive(direction.y > 0);
        _down.SetActive(direction.y < 0);
        _left.SetActive(direction.x < 0);
        _right.SetActive(direction.x > 0);
    }
}
