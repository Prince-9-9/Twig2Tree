using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    [SerializeField] private Animator[] doors;
    private List<Collider2D> pressObjects = new List<Collider2D>();
    Animator animator;
    int isOpenHash;
    bool hasOpened;

    private void Start()
    {
        hasOpened = false;
        animator = GetComponent<Animator>();
        isOpenHash = Animator.StringToHash("isOpen");
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!hasOpened)
        {
            pressObjects.Add(collision);
            foreach (Animator anim in doors)
            {
                anim.SetBool(isOpenHash, true);
                animator.SetBool(isOpenHash, true);

            }
            hasOpened = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        pressObjects.Remove(collision);
        if (pressObjects.Count == 0)
        {
            foreach (Animator anim in doors)
            {
                anim.SetBool(isOpenHash, false);
                animator.SetBool(isOpenHash, false);
                hasOpened = false;
            }
        }
    }

}
