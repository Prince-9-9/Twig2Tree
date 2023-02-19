using UnityEngine;

public class TriggerDoor : MonoBehaviour
{
    [SerializeField] Animator door;
    [SerializeField] Boss boss;
    [SerializeField] GameObject sound;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            door.SetBool("isOpen", false);
            boss.enabled = true;
            sound.SetActive(true);
        }
    }
}
