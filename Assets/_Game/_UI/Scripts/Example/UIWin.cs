using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UIExample
{
    public class UIWin : UICanvas
    {
        private void Start()
        {
            SoundManager.Ins.SfxPlay(Constant.SOUND_WIN);
            GameManager.Ins.UIMainMenu();
            Pref.Cost += 50;
        }
        public void ResetGame()
        {
            SoundManager.Ins.SfxPlay(Constant.SOUND_BUTTON);
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
        public void NextLevelButton()
        {
            LevelManager.Ins.NextLevelGame();
        }
    }
}