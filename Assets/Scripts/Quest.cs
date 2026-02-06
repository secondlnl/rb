using UnityEngine;

public class Quest : MonoBehaviour
{
    [SerializeField] private GameObject textPopup;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            textPopup.SetActive(true);
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            textPopup.SetActive(false);
        }
    }
}
