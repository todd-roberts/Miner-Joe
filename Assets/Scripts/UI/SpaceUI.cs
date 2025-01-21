        using TMPro;
using UnityEngine;

public class SpaceUI : MonoBehaviour
{
    [SerializeField] private GameObject _spacePlanetUI;
    [SerializeField] private PlanetIndicators _planetIndicators;
    
    private void Awake() {
        _spacePlanetUI.gameObject.SetActive(false);
        _planetIndicators.gameObject.SetActive(false);
    }

    private void OnSpacePlanetEnter(Planet planet) {
        _spacePlanetUI.gameObject.SetActive(true);
        _spacePlanetUI.GetComponentInChildren<TextMeshProUGUI>().text = planet.Name;
    }

    private void OnSpacePlanetExit() {
        _spacePlanetUI.gameObject.SetActive(false);
    }

    private void OnPlanetNearby(Vector2 direction) {
        _planetIndicators.gameObject.SetActive(true);
        _planetIndicators.Toggle(direction);
    }

    private void OnNoPlanetNearby() {
        _planetIndicators.gameObject.SetActive(false);
    }
}
