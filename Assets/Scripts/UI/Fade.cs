using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;

public class Fade : MonoBehaviour
{
    private Image _fadeImage;
    [SerializeField] private float _fadeDuration = 1.0f;

    private void Awake()
    {

        _fadeImage = GetComponent<Image>();

    }

    public async Task FadeOut()
    {
        await FadeToColor(Color.black, _fadeDuration);
    }

    public async Task FadeIn()
    {
        await FadeToColor(Color.clear, _fadeDuration);
    }

    private async Task FadeToColor(Color targetColor, float duration)
    {
        if (_fadeImage == null) return;

        Color startColor = _fadeImage.color;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            _fadeImage.color = Color.Lerp(startColor, targetColor, elapsedTime / duration);
            await Task.Yield();
        }

        _fadeImage.color = targetColor;
    }
}
