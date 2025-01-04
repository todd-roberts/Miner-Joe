using UnityEngine;

[System.Serializable]
public class ExperienceManager
{
    [SerializeField]
    private int[] _experienceLevels;

    private int _currentLevel = 0;
    private int _currentLevelExperience = 0;
    private int _experience = 0;

    public int GetCurrentLevel() => _currentLevel;

    private int GetCurrentLevelRequiredExperience() => IsMaxLevel() ? 0 : _experienceLevels[_currentLevel];

    public bool IsMaxLevel() => _currentLevel >= _experienceLevels.Length;

    public float GetLevelCompletionPercentage()
    {
        if (IsMaxLevel()) return 1f;

        float completionRatio = (float)_currentLevelExperience / GetCurrentLevelRequiredExperience();
        return Mathf.Clamp01(completionRatio);
    }

    public bool AddExperience(int amount)
    {
        bool leveledUp = false;

        _experience += amount;
        _currentLevelExperience += amount;

        while (!IsMaxLevel())
        {
            int requiredExperience = GetCurrentLevelRequiredExperience();

            if (_currentLevelExperience < requiredExperience) break;

            int overage = _currentLevelExperience - requiredExperience;

            _currentLevel++;

            leveledUp = true;

            _currentLevelExperience = overage;
        }

        return leveledUp;
    }
}
