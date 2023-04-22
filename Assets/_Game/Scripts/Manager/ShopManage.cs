using System.Collections;
using System.Collections.Generic;
using UIExample;
using UnityEngine;

public class ShopManage : Singleton<ShopManage>
{
    public ShopItem[] itemsShot;
    public ShopItem[] itemBtnHeader;
    private void Start()
    {
        Shot();
        BtnHeader();
    }
    void BtnHeader()
    {
        if (itemBtnHeader == null || itemBtnHeader.Length <= 0)
        {
            return;
        }
        else
        {
            for (int i = 0; i < itemBtnHeader.Length; i++)
            {
                if (i == 0)
                {
                    Pref.SetBool(Constant.BTN_PREF + i, true);
                }
                else
                {
                    if (!PlayerPrefs.HasKey(Constant.BTN_PREF + i))
                    {
                        Pref.SetBool(Constant.BTN_PREF + i, false);
                    }
                }
            }
        }
    }
    void Shot()
    {
        if (itemsShot == null || itemsShot.Length <= 0)
        {
            return;
        }
        else
        {
            for (int i = 0; i < itemsShot.Length; i++)
            {
                if (i == 0)
                {
                    Pref.SetBool(Constant.SKIN_PREF + i, true);
                }
                else
                {
                    if (!PlayerPrefs.HasKey(Constant.SKIN_PREF + i))
                    {
                        Pref.SetBool(Constant.SKIN_PREF + i, false);
                    }
                }
            }
        }
    }
}
[System.Serializable]
public class ShopItem
{
    public int price;
    public Sprite hub;
    public int _index;
}
