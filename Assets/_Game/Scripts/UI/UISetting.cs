using System.Collections;
using System.Collections.Generic;
using UIExample;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UISetting : UICanvas
{
    public void ButtonHome()
    {
       // LevelManager.Ins.player.move = true;
        Time.timeScale = 1.0f;
        SoundManager.Ins.SfxPlay(Constant.SOUND_BUTTON);
        LevelManager.Ins.LoseGame();
        UIManager.Ins.OpenUI<MainMenu>();
        CloseDirectly();
    }
    public void ButtonContinue()
    {
        SoundManager.Ins.SfxPlay(Constant.SOUND_BUTTON);
        Time.timeScale = 1.0f;
        CloseDirectly();
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
