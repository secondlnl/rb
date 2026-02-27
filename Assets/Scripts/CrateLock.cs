using Unity.VisualScripting;
using UnityEngine;

public class CrateLock : MonoBehaviour
{
    [SerializeField] private GameObject door;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
        {
            other.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            other.transform.position = transform.position + new Vector3(0, 0.4f, 0);
            if (door.IsUnityNull() == false) Destroy(door);
        }
    }
}
