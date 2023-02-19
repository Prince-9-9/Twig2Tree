using UnityEngine;
using UnityEngine.UI;

public class Hearts : MonoBehaviour
{
    int health;
    int maxHealth;
    [SerializeField] Image[] hearts;
    [SerializeField] Sprite fullHeart;
    [SerializeField] Sprite emptyHeart;
    PlayerHealth player;


    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }
    // Update is called once per frame
    void Update()
    {
        maxHealth = player.GetMaxHealth();
        health = player.GetHealth();
        for (int i = 0; i < maxHealth; i++)
        {
            if (i < health)
            {
                hearts[i].sprite = fullHeart;
            }    
            else
            {
                hearts[i].sprite = emptyHeart;
            }
        }
    }
}
