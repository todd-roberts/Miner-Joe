using UnityEngine;

public class PlanetBuilder : MonoBehaviour
{
    [SerializeField] private GameObject _grassGroundPrefab;
    [SerializeField] private GameObject _desertGroundPrefab;
    [SerializeField] private GameObject _iceGroundPrefab;
    [SerializeField] private GameObject _lavaGroundPrefab;

    private void Start()
    {
        Build();
    }

    private void Build() {
        LayGround();
    }

    private void LayGround() {
        GameObject prefab = null;

        Debug.Log("Laying ground for " + GameState.GetCurrentPlanet().Name);

        switch (GameState.GetCurrentPlanet().LandTypeIndex)
        {
            case 0:
            prefab = _grassGroundPrefab;
            break;
            case 1:
            prefab = _desertGroundPrefab;
            break;
            case 2:
            prefab = _iceGroundPrefab;
            break;
            case 3:
            prefab = _lavaGroundPrefab;
            break;
        }

        Instantiate(prefab, transform);
    }
}
