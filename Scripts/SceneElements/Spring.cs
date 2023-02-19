using UnityEngine;

public class Spring : MonoBehaviour
{
    [SerializeField] float springBoost;
    Rigidbody2D rb;

    void Awake()
    {
        rb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            rb.velocity = new Vector2(0f, springBoost);
            rb.GetComponent<GrootSelector>().groots[rb.GetComponent<GrootSelector>().index].transform.GetChild(0).GetComponentInChildren<Animator>().SetTrigger("Jump");
        }
    }
}
