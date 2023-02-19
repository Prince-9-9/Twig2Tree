using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int health;
    [SerializeField] int maxHealth;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }


    public void Update()
    {
        if (health == 0)
        {
            Invoke("NextScene", 3f);
        }
    }
    public void TakeDamage(int amt)
    {
        health -= amt;
        GetComponent<Animator>().SetTrigger("Hurt");
        if (health <= 0)
        {
            // Die
            Destroy(gameObject);
        }

    }

    public void IncreaseHealth(int amt)
    {
        health += amt;
        if (health > maxHealth) health = maxHealth;
    }

    public int GetHealth()
    {
        return health;
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }


    public void NextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}
