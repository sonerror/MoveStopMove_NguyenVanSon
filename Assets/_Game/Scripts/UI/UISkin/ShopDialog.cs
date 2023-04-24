using System;
using System.Collections;
using System.Collections.Generic;
using UIExample;
using UnityEngine;

public class ShopDialog : UICanvas
{
    public Transform griRootShot;
    public Transform griRootBtnHeader;
    public ShopItemUI itemPB;
    public ShopBtnHeader itemHeaderPB;
    public GameObject[] shops;

    private void Start()
    {
        CreateButtonHeader();
        Show(true);
        BtnEvent();

    }
    public override void Show(bool isShow)
    {
        base.Show(isShow);
        UpdateUI();

    }
    private void UpdateUI()
    {
        CreateShot();
    }

    public void CreateShot()
    {
        Debug.Log("Shop Shot");
        var items = ShopManage.Ins.itemsShot;
        if (items == null || items.Length <= 0
            || !griRootShot || !itemPB)
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
                var itemUIClone = Instantiate(itemPB, Vector3.zero, Quaternion.identity);
                itemUIClone.transform.SetParent(griRootShot);
                itemUIClone.UpdateUI(item, idx);
                if (itemUIClone.btn)
                {
                    itemUIClone.btn.onClick.RemoveAllListeners();
                    itemUIClone.btn.onClick.AddListener(() => ItemEvent(item, idx));
                }
            }

        }
        Debug.Log("Shop Shop test");
    }

    public void CreateButtonHeader()
    {
        var itemsBtnHeader = ShopManage.Ins.itemBtnHeader;
        if (itemsBtnHeader == null || itemsBtnHeader.Length <= 0
            || !griRootBtnHeader || !itemHeaderPB)
        {
            return;
        }
        ClearChild();
        for (int i = 0; i < itemsBtnHeader.Length; i++)
        {
            int idx = i;
            var item = itemsBtnHeader[i];
            if (item != null)
            {
                var itemUIClone = Instantiate(itemHeaderPB, Vector3.zero, Quaternion.identity);
                itemUIClone.transform.SetParent(griRootBtnHeader);
                itemUIClone.UpdateUIBtnHeader(item, idx);
                if (itemUIClone.btn)
                {
                    itemUIClone.btn.onClick.RemoveAllListeners();
                    itemUIClone.btn.onClick.AddListener(() => BtnEvent());
                }
            }
        }
    }
    public void ClearChild()
    {
        if (!griRootShot || griRootShot.childCount <= 0) return;
        for (int i = 0; i < griRootShot.childCount; i++)
        {
            var child = griRootShot.GetChild(i);
            if (child)
            {
                Destroy(child.gameObject);
            }
        }
    }
    void BtnEvent()
    {

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
