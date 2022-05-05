using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private BoxCollider2D boxC;
    [SerializeField] private LayerMask platformMask;
    [SerializeField] private LayerMask ladderMask;
    private SpriteRenderer s_render;
    [SerializeField] private float jumpForce = 10f;
    // [SerializeField] private float downPull = -10f;
    [SerializeField] private float moveSpeed = 10f;
    float horizontalMove = 0f;
    float verticalMove = 0f;
    private bool running = false;
    private bool climbing = false;
    private bool canDoubleJump = false;
    private bool grounded = true;
    private Collider2D[] overlaps;
    private PowerUpState powerUps;

    // Start is called before the first frame update
    void Start()
    {
        s_render = GetComponent<SpriteRenderer>();
        powerUps = GetComponent<PowerUpState>();
    }

    void Awake()
    {
        overlaps = new Collider2D[10];
    }
    private void CheckCollision()
    {

        // grounded = IsGrounded();
        IsClimbing();
    }

    // Update is called once per frame
    void Update()
    {
        // ResetJumps();

        CheckCollision();
        rb.gravityScale = 2;

        if (climbing)
        {
            verticalMove = Input.GetAxis("Vertical");
            rb.gravityScale = 0;
            rb.velocity = new Vector2(1, moveSpeed / 2 * verticalMove);
        }
        else
        {
            if (Input.GetButtonDown("Jump"))
            {
                if(grounded){
                    rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                    canDoubleJump = true;
                }else if(canDoubleJump && powerUps.DoubleJumps > 0){
                    rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                    powerUps.HasDoubleJumped();
                    canDoubleJump = false;
                }
            }
        }

        horizontalMove = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveSpeed * horizontalMove, rb.velocity.y);

        //change this with animation SetBool
        running = horizontalMove != 0f;
        s_render.flipX = horizontalMove < 0f;
    }

    void FixedUpdate()
    {
        // CheckCollision();
    }

    // private bool CanJump(){
    //     return grounded || (jumps > 0 && powerUps.DoubleJumps > 0);
    // }

    // private void ResetJumps(){
    //     if(grounded){
    //         jumps = 2;
    //     }
    // }

    private void IsClimbing()
    {
        climbing = false;
        grounded = false;

        Vector2 size = boxC.bounds.size;
        size.y += 0.1f;
        size.x /= 2f;

        int overlap_amount = Physics2D.OverlapBoxNonAlloc(transform.position, size, 0f, overlaps);

        for (int i = 0; i < overlap_amount; i++)
        {
            GameObject hit = overlaps[i].gameObject;

            if (hit.layer == LayerMask.NameToLayer("Ladder"))
            {
                climbing = true;
            }
            else if (hit.layer == LayerMask.NameToLayer("Platform"))
            {
                grounded = hit.transform.position.y < (transform.position.y - 0.5f);

                Physics2D.IgnoreCollision(overlaps[i], boxC, !grounded);
            }
            // Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Barrier"), gameObject.layer, true);
        }
    }

    // Deprecated
    private bool IsGrounded()
    {
        float extraHeight = .01f;

        RaycastHit2D raycastHit =
            Physics2D.BoxCast(boxC.bounds.center, boxC.bounds.size, 0f, Vector2.down,
                                boxC.bounds.extents.y, platformMask);

        Color rayColor = Color.green;
        if (raycastHit.collider != null)
        {
            rayColor = Color.black;
        }
        else
        {
            rayColor = Color.red;
        }

        Debug.DrawRay(boxC.bounds.center,
                        Vector2.down * (boxC.bounds.extents.y + extraHeight), rayColor);

        return raycastHit.collider != null;
    }
}
