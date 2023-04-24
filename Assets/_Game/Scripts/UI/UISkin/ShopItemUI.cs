using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShopItemUI : MonoBehaviour
{
    public Text priceText;
    public Image hub;
    public Button btn;
    private bool isOwned =false;
    public void UpdateUI(ShopItem item, int shopItemId)
    {
        if (item == null)  return;

        if(hub)
            hub.sprite = item.hub;
        bool isUnlocked = Pref.GetBool(Constant.SKIN_PREF + shopItemId);
        if(isUnlocked)
        {
            if(shopItemId == Pref.CurSkinId)
            {
                if (priceText)
                    priceText.text = "Active";
            }
            else
            {
                if (priceText)
                {
                    priceText.text = "OWNED";
                    isOwned = true;
                }

            }
        }
        else
        {
            if(priceText)
            {
                priceText.text = item.price.ToString();
            }
        }
        if (Pref.Cost < item.price && !isUnlocked)
        {
            btn.interactable = false;
        }

    }
}
