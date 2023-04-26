using System.Collections;
using System.Collections.Generic;
using UIExample;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UILose : UICanvas
{
    private void Start()
    {
        SoundManager.Ins.sfxSource.Stop();
        SoundManager.Ins.SfxPlay(Constant.SOUND_LOSE);
    }

    public void MenuButton()
    {
        SoundManager.Ins.SfxPlay(Constant.SOUND_BUTTON);
        UIManager.Ins.OpenUI<MainMenu>();
        this.CloseDirectly();
        ResetGame();
    }
    public void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
