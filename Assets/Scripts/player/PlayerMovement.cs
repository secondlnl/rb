using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    [SerializeField] private float moveSpeed = 300f;
    [SerializeField] private float jumpForce = 300f;
    [SerializeField] private LayerMask whatIsGround;
    private float rayDistance = 0.25f;
    [SerializeField] private Transform leftFoot, rightFoot;
    private bool isGrounded;
    private float horizontalAxis;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        horizontalAxis = Input.GetAxis("Horizontal");
        if (Input.GetButtonDown("Jump") && CheckGround() == true)
        {
            Jump();
        }

    }

    private void FixedUpdate()
    {
        if (horizontalAxis > 0)
        {
            FlipSprite(true);

        }
        if (horizontalAxis < 0)
        {
            FlipSprite(false);
        }
        rb.linearVelocity = new Vector2(horizontalAxis * moveSpeed * Time.deltaTime, rb.linearVelocityY);
    }
    private void Jump()
    {
        rb.AddForce(new Vector2(0, jumpForce));
    }
    private void FlipSprite(bool direction)
    {
        sr.flipX = direction;
    }
    private bool CheckGround()
    {
        RaycastHit2D leftHit = Physics2D.Raycast(leftFoot.position, Vector2.down, rayDistance, whatIsGround);
        RaycastHit2D rightHit = Physics2D.Raycast(rightFoot.position, Vector2.down, rayDistance, whatIsGround);

        Debug.DrawRay(leftFoot.position, Vector2.down * rayDistance, Color.blue, rayDistance);
        Debug.DrawRay(rightFoot.position, Vector2.down * rayDistance, Color.blue, rayDistance);

        if (leftHit.collider != null && leftHit.collider.CompareTag("Ground")
        || rightHit.collider != null && rightHit.collider.CompareTag("Ground"))
        {
            return true;
        }
        else return false;

    }
}
