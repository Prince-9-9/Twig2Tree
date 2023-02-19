using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] bool isStartingOpen;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Animator>().SetBool("isOpen", isStartingOpen);
    }

}
