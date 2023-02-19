using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int health;
    [SerializeField] int maxHealth;
    [SerializeField] Animator endGameScreen;
    [SerializeField] GameObject hud;
    [SerializeField] Animator hurt;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    public void TakeDamage(bool isReturn , int amt)
    {
        health -= amt;
        if (isReturn && health > 0)
        {
            transform.position = GameObject.FindGameObjectWithTag("SafePlaceHolder").transform.position;
        }
        
        if (health <= 0)
        {
            GetComponent<GrootSelector>().groots[GetComponent<GrootSelector>().index].transform.GetChild(0).GetComponentInChildren<Animator>().SetTrigger("Die");
            GetComponent<PlayerMovement>().enabled = false;
            GetComponent<Grappler>().enabled = false;
            GetComponent<PlayerAttack>().enabled = false;
            GetComponent<Rigidbody2D>().velocity = new Vector2(0f, GetComponent<Rigidbody2D>().velocity.y);
            hud.SetActive(false);
            endGameScreen.SetTrigger("End");
            Invoke("Load", 2f);
        }
        else
        {
            hurt.SetTrigger("End");
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

    private void Load()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
