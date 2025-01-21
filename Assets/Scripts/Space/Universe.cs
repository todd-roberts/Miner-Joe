using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class Universe : MonoBehaviour
{
    private static Universe _instance;

    // Settings
    [SerializeField] private float _size = 500f;
    [Tooltip("How far rings are from each other")]
    [SerializeField] private float _ringSpread = 50f;
    [Tooltip("Determines how many potential planet positions are in a ring")]
    [SerializeField] private float _ringSparseness = 100f;
    [Tooltip("Chance of a planet being placed at a position on a ring")]
    [SerializeField] private float _planetChance = 0.25f;
    [Tooltip("How far planets can be placed from their position in any random direction")]
    [SerializeField] private float _jitter = 5f;
    [Tooltip("The distance from the spaceship at which a planet will be spawned")]
    [SerializeField] private float _activationRange = 50f;
    [SerializeField] private GameObject _planetPrefab;
    [SerializeField] private Transform _spaceshipTransform;

    private Dictionary<Vector3, SpacePlanet> _spawnedPlanets = new();


    // Lifecycle Methods
    private void Awake()
    {
        _instance = this;
        InitializePlanetPositions();
    }

    private void InitializePlanetPositions()
    {
        if (GameState.UniverseInitialized) return;

        GameState.UniverseInitialized = true;

        /*
            The Universe is made up of square "rings" on which planets can be placed.
            We subtract 1 from the computed count so that we don't create a ring
            on the boundary of space, where the ship will wrap. 
        */
        int ringCount = Mathf.FloorToInt(_size / _ringSpread - 1);

        for (int ring = 1; ring <= ringCount; ring++)
        {
            new PlanetRing(ring, this);
        }

        foreach (var key in GameState.Planets.Keys) {
            Debug.Log(key);
        }
    }


    private void Update()
    {
        WrapSpaceshipPosition();
        ScanPlanets();
    }

    private void WrapSpaceshipPosition()
    {
        Vector3 position = _spaceshipTransform.position;

        if (position.x > _size) position.x -= _size * 2;
        else if (position.x < -_size) position.x += _size * 2;

        if (position.y > _size) position.y -= _size * 2;
        else if (position.y < -_size) position.y += _size * 2;

        _spaceshipTransform.position = position;
    }

    private void ScanPlanets()
    {
        /* 
            The ToList() call is necessary to avoid a "Collection was modified" exception
            as we may add a planet while checking against the existing collection of them
        */
        List<Vector3> planetPositions = GameState.Planets.Keys.ToList();

        foreach (Vector3 planetPosition in planetPositions)
        {
            CheckPlanetPosition(planetPosition);
        }
    }

    private void CheckPlanetPosition(Vector3 planetPosition)
    {
        Vector3 spaceshipPosition = _spaceshipTransform.position;

        bool inRange = Vector3.Distance(spaceshipPosition, planetPosition) <= _activationRange;

        if (!inRange) return;

        // If the planet is already spawned in the scene, we don't need to do anything
        if (_spawnedPlanets.ContainsKey(planetPosition)) return;
        
        SpawnPlanet(planetPosition);
    }

    private void SpawnPlanet(Vector3 position)
    {
        GameState.Planets[position] ??= Planet.CreateRandom(position);

        GameObject spacePlanetObj = Instantiate(_planetPrefab, position, Quaternion.identity);
        SpacePlanet spacePlanet = spacePlanetObj.GetComponent<SpacePlanet>();

        spacePlanet.Initialize(GameState.Planets[position]);

        _spawnedPlanets.Add(position, spacePlanet);
    }


    // Setting Getters
    public static float GetSize() => _instance._size;
    public float GetRingSpread() => _ringSpread;
    public float GetRingSparseness() => _ringSparseness;
    public float GetJitter() => _jitter;
    public float GetPlanetChance() => _planetChance;

    // Helpers
    public static SpacePlanet GetNearestActivePlanet()
    {
        float minDistance = float.MaxValue;
        SpacePlanet nearestPlanet = null;

        foreach (var kvp in _instance._spawnedPlanets)
        {
            Vector3 planetPosition = kvp.Key;
            SpacePlanet spacePlanet = kvp.Value;

            float distance = Vector3.Distance(GameState.Spaceship.Position, planetPosition);

            if (distance < minDistance)
            {
                minDistance = distance;
                nearestPlanet = spacePlanet;
            }
        }

        return nearestPlanet;
    }
}
