using UnityEngine;

public class movingplatform : MonoBehaviour
{
    [SerializeField] private Transform target1, target2;
    [SerializeField] private float moveSpeed = 2f;

    private Transform currenttarget;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currenttarget = target1;
    }

    void FixedUpdate()
    {

        if (transform.position == target1.position)
        {
            currenttarget = target2;
        }

        if (transform.position == target2.position)
        {
            currenttarget = target1;
        }

        transform.position = Vector2.MoveTowards(transform.position, currenttarget.position, moveSpeed * Time.deltaTime);
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (other.transform.position.y > transform.position.y)
            {
                other.transform.SetParent(transform);
            }
        }
    }
    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.SetParent(null);
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}
