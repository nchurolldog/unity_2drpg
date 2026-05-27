using UnityEngine;

public class LadderIn : MonoBehaviour
{
    public BoxCollider2D ladderCollider;
    public Collider2D[] mountainCollider;
    
    public SpriteRenderer player;
    public Collider2D[]  airWallCollider;
    private void OnTriggerEnter2D(Collider2D collision)    {
        if (collision.CompareTag("Player"))
        {
                player.sortingOrder = 10;
         foreach (Collider2D collider in mountainCollider)
            {
                collider.isTrigger = true;
                
                
            }
            
           
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            foreach (Collider2D collider in airWallCollider)
            {
                collider.isTrigger = false;
               
            }
           
        }
    }

  
}
