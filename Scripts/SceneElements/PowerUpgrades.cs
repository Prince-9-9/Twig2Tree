using UnityEngine;

public class PowerUpgrades : MonoBehaviour
{
    [SerializeField] int powerIndex;
    GrootSelector grootSelector;
    PlayerMovement playerMovement;
    PlayerAttack playerAttack;
    Grappler grappler;

    void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        grootSelector = player.GetComponent<GrootSelector>();
        playerMovement = player.GetComponent<PlayerMovement>();
        playerAttack = player.GetComponent<PlayerAttack>();
        grappler = player.GetComponent<Grappler>();
        if (PlayerPrefs.GetInt("status") >= powerIndex) Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.CompareTag("Player"))
        {
            collider.GetComponent<PlayerHealth>().IncreaseHealth(collider.GetComponent<PlayerHealth>().GetMaxHealth() - collider.GetComponent<PlayerHealth>().GetHealth());
            if(powerIndex == 1)
            {
                grootSelector.index = 1;
                playerMovement.canJumpHigh = true;
                playerMovement.canRunFast = true;
                PlayerPrefs.SetInt("status", 1);
            }
            else if(powerIndex == 2)
            {
                grootSelector.index = 1;
                playerMovement.canJumpHigh = true;
                grappler.canGrapple = true;
                playerMovement.canRunFast = true;
                PlayerPrefs.SetInt("status", 2);
            }
            else if(powerIndex == 3)
            {
                grootSelector.index = 2;
                playerMovement.canJumpHigh = true;
                playerMovement.canPush = true;
                grappler.canGrapple = true;
                playerMovement.canRunFast = true;
                PlayerPrefs.SetInt("status", 3);
            }
            else if(powerIndex == 4)
            {
                grootSelector.index = 2;
                playerMovement.canJumpHigh = true;
                playerMovement.canPush = true;
                grappler.canGrapple = true;
                playerAttack.canRangedAttack = true;
                playerMovement.canRunFast = true;
                PlayerPrefs.SetInt("status", 4);
            }

            Destroy(gameObject);
        }
    }
}
