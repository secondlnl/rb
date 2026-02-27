using UnityEngine;

public class Climbable : MonoBehaviour
{
    [SerializeField] private float climbSpeed = 30f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // print(Mathf.RoundToInt(Input.GetAxis("Vertical")));
            if (Mathf.RoundToInt(Input.GetAxis("Vertical")) == 1)
            {
                other.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, climbSpeed));

            }
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Invoke("SlowDown", 0.2f);
            // other.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(other.GetComponent<Rigidbody2D>().linearVelocityX, 0);

        }
    }
    private void SlowDown()
    {
        GameObject gameObject = GameObject.FindWithTag("Player");
        if (gameObject.CompareTag("Player"))
        {
            gameObject.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(gameObject.GetComponent<Rigidbody2D>().linearVelocityX, 0);


        }
    }
}
