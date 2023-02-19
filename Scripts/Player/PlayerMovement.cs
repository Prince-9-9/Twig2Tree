using UnityEngine;

public class PlayerMovement: MonoBehaviour
{
    private Rigidbody2D rb;
    [Header("Run And Jump")]
    [Space]
    [SerializeField] private float maxRunSpeed = 10f;
    [SerializeField] private float adultRunSpeed;
    [SerializeField] private float babyRunSpeed;
    public bool canRunFast;
    [SerializeField] [Range(0.01f, 1)] private float accelerationGround = 1f;
    [SerializeField] [Range(0.01f, 1)] private float reverseAccGround = 1f;
    [SerializeField] [Range(0.01f, 1)] private float decelerationGround = 1f;
    [Space]
    [SerializeField] private float maxAirSpeed = 10f;
    [SerializeField] [Range(0.01f, 1)] private float accelerationAir = 1f;
    [SerializeField] [Range(0.01f, 1)] private float reverseAccAir = 1f;
    [SerializeField] [Range(0.01f, 1)] private float decelerationAir = 1f;
    [Space]
    public KeyCode jumpButton = KeyCode.Space;
    [SerializeField] private float jumpSpeed;
    [SerializeField] private float highJumpSpeed;
    [SerializeField] [Range(0, 1)] private float jumpCutPercentage = 0.5f;
    [Space]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckSize = 0.01f;
    [SerializeField] private LayerMask whatIsGround;
    private float xInput, yInput;
    private bool isGrounded;
    bool isFacingRight;
    public bool canJumpHigh;


    [Header("Assist Mode")]
    [SerializeField] private float CoyoteTime = 0.2f;
    [SerializeField] private float jumpBuffer = 0.2f;
    private float CoyoteCounter;
    private float jumpBufferCounter;

    [Header("Vine Swinging")]
    private bool isSwinging;

    [Header("Check-Point Reloading")]
    [SerializeField] private LayerMask whatIsSafePlace;
    private Transform safePlaceHolder;
    bool isSafelyPlaced;

    [Header("Pushing Blocks")]
    public bool canPush;
    [SerializeField] Transform boxCheck;
    [SerializeField] LayerMask whatIsBox;

    [Header("Growth")]
    private GrootSelector groot;

    [Header("Load")]
    [SerializeField] private Vector3[] startPoints;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        safePlaceHolder = GameObject.FindGameObjectWithTag("SafePlaceHolder").transform;
        groot = GetComponent<GrootSelector>();
        isFacingRight = true;
        transform.position = startPoints[PlayerPrefs.GetInt("status")];
    }

    void Start()
    {
        jumpBufferCounter = 0f;
        CoyoteCounter = 0f;
        isSwinging = false;
        safePlaceHolder.position = transform.position;
        if (PlayerPrefs.GetInt("status") >= 1)
        {
            canRunFast = true;
            canJumpHigh = true;
        }
        if (PlayerPrefs.GetInt("status") >= 3)
        {
            canPush = true;
        }
    }

    private void Update()
    {
        if (canRunFast)
        {
            maxRunSpeed = adultRunSpeed;
            maxAirSpeed = adultRunSpeed;
        }
        else
        {
            maxRunSpeed = babyRunSpeed;
            maxAirSpeed = babyRunSpeed;
        }
        isSwinging = GetComponent<DistanceJoint2D>().enabled;
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckSize , whatIsGround);
        isSafelyPlaced = Physics2D.OverlapCircle(groundCheck.position, groundCheckSize, whatIsSafePlace);
        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");

        if (!isSwinging)
        {
            // Accelerated Movement on ground
            if (isGrounded)
            {
                if (Mathf.Abs(xInput) <= 0.01f)
                {
                    float sgn = Mathf.Sign(rb.velocity.x);
                    rb.velocity = new Vector2(Mathf.Clamp(Mathf.Abs(rb.velocity.x) - maxRunSpeed * decelerationGround, 0f, maxRunSpeed) * sgn, rb.velocity.y);
                }
                else if (Mathf.Sign(xInput) == -Mathf.Sign(rb.velocity.x))
                {
                    rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x + xInput * maxRunSpeed * reverseAccGround, -maxRunSpeed, maxRunSpeed), rb.velocity.y);
                }
                else if (Mathf.Abs(rb.velocity.x) < maxRunSpeed)
                {
                    rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x + maxRunSpeed * xInput * accelerationGround, -maxRunSpeed, maxRunSpeed), rb.velocity.y);
                }
            }
            // Accelerated Movement in Air
            else
            {
                if (Mathf.Abs(xInput) <= 0.01f)
                {
                    float sgn = Mathf.Sign(rb.velocity.x);
                    rb.velocity = new Vector2(Mathf.Clamp(Mathf.Abs(rb.velocity.x) - maxAirSpeed * decelerationAir, 0f, maxAirSpeed) * sgn, rb.velocity.y);
                }
                else if (Mathf.Sign(xInput) == -Mathf.Sign(rb.velocity.x))
                {
                    rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x + xInput * maxAirSpeed * reverseAccAir, -maxAirSpeed, maxAirSpeed), rb.velocity.y);
                }
                else if (Mathf.Abs(rb.velocity.x) < maxAirSpeed)
                {
                    rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x + maxRunSpeed * xInput * accelerationAir, -maxAirSpeed, maxAirSpeed), rb.velocity.y);
                }
            }

            // Jump
            if (isGrounded)
            {
                CoyoteCounter = CoyoteTime;
            }
            else if (CoyoteCounter > 0f)
            {
                CoyoteCounter -= Time.deltaTime;
            }

            if (Input.GetKeyDown(jumpButton))
            {
                jumpBufferCounter = jumpBuffer;
            }
            else if (jumpBufferCounter > 0f)
            {
                jumpBufferCounter -= Time.deltaTime;
            }

            if (jumpBufferCounter > 0f)
            {
                if (CoyoteCounter > 0f)
                {
                    groot.groots[groot.index].GetComponentInChildren<Animator>().SetTrigger("Jump");
                    if (canJumpHigh)
                    {
                        rb.velocity = new Vector2(rb.velocity.x, highJumpSpeed);
                    }
                    else
                    {
                        rb.velocity = new Vector2(rb.velocity.x , jumpSpeed);
                    }
                    CoyoteCounter = 0f;
                    jumpBufferCounter = 0f;
                }
            }
            if (Input.GetKeyUp(jumpButton) && rb.velocity.y > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * (1 - jumpCutPercentage));
            }

        }

        if (isSafelyPlaced)
        {
            safePlaceHolder.position = transform.position;
        }

        if ((rb.velocity.x > 0 && !isFacingRight) || rb.velocity.x < 0 && isFacingRight)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            isFacingRight = !isFacingRight;
        }

        groot.groots[groot.index].GetComponentInChildren<Animator>().SetFloat("SpeedX", Mathf.Abs(rb.velocity.x));

        bool isPushing = Physics2D.OverlapCircle(boxCheck.position , 0.1f , whatIsBox) && canPush && Mathf.Abs(rb.velocity.x) > 0.01f;
        groot.groots[groot.index].GetComponentInChildren<Animator>().SetBool("Pushing", isPushing);

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckSize);
    }


}