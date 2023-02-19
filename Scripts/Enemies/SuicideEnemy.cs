using UnityEngine;

public class SuicideEnemy : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float detectRadius;
    // [SerializeField] int blastDamage;
    // [SerializeField] float blastRadius;
    // [SerializeField] Transform bomb;

    GameObject player;
    Rigidbody2D rb;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {   
        if (Vector2.Distance(transform.position , player.transform.position) < detectRadius)
        { 
            float dir = Mathf.Sign(player.transform.position.x - transform.position.x);
            rb.velocity = new Vector2(dir * moveSpeed, 0);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectRadius);
    }

}
