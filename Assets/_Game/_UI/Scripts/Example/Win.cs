using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UIExample
{
    public class Win : UICanvas
    {
        private void Awake()
        {
            SoundManager.Ins.SfxPlay(Constant.SOUND_WIN);
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

        }
    }
}