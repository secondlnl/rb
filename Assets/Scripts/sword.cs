using UnityEngine;

public class sword : MonoBehaviour
{
    [SerializeField] private GameObject swordPart;

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
        if (other.CompareTag("Enemy"))
        {
            //other.GetComponent<SpriteRenderer>().color = Color.black;
            Instantiate(swordPart, other.transform.position, Quaternion.identity);

            Destroy(other.gameObject);
        }
    }
}
