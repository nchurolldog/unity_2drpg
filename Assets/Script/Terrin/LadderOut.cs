using UnityEngine;

public class LadderOut : MonoBehaviour
{
  public BoxCollider2D ladderCollider;
    public Collider2D[] mountainCollider;
      public SpriteRenderer player;
    public Collider2D[]  airWallCollider;
    private void OnTriggerEnter2D(Collider2D collision)    {
        if (collision.CompareTag("Player"))
        {
         foreach (Collider2D collider in mountainCollider)
            {
                collider.isTrigger = false;
                

            }
            
           
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            foreach (Collider2D collider in airWallCollider)
            {
                collider.isTrigger = true;
            
            }
           
        }
        player.sortingOrder = 2;
    }
}
