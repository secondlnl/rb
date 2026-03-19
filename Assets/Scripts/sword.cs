using UnityEngine;

public class sword : MonoBehaviour
{
    [SerializeField] private GameObject swordPart;

    public void attack(int direct)
    {
        Instantiate(swordPart, transform.position + new Vector3(1f, 0f) * direct, Quaternion.identity);

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            //other.GetComponent<SpriteRenderer>().color = Color.black;

            Destroy(other.gameObject);
        }
    }
}
