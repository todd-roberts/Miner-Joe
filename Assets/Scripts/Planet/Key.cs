using UnityEngine;

public class Key : MonoBehaviour
{
    private Door _door;
   
   private void Awake() {
     _door = GetComponentInParent<Door>();
   }

    private void OnTriggerEnter2D (Collider2D other)
    {
        Miner miner = other.GetComponent<Miner>();

        if (miner == null) return;

        miner.AddKey(_door.GetId());

        Destroy(gameObject);
    }
}
