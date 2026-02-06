using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator anim;
    private bool knockedOut = false;

    [SerializeField] private float moveSpeed = 300f;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Color healthGreen;
    [SerializeField] private Color healthRed;
    [SerializeField] private TMP_Text coinText;
    [SerializeField] private Image fillColour;
    [SerializeField] private float jumpForce = 300f;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private Transform leftFoot, rightFoot;
    [SerializeField] private Transform respawnPoint;
    [SerializeField] private int startingHealth = 5;
    private float rayDistance = 0.25f;
    private int currentHealth = 0;
    private int collectedCoins = 0;
    private bool isGrounded;
    private float horizontalAxis;

    void Start()
    {
        currentHealth = startingHealth;
        UpdateHealthBar();
        coinText.text = "" + collectedCoins;
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        horizontalAxis = Input.GetAxis("Horizontal");

        if (horizontalAxis > 0)
        {
            FlipSprite(true);

        }
        else if (horizontalAxis < 0)
        {
            FlipSprite(false);
        }

        if (Input.GetButtonDown("Jump") && CheckGround() == true)
        {
            Jump();
        }
        anim.SetFloat("MoveSpeed", Mathf.Abs(rb.linearVelocityX));
        anim.SetFloat("VerticalSpeed", rb.linearVelocityY);
        anim.SetBool("isGrounded", CheckGround());
    }

    private void FixedUpdate()
    {
        if (knockedOut) { return; }
        rb.linearVelocity = new Vector2(horizontalAxis * moveSpeed * Time.deltaTime, rb.linearVelocityY);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
            collectedCoins++;
            coinText.text = "" + collectedCoins;

        }
        if (other.CompareTag("Heart"))
        {
            HealUp(other.gameObject);
        }
    }
    private void Jump()
    {
        rb.AddForce(new Vector2(0, jumpForce));
    }
    public void TakeDamage(int hitTaken)
    {
        currentHealth -= hitTaken;
        UpdateHealthBar();
        if (currentHealth <= 0)
        {
            Respawn();
        }
    }
    private void Respawn()

    {
        currentHealth = startingHealth;
        UpdateHealthBar();
        transform.position = respawnPoint.position;
        rb.linearVelocity = Vector2.zero;
    }
    public void TakeKnockback(float knocked, float uplift)
    {
        knockedOut = true;
        rb.AddForce(new Vector2(knocked, uplift));
        Invoke("CanMove", 0.25f);
    }
    private void CanMove()
    {
        knockedOut = false;
    }
    private void HealUp(GameObject obj)
    {
        if (currentHealth >= startingHealth) { return; }
        else
        {
            int health = obj.GetComponent<Heart>().healingPower;
            currentHealth += health;
            UpdateHealthBar();
            Destroy(obj);
            if (currentHealth >= startingHealth) { currentHealth = startingHealth; }
        }

    }
    private void UpdateHealthBar()
    {
        healthSlider.value = currentHealth;

        if (currentHealth >= 2)
        {
            fillColour.color = healthGreen;
        }
        else
        {

            fillColour.color = healthRed;
        }
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
