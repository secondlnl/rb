using UnityEngine;

public class attacksign : MonoBehaviour
{
    [SerializeField] private GameObject sign;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sign.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other.GetComponent<PlayerController>().haveSword)
        {
            sign.SetActive(true);
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            sign.SetActive(false);
        }
    }
}
