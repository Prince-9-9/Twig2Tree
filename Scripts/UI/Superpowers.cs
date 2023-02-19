using UnityEngine;

public class Superpowers : MonoBehaviour
{
    [SerializeField] GameObject strength;
    [SerializeField] GameObject thorn;
    [SerializeField] GameObject jump;
    [SerializeField] GameObject grapple;
    PlayerMovement pMove;
    PlayerAttack pAtk;
    Grappler grap;

    // Start is called before the first frame update
    void Start()
    {
        pMove = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        pAtk = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAttack>();
        grap = GameObject.FindGameObjectWithTag("Player").GetComponent<Grappler>();
    }

    // Update is called once per frame
    void Update()
    {
        if (pMove.canPush)
        {
            strength.SetActive(true);
        }
        else
        {
            strength.SetActive(false);
        }
        if (grap.canGrapple)
        {
            grapple.SetActive(true);
        }
        else
        {
            grapple.SetActive(false);
        }
        if (pAtk.canRangedAttack)
        {
            thorn.SetActive(true);
        }
        else
        {
            thorn.SetActive(false);
        }
        if (pMove.canJumpHigh)
        {
            jump.SetActive(true);
        }
        else
        {
            jump.SetActive(false);
        }
    }
}
