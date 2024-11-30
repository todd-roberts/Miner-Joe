using UnityEngine;

public enum BrickType {
    Normal,
    Rare
}

public class Brick : MonoBehaviour
{
    public BrickType brickType;
    public int health = 1;

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            // TODO: Add to player inventory
            Destroy(gameObject);
        }
    }
}
