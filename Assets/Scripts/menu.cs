using UnityEngine;
using UnityEngine.SceneManagement;

public class menu : MonoBehaviour
{
    public void Startgame()
    {
        SceneManager.LoadScene(1);
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void ShowCredits() { }
    public void ShowOptions()
    {
        // TODO: Options maybe?
    }

}
