using UnityEngine;

public class Grappler : MonoBehaviour
{
    public bool canGrapple;
    Transform grapplePoint;
    Camera mainCamera;
    LineRenderer lineRenderer;
    DistanceJoint2D distanceJoint;
    [SerializeField] float checkRadius;
    [SerializeField] LayerMask anchor;
    Collider2D[] anchorPoints;
    Transform anchorPos;
    private GrootSelector groot;

    private void Awake()
    {
        mainCamera = Camera.main;
        distanceJoint = GetComponent<DistanceJoint2D>();
        lineRenderer = GetComponent<LineRenderer>();
        canGrapple = PlayerPrefs.GetInt("status") >= 2;
    }
    // Start is called before the first frame update
    void Start()
    {
        distanceJoint.enabled = false;
        lineRenderer.enabled = false;
        groot = GetComponent<GrootSelector>();
    }

    // Update is called once per frame
    void Update()
    {
        anchorPoints = Physics2D.OverlapCircleAll(transform.position, checkRadius, anchor);
        if (anchorPoints.Length > 0 && canGrapple)
        {
            anchorPos = anchorPoints[0].transform;
            for (int i = 1; i < anchorPoints.Length; i++)
            {
                if (Vector2.Distance(transform.position, anchorPoints[i].transform.position) < Vector2.Distance(transform.position, anchorPos.position))
                {
                    anchorPos = anchorPoints[i].transform;
                }
            }
            Debug.DrawLine(transform.position, anchorPos.position);
        }
        if (Input.GetKey(KeyCode.C) && anchorPoints.Length > 0 && canGrapple)
        {
            groot.groots[groot.index].transform.GetChild(0).GetComponentInChildren<Animator>().SetBool("Swinging", true);
            grapplePoint = groot.grapplePoints[groot.index];
            distanceJoint.connectedAnchor = anchorPos.position;
            lineRenderer.SetPosition(0, grapplePoint.position);
            lineRenderer.SetPosition(1, anchorPos.position);
            distanceJoint.enabled = true;
            lineRenderer.enabled = true;
            
}
        else
        {
            groot.groots[groot.index].transform.GetChild(0).GetComponentInChildren<Animator>().SetBool("Swinging", false);
            distanceJoint.enabled = false;
            lineRenderer.enabled = false;
        }
        if (anchorPoints.Length == 0)
        {
            groot.groots[groot.index].transform.GetChild(0).GetComponentInChildren<Animator>().SetBool("Swinging", false);
            distanceJoint.enabled = false;
            lineRenderer.enabled = false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, checkRadius);
    }
}
