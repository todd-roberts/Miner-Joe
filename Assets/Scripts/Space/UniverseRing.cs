using UnityEngine;

public class UniverseRing
{
    private int _ringNumber;
    private Universe _universe;

    public UniverseRing(int ringNumber, Universe universe)
    {
        _ringNumber = ringNumber;
        _universe = universe;

        for (int i = 0; i < 4; i++) // Four sides of the square ring
        {
            InitializeRingSide(i);
        }
    }

    private float GetRingDistance() => _ringNumber * _universe.GetRingSpread();

    private float GetSideLength() => GetRingDistance() * 2;

    private void InitializeRingSide(int sideIndex)
    {
        // We subtract 1 to avoid placing overlapping planets from conjoining sides 
        int numPositions = Mathf.FloorToInt(GetSideLength() / _universe.GetRingSparseness() - 1);
        float stepSize = GetSideLength() / numPositions;

        for (int i = 0; i < numPositions; i++)
        {
            InitializePosition(i, sideIndex, stepSize);
        }
    }

    private void InitializePosition(int positionIndex, int sideIndex, float stepSize)
    {
        float offset = positionIndex * stepSize - GetRingDistance();

        Vector3 position = GetPosition(offset, sideIndex);

        if (Random.value < _universe.GetPlanetChance())
        {
            position += (Vector3)Random.insideUnitCircle * _universe.GetJitter();

            // We don't initialize Planet data until it's necessary. We simply store the position.
            GameState.Planets.Add(position, null);
        }
    }

    private Vector3 GetPosition(float offset, int sideIndex)
    {
        float ringDistance = GetRingDistance();

        return sideIndex switch
        {
            0 => new Vector3(offset, ringDistance, 0), // Top side
            1 => new Vector3(ringDistance, -offset, 0), // Right side
            2 => new Vector3(-offset, -ringDistance, 0), // Bottom side
            3 => new Vector3(-ringDistance, offset, 0), // Left side
            _ => Vector3.zero
        };
    }

}

