using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
namespace UIExample
{
    public class MainMenu : UICanvas
    {
        public void PlayButton()
        {
            GameManager.Ins.StartGame();
            UIManager.Ins.OpenUI<GamePlay>();
            foreach (var item in BotManager.instance.bots)
            {
                item._isCanMove = true;
            }
            CloseDirectly();
        }
        public void buttomSkin()
        {

        }
    }
}