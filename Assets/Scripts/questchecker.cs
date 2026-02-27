using UnityEngine;
using UnityEngine.SceneManagement;


public class questchecker : MonoBehaviour
{
    [SerializeField] private GameObject dialogBox, finishText, notFinishText;
    [SerializeField] private int QuestGoal = 10;
    [SerializeField] private int levelLoad;
    [SerializeField] private int levelLoadDelay;
    private bool loading = false;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            dialogBox.SetActive(true);
            if (other.GetComponent<PlayerController>().collectedCoins >= QuestGoal)
            {
                finishText.SetActive(true);
                loading = true;
                Invoke("LoadLevel", levelLoadDelay);
            }
            else
                notFinishText.SetActive(true);
        }
    }
    private void LoadLevel()
    {
        SceneManager.LoadSceneAsync(levelLoad);

    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !loading)
        {
            dialogBox.SetActive(false);
            finishText.SetActive(false);
            notFinishText.SetActive(false);
        }
    }
}
