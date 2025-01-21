using UnityEngine;

public static class SpriteExtensions
{
    public static Sprite GetSpriteByIndex(SpriteRenderer renderer, int index)
    {
        Sprite[] sprites = Resources.LoadAll<Sprite>(renderer.sprite.texture.name);
        return sprites.Length > index ? sprites[index] : null;
    }
}