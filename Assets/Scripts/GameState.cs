using System.Collections.Generic;
using UnityEngine;

public static class GameState
{
    public static MinerState Miner = new();
    public static SpaceshipState Spaceship = new();
    public static bool UniverseInitialized = false;
    public static Dictionary<Vector3, Planet> Planets = new() {
        { Vector3.zero, Planet.CreateGoldilocks() }
    };

    public static Vector3 CurrentPlanetPosition = Vector3.zero;

    public static Planet GetCurrentPlanet() => Planets[CurrentPlanetPosition];
}

public class MinerState
{
    public Vector3 Position = Vector3.zero;
    public Vector2 Facing = Vector2.down;
    public HashSet<int> Keys = new();
    public int? DoorEntered = null;
}

public class SpaceshipState
{
    public Vector3 Position = Vector3.zero;
    public Vector2 Velocity = Vector2.zero;
}
