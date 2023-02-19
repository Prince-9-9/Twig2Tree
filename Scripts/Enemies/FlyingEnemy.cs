using UnityEngine;

public class FlyingEnemy : MonoBehaviour
{
    [SerializeField] float dashSpeed;
    [SerializeField] Transform idlePoint;
    [SerializeField] float dashRange;
    [SerializeField] int damage;
    [SerializeField] bool isDashing;
    [SerializeField] bool isRetreating;
    GameObject player;
    Rigidbody2D rb;

    bool canDamage;
    [SerializeField] float damageCooldown = 1f;


    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        // isDashing = false;
        canDamage = true;
    }

    void Update() 
    {
        float dist = Vector2.Distance(idlePoint.position, player.transform.position);
        if(dist > dashRange && isDashing)
        {
            ReturnIdle();
        }
        else if(Vector2.Distance(idlePoint.position, transform.position) < 1f && isRetreating)
        {
            rb.velocity = Vector2.zero;
            isRetreating = false;
        }
        else if(dist <= dashRange && !isDashing)
        {
            float delayTime = Random.Range(0f, 2f);
            Invoke("Dash", delayTime);
        }

        transform.right = player.transform.position - transform.position;
    }

    void Dash(){
        Vector2 dir = (player.transform.position - transform.position).normalized;
        rb.velocity = dir * dashSpeed;
        isDashing = true;
        isRetreating = false;
    }

    void ReturnIdle()
    {
        Vector2 dir = (idlePoint.position - transform.position).normalized;
        rb.velocity = dir * dashSpeed;
        isDashing = false;
        isRetreating = true;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Player") && canDamage){
            collision.collider.GetComponent<PlayerHealth>().TakeDamage(false, damage);
            canDamage = false;
            Invoke("DamageTrigger" , damageCooldown);
        }
        ReturnIdle();
    }

    void DamageTrigger()
    {
        canDamage = true;
    }


    void OnDrawGizmosSelected(){
        Gizmos.DrawWireSphere(idlePoint.position, dashRange);
    }
}
