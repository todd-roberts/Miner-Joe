using UnityEngine;

public class SpacePlanet : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _baseLayer;
    [SerializeField] private SpriteRenderer _landLayer;
    [SerializeField] private SpriteRenderer _civilizationLayer;
    [SerializeField] private SpriteRenderer _ringsLayer;
    [SerializeField] private Transform _body;
    [SerializeField] private float _scale = 3f;

    private Planet _planet;

    public void Initialize(Planet planet)
    {
        _planet = planet;

        Debug.Log("Planet initialized: " + _planet.Name);

        // Set Base Layer
        _baseLayer.sprite = SpriteExtensions.GetSpriteByIndex(_baseLayer, planet.BaseColorIndex);

        // Set Land Layer
        if (planet.LandTypeIndex < 4)
        {
            _landLayer.sprite = SpriteExtensions.GetSpriteByIndex(_landLayer, planet.LandTypeIndex);
            _landLayer.gameObject.SetActive(true);
        }
        else
        {
            _landLayer.gameObject.SetActive(false); // No Land
        }

        _civilizationLayer.gameObject.SetActive(planet.HasCivilization);

        _ringsLayer.gameObject.SetActive(planet.HasRings);

        _body.localRotation = Quaternion.Euler(0, 0, planet.InitialRotation);

        transform.localScale = planet.IsBig ? Vector3.one * _scale * 2 : Vector3.one * _scale;
    }

    public string GetName()
    {
        return _planet.Name;
    }

    private void OnTriggerEnter2D()
    {
        /* 
            We have to set the current planet position instead of setting the
            current planet because passing the instance _planet by reference
            would cause the object to be garbage collected when the scene changes
        */
        GameState.CurrentPlanetPosition = _planet.Position;
        GameManager.Broadcast("OnSpacePlanetEnter", _planet);
    }

    private void OnTriggerExit2D()
    {
        GameManager.Broadcast("OnSpacePlanetExit", _planet);
    }
}
