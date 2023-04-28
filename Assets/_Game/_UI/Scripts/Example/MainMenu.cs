using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UIExample
{
    public class MainMenu : UICanvas
    {
        public InputField inputField;

        private void Start()
        {
            Time.timeScale = 1.0f;
        }
        public void PlayButton()
        {
            SoundManager.Ins.SfxPlay(Constant.SOUND_BUTTON);
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
            SoundManager.Ins.SfxPlay(Constant.SOUND_BUTTON);
            UIManager.Ins.OpenUI<ShopDialog>();
            GameManager.Ins.UIShopWeapon();
            CloseDirectly();
        }
        public void ButtonSweapon()
        {
            SoundManager.Ins.SfxPlay(Constant.SOUND_BUTTON);
            UIManager.Ins.OpenUI<UIShopWeapon>();
            GameManager.Ins.UIShopWeapon();
            CloseDirectly();
        }
        public void SetName()
        {
            PlayerPrefs.SetString(Constant.NAME_PLAYER_PREF, inputField.text);
            PlayerPrefs.Save();
            NamePlayer.Ins.SetNamePlayer();
        }
    }
}