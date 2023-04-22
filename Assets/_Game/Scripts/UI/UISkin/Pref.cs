using UnityEngine;
public class Pref
{
    public static int CurBtnId
    {
        set => PlayerPrefs.SetInt(Constant.CUR_BTN_ID, value);
        get => PlayerPrefs.GetInt(Constant.CUR_BTN_ID);
    }
    public static int CurSkinId
    {
        set => PlayerPrefs.SetInt(Constant.CUR_SKIN_ID, value);
        get => PlayerPrefs.GetInt(Constant.CUR_SKIN_ID);
    }
    public static int Cost
    {
        set => PlayerPrefs.SetInt(Constant.COST_KEY, value);
        get => PlayerPrefs.GetInt(Constant.COST_KEY);
        
    }
    public static void SetBool(string key, bool isOn)
    {
        if(isOn)
        {
            PlayerPrefs.SetInt(key, 1);
        }
        else
        {
            PlayerPrefs.SetInt(key, 0);
        }
    }
    public static bool GetBool(string key)
    {
/*        if(PlayerPrefs.GetInt(key) == 1)
        {
            return true;
        }
        else
        {
            return false;
        }*/
        return PlayerPrefs.GetInt(key) == 1 ? true: false;
    }

}
