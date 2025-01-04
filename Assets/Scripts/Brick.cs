using UnityEngine;

public enum BrickType {
    Normal,
    Rare
}

public class Brick : MonoBehaviour
{
    public BrickType brickType;

    public AudioClip breakSound;
    public int health = 1;

    public int experience = 1;

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
           Break();
        }
    }

    public void Break() {
        Miner.Broadcast("OnBrickBreak", this);
        Destroy(gameObject);
    }
}
