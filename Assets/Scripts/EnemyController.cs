using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float knockbackForce = 400f;
    [SerializeField] private float killBounce = 100f;
    [SerializeField] private float uplift = 180f;
    [SerializeField] private int damage = 1;

    private SpriteRenderer sp;


    void Start()
    {
        moveSpeed = Random.Range(1f, 2.5f);
        sp = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        transform.Translate(new Vector2(moveSpeed, 0) * Time.deltaTime);
        if (moveSpeed > 0)
        {
            sp.flipX = true;
        }
        else sp.flipX = false;
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("EnemyBlock") || other.gameObject.CompareTag("Enemy"))
        {
            moveSpeed = -moveSpeed;
        }
        if (other.gameObject.CompareTag("Player") && other.gameObject.GetComponent<PlayerController>().canHurt)
        {
            other.gameObject.GetComponent<PlayerController>().TakeDamage(damage);

            if (other.transform.position.x > transform.position.x)
            {
                other.gameObject.GetComponent<PlayerController>().TakeKnockback(knockbackForce, uplift);
            }
            else
            {
                other.gameObject.GetComponent<PlayerController>().TakeKnockback(-knockbackForce, uplift);

            }
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(other.GetComponent<Rigidbody2D>().linearVelocityX, 0);
            other.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, killBounce));
            Destroy(gameObject);
        }
    }
}
