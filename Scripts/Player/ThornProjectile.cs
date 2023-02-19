using UnityEngine;

public class ThornProjectile : MonoBehaviour
{
    [SerializeField] float lifetime = 10f;
    [SerializeField] float thornSpeed = 100f;
    [SerializeField] int damage = 10;
    Rigidbody2D rb;

    void Awake(){
        rb = GetComponent<Rigidbody2D>();
    }
    
    void Start(){
        float dir = GameObject.FindGameObjectWithTag("Player").transform.localScale.x;
        if(dir != 0){
            dir = dir / Mathf.Abs(dir);
        }
        rb.velocity = new Vector2(thornSpeed * dir, 0f);
        transform.right = -rb.velocity;
        Destroy(gameObject, lifetime);
    }

    void OnTriggerEnter2D(Collider2D collider){
        if(collider.CompareTag("Player") || collider.CompareTag("Bomber")){
            return;
        }
        if(collider.GetComponent<EnemyHealth>() != null)
        {
            collider.GetComponent<EnemyHealth>().TakeDamage(damage);
        }
        Destroy(gameObject);
    }

}
