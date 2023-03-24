using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMainMenu : UICanvas
{
    public void ButtonPLAY()
    {
        Time.timeScale = 1f;
    }
    public void ButtonWeapon()
    {
        //UIManager.Ins.OpenUI<CanvasWeapon>();
    }
    public void ButtonSkin()
    {
        //UIManager.Ins.OpenUI<CanvasSkin>();
    }
}