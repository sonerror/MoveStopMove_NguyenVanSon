using System.Collections;
using System.Collections.Generic;
using UIExample;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UISetting : UICanvas
{
    public void ButtonHome()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        CloseDirectly();
    }
    public void ButtonContinue()
    {
        CloseDirectly();
        GameManager.Ins.WaitGame();
    }
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
#if UNITY_STANDALONE
        Application.Quit();
#endif
    }

}
