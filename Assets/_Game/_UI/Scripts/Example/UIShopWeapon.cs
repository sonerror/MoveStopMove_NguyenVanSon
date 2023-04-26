using System.Collections;
using System.Collections.Generic;
using UIExample;
using UnityEngine;

public class UIShopWeapon : UICanvas
{
    public void ButtonQuit()
    {
        SoundManager.Ins.SfxPlay(Constant.SOUND_BUTTON);
        CloseDirectly();
        UIManager.Ins.OpenUI<MainMenu>();
        GameManager.Ins.UIMainMenu();
    }
}
