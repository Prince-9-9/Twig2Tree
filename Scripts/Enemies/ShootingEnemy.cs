using UnityEngine;

public class ShootingEnemy : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform cannon;
    [SerializeField] float bulletSpeed = 10f;
    [SerializeField] float cannonFrequency;
    
    void Start()
    {
        ShootCannon();
    }

    void ShootCannon()
    {
        GetComponent<Animator>().SetTrigger("Shoot");
        Invoke("ShootCannon", cannonFrequency);
    }

    public void Fire()
    {
        GameObject bullet = Instantiate(bulletPrefab, cannon.position, Quaternion.identity);
        bullet.transform.right = -transform.right;
        bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(bulletSpeed * -transform.localScale.x, 0f);
    }
}
