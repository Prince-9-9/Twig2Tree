using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] float blastRadius;
    [SerializeField] int blastDamage;
    [SerializeField] GameObject explosionEffect;
    void OnTriggerEnter2D(Collider2D collider2d)
    {
        if (collider2d.CompareTag("Player") || collider2d.CompareTag("Thorn"))
        {
            Collider2D[] blasted = Physics2D.OverlapCircleAll(transform.position, blastRadius);
            foreach (Collider2D collider in blasted)
            {
                if (collider.CompareTag("Player"))
                {
                    collider.GetComponent<PlayerHealth>().TakeDamage(false, blastDamage);
                }
                else if (collider.GetComponent<EnemyHealth>() != null && !collider.CompareTag("Bomber"))
                {
                    collider.GetComponent<EnemyHealth>().TakeDamage(blastDamage);
                }
                else if (collider.CompareTag("Breakable"))
                {
                    Destroy(collider.gameObject);
                }
            }
            Destroy(Instantiate(explosionEffect, transform.position, Quaternion.identity) , 2f);
            Destroy(transform.parent.gameObject);

        }


        
        

        
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, blastRadius);
    }
}
