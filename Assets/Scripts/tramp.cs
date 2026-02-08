using UnityEngine;

public class tramp : MonoBehaviour
{
    private Animator anim;
    [SerializeField] private float JumpPower = 650f;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody2D plRb = other.GetComponent<Rigidbody2D>();
            plRb.linearVelocity = new Vector2(plRb.linearVelocityX, 0);
            plRb.AddForce(new Vector2(0, JumpPower));
            anim.SetTrigger("Up");
        }
    }
}
