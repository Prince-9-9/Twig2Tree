using UnityEngine;

public class GrootSelector : MonoBehaviour
{
    public GameObject[] groots;
    [SerializeField] private CapsuleCollider2D[] colliders;
    [Range(0,2)]public int index;
    private int previndex;
    public Transform[] grapplePoints;

    private void Awake()
    {
        int status = PlayerPrefs.GetInt("status");
        if (status < 1) index = 0;
        else if (status < 3) index = 1;
        else index = 2;
    }
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < groots.Length; i++)
        {
            groots[i].transform.GetChild(0).GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;
            colliders[i].enabled = false;
            if (i == index)
            {
                groots[i].transform.GetChild(0).GetComponentInChildren<SkinnedMeshRenderer>().enabled = true;
                colliders[i].enabled = true;
            }
        }
        previndex = index;
    }

    // Update is called once per frame
    void Update()
    {
        if (previndex != index)
        {
            for (int i = 0; i < groots.Length; i++)
            {
                groots[i].transform.GetChild(0).GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;
                colliders[i].enabled = false;
                if (i == index)
                {
                    groots[i].transform.GetChild(0).GetComponentInChildren<SkinnedMeshRenderer>().enabled = true;
                    colliders[i].enabled = true;
                }
            }
        }
        previndex = index;
    }
}
