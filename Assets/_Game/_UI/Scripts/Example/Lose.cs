using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace UIExample
{
    public class Lose : UICanvas
    {
        public Text score;
        private void Start()
        {
            
        }
        public void MainMenuButton()
        {
            UIManager.Ins.OpenUI<MainMenu>();
            CloseDirectly();
        }
    }
}