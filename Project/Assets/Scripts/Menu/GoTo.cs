using UnityEngine;
using UnityEngine.SceneManagement;

public class GoTo : MonoBehaviour
{
    public GameObject tutorialScreen, destroyMe;

    //Exit to Windows desktop
    public void CloseGame()
    {
        Application.Quit();
    }

    //Go to title screen
    public void GoToMainScreen()
    {
        SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.MENU);
        //SceneManager.LoadScene(0);
        tutorialScreen.SetActive(false);
    }

    //Go to tutorial screen
    public void GoToTutorial()
    {
        SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.MENU);
        tutorialScreen.SetActive(true);
    }

    //Go to game level
    public void StartGame()
    {
        SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.MENU);
        SceneManager.LoadScene(1);
    }

    public void RebootGame()
    {
        if(destroyMe != null)
        {
            PlayerController.playerCreated = false;
            Destroy(destroyMe);
            SceneManager.LoadScene(0);
        }
    }
}
