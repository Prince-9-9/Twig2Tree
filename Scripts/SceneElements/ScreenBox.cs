using UnityEngine;

public class ScreenBox : MonoBehaviour
{
    private GameObject scene;

    private void Awake()
    {
        scene = transform.GetChild(0).gameObject;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            scene.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            scene.SetActive(false);
        }
    }
}
