using System.Collections;
using System.Collections.Generic;
using TMPro;
using UIExample;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UILose : UICanvas
{
    public TextMeshProUGUI textAlive;
    private void Start()
    {
        SoundManager.Ins.sfxSource.Stop();
        SoundManager.Ins.SfxPlay(Constant.SOUND_LOSE);
        Pref.Cost += 10;
        Debug.Log(LevelManager.Ins.alive);
        textAlive.text = "Số Người Còn Sống: " + LevelManager.Ins.alive.ToString();

    }
    public void MenuButton()
    {
        SoundManager.Ins.SfxPlay(Constant.SOUND_BUTTON);
       LevelManager.Ins.LoseGame();
        UIManager.Ins.OpenUI<MainMenu>();
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