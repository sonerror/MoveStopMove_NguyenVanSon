using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UIExample
{
    public class GamePlay : UICanvas
    {
        private void Update()
        {
            if(LevelManager.instance.alive == 1)
            {
                WinButton();
            }
        }
        public void WinButton()
        {
           UIManager.Ins.OpenUI<Win>();
           CloseDirectly();
        }

        public void LoseButton()
        {
            UIManager.Ins.OpenUI<Lose>().score.text = Random.Range(0, 100).ToString();
            CloseDirectly();
        }

        public void SettingButton()
        {
            UIManager.Ins.OpenUI<UISetting>();
        }
    }
}