using UnityEngine;

public class Background : MonoBehaviour
{
    private Renderer _renderer;

    [SerializeField] private float offsetSpeed = 1f;

    void Awake() {
        _renderer = GetComponent<Renderer>();
    }
    public void Offset(Vector3 movement) {
        _renderer.material.mainTextureOffset -= (Vector2)movement * Time.deltaTime * offsetSpeed;
    }
}
