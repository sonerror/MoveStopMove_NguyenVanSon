using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace UIExample

{
    public class MainMenu : UICanvas
    {
        private void Start()
        {
            Time.timeScale= 0.0f;
        }
        public void PlayButton()
        {
           GameManager.Ins.StartGame();
            Time.timeScale = 1.0f;
            UIManager.Ins.OpenUI<GamePlay>();
            CloseDirectly();
        }
    }
}