using UnityEngine;

public class questchecker : MonoBehaviour
{
    [SerializeField] private GameObject dialogBox, finishText, notFinishText;
    [SerializeField] private int QuestGoal = 10;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            dialogBox.SetActive(true);
            if (other.GetComponent<PlayerMovement>().collectedCoins >= QuestGoal)
            {
                finishText.SetActive(true);
            }
            else
                notFinishText.SetActive(true);
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            dialogBox.SetActive(false);
            finishText.SetActive(false);
            notFinishText.SetActive(false);
        }
    }
}
