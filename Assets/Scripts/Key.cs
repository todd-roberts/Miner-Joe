using UnityEngine;

public class Key : MonoBehaviour
{
    private int _id;
   
    public void Init(Door door) {
        _id = door.GetId();
    }

    private void OnTriggerEnter2D (Collider2D other)
    {
        Miner miner = other.GetComponent<Miner>();

        if (miner == null) return;

        miner.AddKey(_id);

        Destroy(gameObject);
    }
}
