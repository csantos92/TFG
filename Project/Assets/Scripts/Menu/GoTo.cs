using UnityEngine;
using UnityEngine.SceneManagement;


public class GoTo : MonoBehaviour
{
    public void CloseGame()
    {
        //SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.MENU);

        Application.Quit();
    }

    public void GoToMainScreen()
    {
        //SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.MENU);

        SceneManager.LoadScene(0);
    }

    public void GoToTutorial()
    {
        //SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.MENU);

        SceneManager.LoadScene(1);
    }

    public void StartGame()
    {
        //SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.MENU);

        SceneManager.LoadScene(2);
    }

}
