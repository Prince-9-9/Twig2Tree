using UnityEngine;

public class Spikes : MonoBehaviour
{
    [SerializeField] int damage = 20;

    void OnTriggerEnter2D(Collider2D collider){
        if(collider.CompareTag("Player")){
            collider.GetComponent<PlayerHealth>().TakeDamage(true, damage);
        }
    }
}
