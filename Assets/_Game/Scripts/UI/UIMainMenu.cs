using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMainMenu : UICanvas
{
    [SerializeField] private GameObject canVasAlive;
    private void Start()
    {
        Time.timeScale = 0f;
        canVasAlive.SetActive(false);
        LevelManager.instance.player.move = false;
    }
    public void ButtonPLAY()
    {
        LevelManager.instance.player.move = true;
        Time.timeScale = 1f;
        gameObject.SetActive(false);
        canVasAlive.SetActive(true);
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