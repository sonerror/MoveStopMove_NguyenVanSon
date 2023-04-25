using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShopBtnHeader : MonoBehaviour
{
    [SerializeField] public Image hub;
    [SerializeField] public Button btn;

    public void UpdateUIBtnHeader(ShopItem item, int shopItemId)
    {
        if (item == null) return;
        if (hub)
            hub.sprite = item.hub;
        bool isUnlocked = Pref.GetBool(Constant.BTN_PREF + shopItemId);
        if (isUnlocked)
        {

        }
        else
        {

        }
    }
}
