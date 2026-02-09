using UnityEngine;
using UnityEngine.SceneManagement;

public class menu : MonoBehaviour
{
    public void Startgame()
    {
        SceneManager.LoadSceneAsync("LevelOne");
    }
    public void ShowCredits() { }
    public void ShowOptions()
    {
        // TODO: Options maybe?
    }

}
