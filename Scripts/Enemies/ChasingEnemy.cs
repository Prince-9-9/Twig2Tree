using UnityEngine;

public class ChasingEnemy : MonoBehaviour
{
    [SerializeField] private Vector3 startPosition;
    [SerializeField] private float speed;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        transform.position = startPosition;
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = Vector2.right * speed;
        if (transform.position.x > GameObject.FindGameObjectWithTag("Player").transform.position.x)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>().TakeDamage(false , 10);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("GeodudeKiller"))
        {
            speed = 0f;
        }
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerHealth>().TakeDamage(false , 10);
        }
    }
}
