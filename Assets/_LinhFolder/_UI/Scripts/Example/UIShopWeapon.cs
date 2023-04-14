using System.Collections;
using System.Collections.Generic;
using UIExample;
using UnityEngine;

public class UIShopWeapon : UICanvas
{
    public void ButtonQuit()
    {
        CloseDirectly();
        UIManager.Ins.OpenUI<MainMenu>();
        GameManager.Ins.UIMainMenu();
    }
}
