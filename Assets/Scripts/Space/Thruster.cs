using UnityEngine;

public class Thruster : MonoBehaviour
{
   [SerializeField] private SpriteRenderer _sprite;

   private void Awake()
   {
      _sprite.gameObject.SetActive(false);
   }

   private void OnShipMove(Vector2 movementInput)
   {
      if (movementInput == Vector2.zero)
      {
         _sprite.gameObject.SetActive(false);
      }
      else
      {
         _sprite.gameObject.SetActive(true);
      }
   }
}
