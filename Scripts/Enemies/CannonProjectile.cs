using UnityEngine;

public class CannonProjectile : MonoBehaviour
{
    [SerializeField] float lifetime = 10f;
    [SerializeField] int damage = 10;
    Rigidbody2D rb;

    void Awake(){
        rb = GetComponent<Rigidbody2D>();
    }
    
    void Start(){
        Destroy(gameObject, lifetime);
    }

    void Update(){
        transform.right = rb.velocity;
    }


    void OnTriggerEnter2D(Collider2D collider){
        if(collider.GetComponent<EnemyHealth>() != null){ // Enemy Layer at 8
            return;
        }
        if(collider.CompareTag("Player")){
            collider.GetComponent<PlayerHealth>().TakeDamage(false, damage);
        }
        Destroy(gameObject);
    }

}
