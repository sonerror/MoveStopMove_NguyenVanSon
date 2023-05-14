using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UIExample
{
    public class UIGamePlay : UICanvas
    {
        private void Start()
        {
            Time.timeScale = 1f;
            LevelManager.Ins.player.move = true;
        }
        private void Update()
        {
            if(LevelManager.Ins.alive == 1)
            {
                WinButton();
            }
        }
        public void WinButton()
        {
           UIManager.Ins.OpenUI<UIWin>();
           CloseDirectly();
        }
        public void LoseButton()
        {
            UIManager.Ins.OpenUI<Lose>().score.text = Random.Range(0, 100).ToString();
            CloseDirectly();
        }

        public void SettingButton()
        {
            SoundManager.Ins.SfxPlay(Constant.SOUND_BUTTON);
            Time.timeScale = 0.0f;
            UIManager.Ins.OpenUI<UISetting>();
        }
    }
}