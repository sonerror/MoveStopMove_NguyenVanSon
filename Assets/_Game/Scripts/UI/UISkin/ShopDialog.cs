using System;
using System.Collections;
using System.Collections.Generic;
using UIExample;
using UnityEngine;
using static UnityEditor.Progress;

public class ShopDialog : UICanvas
{
    public Transform griRoot;
    public ShopItemUI itemPB;

    private void Start()
    {
        Show(true);
    }
    public override void Show(bool isShow)
    {
        base.Show(isShow);
        UpdateUI();
    }
    private void UpdateUI()
    {
        var items = ShopManage.Ins.items;
        if (items == null || items.Length <= 0
            || !griRoot || !itemPB)
        {
            return;
        }
        ClearChild();
        for (int i = 0; i < items.Length; i++)
        {
            int idx = i;
            var item = items[i];
            if (item != null)
            {
                Debug.Log("Skin ins");
                var itemUIClone = Instantiate(itemPB, Vector3.zero, Quaternion.identity);
                itemUIClone.transform.SetParent(griRoot);
                itemUIClone.transform.localPosition = Vector3.zero;
                itemUIClone.transform.localScale = Vector3.one;
                itemUIClone.UpdateUI(item, idx);
                if (itemUIClone.btn)
                {

                    itemUIClone.btn.onClick.RemoveAllListeners();
                    itemUIClone.btn.onClick.AddListener(() => ItemEvent(item, idx));
                    // Debug.Log();
                }
            }

        }
    }
    public void ClearChild()
    {
        if (!griRoot || griRoot.childCount <= 0) return;
        for (int i = 0; i < griRoot.childCount; i++)
        {
            var child = griRoot.GetChild(i);
            if (child)
            {
                Destroy(child.gameObject);
            }
        }
    }
    void ItemEvent(ShopItem item, int shopItemId)

    {
        if (item == null)
        {
            return;
        }
        bool isUnlocked = Pref.GetBool(Constant.SKIN_PREF + shopItemId);
        if (isUnlocked)
        {
            if (shopItemId == Pref.CurSkinId) return;
            Pref.CurSkinId = shopItemId;
            UpdateUI();
        }
        else
        {
            if (Pref.Cost >= item.price)
            {
                Pref.Cost -= item.price;
                Pref.SetBool(Constant.SKIN_PREF + shopItemId, true);
                Pref.CurSkinId = shopItemId;
                UpdateUI();
            }
            else
            {
                
            }
        }
    }
    public void ButtonQuit()
    {
        CloseDirectly();
        UIManager.Ins.OpenUI<MainMenu>();
        GameManager.Ins.UIMainMenu();
    }
}
