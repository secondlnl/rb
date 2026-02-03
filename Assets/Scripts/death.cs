using UnityEngine;

public class death : MonoBehaviour
{
    [SerializeField] private Transform spawnposition;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.position = spawnposition.position;
            other.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
        }
    }
}
