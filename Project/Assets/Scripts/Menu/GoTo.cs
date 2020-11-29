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
        SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.MENU);
        SceneManager.LoadScene(0);
    }

    //Go to tutorial screen
    public void GoToTutorial()
    {
        SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.MENU);
        SceneManager.LoadScene(1);
    }

    //Go to game level
    public void StartGame()
    {
        SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.MENU);
        SceneManager.LoadScene(2);
    }

}
