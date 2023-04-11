using System.Collections;
using System.Collections.Generic;
using UIExample;
using UnityEngine;

public class UISetting : UICanvas
{
    void Start()
    {
        Time.timeScale = 0f;
    }
    public void ButtonHome()
    {
        UIManager.Ins.OpenUI<MainMenu>();
        CloseDirectly();
    }
    public void ButtonContinue()
    {
        CloseDirectly();
        Time.timeScale = 1f;
        UIManager.Ins.OpenUI<GamePlay>();

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
