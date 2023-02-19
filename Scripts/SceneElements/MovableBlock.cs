using UnityEngine;

public class MovableBlock : MonoBehaviour
{
    Rigidbody2D rb;
    PlayerMovement player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        rb = GetComponent<Rigidbody2D>();

    }
    // Update is called once per frame
    void Update()
    {
        if (player.canPush)
        {
            rb.mass = 1f;
        }
        else
        {
            rb.mass = 100000f;
        }
    }
}
