using UnityEngine;
using UnityEngine.SceneManagement;


public class GoTo : MonoBehaviour
{
    public void CloseGame()
    {
        Application.Quit();
    }

    public void GoToMainScreen()
    {
        SceneManager.LoadScene(0);
    }

    public void GoToTutorial()
    {
        SceneManager.LoadScene(1);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(2);
    }

}
