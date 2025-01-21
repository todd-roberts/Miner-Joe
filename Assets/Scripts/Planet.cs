using System;
using UnityEngine;

[Serializable]
public class Planet
{
    public Vector3 Position { get; private set;}
    public int BaseColorIndex { get; private set; } // 0 to 6
    public int LandTypeIndex { get; private set; }  // 0 to 3 (4 = No Land)
    public bool HasCivilization { get; private set; }
    public bool HasRings { get; private set; }
    public float InitialRotation { get; private set; } // 0 to 360
    public string Name { get; private set; }
    public bool IsBig { get; private set; }

    public static Planet CreateRandom(Vector3 position)
    {
        System.Random random = new();

        int baseColorIndex = random.Next(0, 7); // 7 colors
        int landTypeIndex = random.Next(0, 5); // 4 land types + No Land
        bool hasCivilization = random.NextDouble() < 0.35;
        bool hasRings = random.NextDouble() < 0.15;
        float initialRotation = (float)(random.NextDouble() * 360); // 0 to 360
        bool isBig = random.NextDouble() < 0.10;


        Planet planet = new () {
            Position = position,
            BaseColorIndex = baseColorIndex,
            LandTypeIndex = landTypeIndex,
            HasCivilization = hasCivilization,
            HasRings = hasRings,
            InitialRotation = initialRotation,
            IsBig = isBig,
        };

        planet.NamePlanet();

        return planet;
    }

     private void NamePlanet()
    {
        string[][] baseSyllables = new string[][] {
        new string[] { "Roy", "Re", "Apple", "Anger" },
        new string[] { "Oyg", "Or", "Orange", "Excite" },
        new string[] { "Ygb", "Yel", "Nana", "Happy" },
        new string[] { "Gbi", "Gre", "Kiwi", "Jelly" },
        new string[] { "Biv", "Blu", "Berry", "Sad" },
        new string[] { "Ivr", "Ind", "Elder", "Depressed" },
        new string[] { "Vro", "Vi", "Grape", "Snazzy" },
    };

        string[] basePossibilities = baseSyllables[BaseColorIndex];

        string[][] landSyllables = new string[][] {
        new string[] { "earth", "grow", "leaf", "tree", "grass" },
        new string[] { "dust", "sand", "crag", "crack", "rock" },
        new string[] { "ice", "froze", "chill", "zero", "cold" },
        new string[] { "molt", "heat", "lava", "burn", "fire" },
        new string[] {"void", "gas", "neb", "quiet", "bleak"}
    };

        string[] landPossibilities = landSyllables[LandTypeIndex];

        string basePart = basePossibilities[UnityEngine.Random.Range(0, basePossibilities.Length)];
        string landPart = landPossibilities.Length > 0
            ? landPossibilities[UnityEngine.Random.Range(0, landPossibilities.Length)]
            : "";

        int rotationWhole = Mathf.FloorToInt(InitialRotation);

        Name = $"{basePart}{landPart} {rotationWhole}";
    }


    public static Planet CreateGoldilocks()
    {
        return new()
        {
            Position = Vector3.zero,
            BaseColorIndex = 4,
            LandTypeIndex = 0,
            HasCivilization = false,
            HasRings = false,
            InitialRotation = 0,
            IsBig = false,
            Name = "Goldilocks"
        };
    }
}
