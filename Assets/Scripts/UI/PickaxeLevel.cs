using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PickaxeLevel : MonoBehaviour
{
    [SerializeField] private Image _fillBar;
    [SerializeField] private TextMeshProUGUI _level;

    private void Update() {
         _fillBar.fillAmount = Pickaxe.GetLevelCompletionPercentage();
         _level.text = GetLevelText();
    }

    private string GetLevelText() {
        string levelText;

        if (Pickaxe.IsMaxLevel()) {
            levelText = "Max";
        } else {
            int levelNumber = Pickaxe.GetCurrentLevel() + 1;

            levelText = levelNumber.ToString().PadLeft(3, '0');
        }

         return levelText;
    }
}
