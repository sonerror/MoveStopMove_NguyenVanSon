using System.Collections;
using System.Collections.Generic;
using UIExample;
using UnityEngine;

public class ShopManage : Singleton<ShopManage>
{
    public ShopItem[] items;
    private void Start()
    {
        if(items == null || items.Length <= 0 )
        {
            return;
        }
        else
        {
            for(int i = 0; i< items.Length; i++)
            {
                if(i == 0)
                {
                    Pref.SetBool(Constant.SKIN_PREF + i, true);
                }
                else
                {
                    if(!PlayerPrefs.HasKey(Constant.SKIN_PREF + i))
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