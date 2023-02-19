using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] Transform meleePivot;
    [SerializeField] float meleeRange;
    [SerializeField] int meleeDamage;
    [SerializeField] LayerMask enemyLayers;
    [SerializeField] GameObject thornsPrefab;
    [SerializeField] Transform thornShooter;
    GrootSelector groot;

    public bool canMeleeAttack;
    public bool canRangedAttack;

    private void Awake()
    {
        groot = GetComponent<GrootSelector>();
        canRangedAttack = PlayerPrefs.GetInt("status") >= 4;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X) && canMeleeAttack)
        {
            groot.groots[groot.index].transform.GetChild(0).GetComponentInChildren<Animator>().SetTrigger("Punch");
        }
        if(Input.GetKeyDown(KeyCode.A) && canRangedAttack)
        {
            groot.groots[groot.index].transform.GetChild(0).GetComponentInChildren<Animator>().SetTrigger("Thorn");
        }
    }

    public void MeleeAttack()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(meleePivot.position, meleeRange, enemyLayers);

        foreach(Collider2D enemy in enemies) {
            enemy.GetComponent<EnemyHealth>().TakeDamage(meleeDamage);
        }
    }

    public void RangedAttack()
    {
        GameObject thorn = Instantiate(thornsPrefab, thornShooter.position, Quaternion.identity);
        thorn.transform.right = -transform.right;
    }

    void OnDrawGizmosSelected(){
        Gizmos.DrawWireSphere(meleePivot.position, meleeRange);
        Gizmos.DrawWireSphere(thornShooter.position, .1f);
    }
}
