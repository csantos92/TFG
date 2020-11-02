using UnityEngine;
using UnityEngine.SceneManagement;


public class GoTo : MonoBehaviour
{
    //Exit to Windows desktop
    public void CloseGame()
    {
        Application.Quit();
    }

    //Go to title screen
    public void GoToMainScreen()
    {
        SceneManager.LoadScene(0);
    }

    //Go to tutorial screen
    public void GoToTutorial()
    {
        SceneManager.LoadScene(1);
    }

    //Go to game level
    public void StartGame()
    {
        SceneManager.LoadScene(2);
    }

}
