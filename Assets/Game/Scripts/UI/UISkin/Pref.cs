using UIExample;
using UnityEngine;
public class Pref : Singleton<Pref>
{
    public const string NAME_PLAYER_PREF = "NamePlayer";
    public const string KEY_LEVEL = "Level";
    public const string KEY_ALIVEMAX = "MaxAlive";
    public static string SKIN_PREF = "skin_";
    public static string CUR_ITEM_ID = "cur_skin_id";
    public static string COST_KEY = "cost";

    public static string SELECT_WEAPON = "SelectWeapon";
    public static string CUR_BTN_ID = "cur_btn_id";
    public static string BTN_PREF = "btn_";

    public int coin = 0;
    public static int CurBtnId
    {
        set => PlayerPrefs.SetInt(CUR_BTN_ID, value);
        get => PlayerPrefs.GetInt(CUR_BTN_ID);
    }
    public static int CurId
    {
        set => PlayerPrefs.SetInt(CUR_ITEM_ID, value);
        get => PlayerPrefs.GetInt(CUR_ITEM_ID);
    }
    public static int Cost
    {
        set => PlayerPrefs.SetInt(COST_KEY, value);
        get => PlayerPrefs.GetInt(COST_KEY);
        
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
