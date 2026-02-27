using System.Collections.Generic;
using System.Collections.Specialized;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator anim;
    private bool knockedOut = false, inventoryOpen = false;
    private AudioSource audSrc;
    private List<Item> dummyinventory = new List<Item>();
    [SerializeField] private float moveSpeed = 300f;
    public bool haveSword = false, haveBelt = false, isPaused = false;
    [SerializeField] private GameObject sword, belt, inventoryPanel;
    [SerializeField] private GameObject pickUpPart, jumpPart;
    [SerializeField] private AudioClip[] pickups, jumps, hits;
    [SerializeField] private float volume = 0.5f;
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
    public int collectedCoins = 0;
    private bool isGrounded;
    [SerializeField] private GameObject inventory;
    // private bool DouJump = false;
    // [SerializeField] private bool canDouJump = false;
    private float horizontalAxis;

    void Start()
    {
        sword.SetActive(haveSword);
        belt.SetActive(haveBelt);
        inventoryPanel.SetActive(inventoryOpen);
        currentHealth = startingHealth;
        UpdateHealthBar();
        coinText.text = "" + collectedCoins;
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        audSrc = GetComponent<AudioSource>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && haveBelt)
        {

            inventoryPanel.SetActive(!inventoryOpen);
            inventoryOpen = !inventoryOpen;
            // if (dummyinventory.Equals(inventory.GetComponent<Plinventory>().Inventory) == false)
            // {
            inventory.GetComponent<Plinventory>().UpdateInventory();
            // dummyinventory = inventory.GetComponent<Plinventory>().Inventory;

            // }
            isPaused = inventoryOpen;
            if (inventoryOpen) { Time.timeScale = 0; }
            if (inventoryOpen == false) { Time.timeScale = 1; }
        }
        if (isPaused) { return; }
        horizontalAxis = Input.GetAxis("Horizontal");

        if (horizontalAxis > 0)
        {
            FlipSprite(false);

        }
        else if (horizontalAxis < 0)
        {
            FlipSprite(true);
        }
        if (Input.GetButtonDown("Fire1") || Input.GetKeyDown(KeyCode.Z) && haveSword == true) { Attack(); }
        if (Input.GetButtonDown("Jump") && CheckGround())
        {
            Jump();
            // if (CheckGround() == true)
            // {
            //     // DouJump = false;
            // }
            // if (CheckGround() == false && DouJump == false && canDouJump == true)
            // {
            //     DouJump = true;
            //     rb.linearVelocity = new Vector2(rb.linearVelocityX, 0.0f);
            //     Jump();
            // }
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

        if (other.CompareTag("Item"))
        {
            if (other.gameObject.name.ToLower() == "belt")
            {
                haveBelt = true;
                belt.SetActive(true);
                inventory.GetComponent<Plinventory>().Activate();
                audSrc.pitch = Random.Range(0.5f, 1.2f);
                int randomValue = Random.Range(0, pickups.Length);
                audSrc.PlayOneShot(pickups[randomValue], volume);
                Destroy(other.gameObject);
            }
            if (haveBelt && other.gameObject.name.ToLower() != "belt")
            {
                inventory.GetComponent<Plinventory>().Add(other.gameObject.name, other.gameObject.GetComponent<SpriteRenderer>().sprite);
                Destroy(other.gameObject);
                audSrc.pitch = Random.Range(0.5f, 1.2f);
                int randomValue = Random.Range(0, pickups.Length);
                audSrc.PlayOneShot(pickups[randomValue], volume);
            }
        }
        if (other.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
            collectedCoins++;
            coinText.text = "" + collectedCoins;
            audSrc.pitch = Random.Range(0.5f, 1.2f);
            int randomValue = Random.Range(0, pickups.Length);
            audSrc.PlayOneShot(pickups[randomValue], volume);
            Instantiate(pickUpPart, other.transform.position, Quaternion.identity);

        }
        if (other.CompareTag("Heart"))
        {
            HealUp(other.gameObject);
        }
    }
    private void Jump()
    {
        rb.AddForce(new Vector2(0, jumpForce));
        audSrc.pitch = 1;
        int randomValue = Random.Range(0, jumps.Length);
        audSrc.PlayOneShot(jumps[randomValue], volume);
        Instantiate(jumpPart, transform.position, jumpPart.transform.localRotation);

    }
    public void TakeDamage(int hitTaken)
    {
        currentHealth -= hitTaken;
        UpdateHealthBar();
        audSrc.pitch = 0.75f;
        int randomValue = Random.Range(0, hits.Length);
        audSrc.PlayOneShot(hits[randomValue], volume);
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
        if (direction)
        {
            transform.localScale = new Vector3(-1, 1);

        }
        else
            transform.localScale = new Vector3(1, 1);
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
    /// <summary>
    /// Actives the sword animation layer
    /// </summary>
    public void GetSword()
    {
        haveSword = true;
        sword.GetComponent<SpriteRenderer>().flipX = GetComponent<SpriteRenderer>().flipX;
        sword.SetActive(haveSword);
    }
    private void Attack()
    {
        sword.GetComponent<BoxCollider2D>().enabled = true;
        sword.GetComponent<BoxCollider2D>().isTrigger = true;
        Invoke("stopAttack", 0.2f);
    }
    private void stopAttack()
    {
        if (haveSword)
        {
            sword.GetComponent<BoxCollider2D>().isTrigger = false;
            sword.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
