using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] Transform gun;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float bulletSpeed;
    [SerializeField] float mFreq;
    [SerializeField] float gFreq;
    [SerializeField] float pFreq;
    [SerializeField] float gSpeed;
    [SerializeField] float mHeight;
    [SerializeField] float gravity;
    [SerializeField] ParticleSystem thruster;

    // Animator anim;
    Transform player;
    Rigidbody2D rb;
    
    void Awake()
    {
        // anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        thruster.Stop();
    }

    void Start()
    {
        Invoke("ShootGun", 3 * gFreq);
        Invoke("ShootMortar", 3 * mFreq);
        Invoke("ShootProjectile", 3 * pFreq);
        Invoke("StartThruster", 2f);
    }

    void StartThruster()
    {
        thruster.Play();
    }

    void ShootGun()
    {
        // anim.SetTrigger("ShootGun");
        Gun();
        Invoke("ShootGun", gFreq);
    }

    void ShootMortar()
    {
        Mortar();
        Invoke("ShootMortar", mFreq);
    }
    void ShootProjectile()
    {
        Projectile();
        Invoke("ShootProjectile", pFreq);
    }

    public void Gun()
    {
        GameObject projectile = Instantiate(bulletPrefab, gun.position, Quaternion.identity);
        projectile.transform.right = -transform.right;
        // projectile.transform.forward = rb.velocity;
        projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(bulletSpeed * -transform.localScale.x, 0f);
    }

    public void Mortar()
    {
        Vector2 mortarPos = new Vector2(player.position.x, player.position.y + mHeight);
        GameObject projectile = Instantiate(bulletPrefab, mortarPos, Quaternion.identity);
        // projectile.transform.forward = rb.velocity;
        projectile.GetComponent<Rigidbody2D>().gravityScale = 5f;
    }

    public void Projectile()
    {
        GameObject projectile = Instantiate(bulletPrefab, gun.position, Quaternion.identity);
        projectile.GetComponent<Rigidbody2D>().gravityScale = 5f;
        // projectile.transform.right = -transform.right;
        float range = player.position.x - gun.position.x;
        float projSpeed = Mathf.Sqrt(Mathf.Abs(gravity * range) / 2);
        projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(-1, 1) * projSpeed;
    }


}
