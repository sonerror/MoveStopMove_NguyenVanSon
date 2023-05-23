using System;
using System.Collections;
using System.Collections.Generic;
using UIExample;
using UnityEngine;

public class ShopDialog : UICanvas
{
    public Transform griRootShot;
    public Transform griRootBtnHeader;
    public Transform griRootAccessory;
    public Transform griRootSkin;
    public Transform griRootHat;
    public ShopItemUI itemPB;
    public ShopBtnHeader itemHeaderPB;
    public GameObject[] shops;

    private void Start()
    {
        CreateButtonHeader();
        Show(true);
    }
    public override void Show(bool isShow)
    {
        base.Show(isShow);
        UpdateUI();
    }
    private void UpdateUI()
    {
        CreateItemShop(griRootShot, ShopManage.Ins.itemsShot);
        CreateItemShop(griRootAccessory, ShopManage.Ins.itemsAccessory);
        CreateItemShop(griRootSkin, ShopManage.Ins.itemsSkin);
        CreateItemShop(griRootHat, ShopManage.Ins.itemsHat);
    }
    public void CreateItemShop(Transform TF, ShopItem[] itemsShot)
    {
        var items = itemsShot;
        if (items == null || items.Length <= 0
            || !griRootShot || !itemPB)
        {
            return;
        }
        foreach (Transform child in TF)
        {
            if (child)
            {
                Destroy(child.gameObject);
            }
        }
        for (int i = 0; i < items.Length; i++)
        {
            int idx = i;
            var item = items[i];
            if (item != null)
            {
                var itemUIClone = Instantiate(itemPB, Vector3.zero, Quaternion.identity);
                itemUIClone.transform.SetParent(TF);
                itemUIClone.UpdateItemUI(item, idx);
                if (itemUIClone.btn)
                {
                    itemUIClone.btn.onClick.RemoveAllListeners();
                    itemUIClone.btn.onClick.AddListener(() => ItemEvent(item, idx));
                }
            }
        }
    }
    public void CreateButtonHeader()
    {
        var itemsBtnHeader = ShopManage.Ins.itemsBtnHeader;
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
                    shops[0].SetActive(true);
                    itemUIClone.btn.onClick.AddListener(() => BtnEvent(item));
                }
            }
        }
    }
    public void ClearChild()
    {
        if (!griRootShot || griRootShot.childCount <= 0) return;
        /*for (int i = 0; i < griRootShot.childCount; i++)
        {
            var child = griRootShot.GetChild(i);
            if (child)
            {
                Destroy(child.gameObject);
            }
        }*/
        foreach (Transform child in griRootShot)
        {
            if (child)
            {
                Destroy(child.gameObject);
            }
        }
    }
    void BtnEvent(ShopItem item)
    {
        SoundManager.Ins.SfxPlay(Constant.SOUND_BUTTON);
        if (item == null)
        {
            return;
        }
        for (int i = 0; i < shops.Length; i++)
        {
            shops[i].SetActive(false);
            if (item._index == i)
            {
                shops[i].SetActive(true);

                int id = ShopManage.Ins.itemsBtnHeader[i]._index;
                PlayerPrefs.SetInt(Pref.CUR_BTN_ID, id);
                ShopManage.Ins.clickBtnHeader = true;
            }
        }
    }
    void ItemEvent(ShopItem item, int shopItemId)
    {
        SoundManager.Ins.SfxPlay(Constant.SOUND_BUTTON);
        if (item == null) return;
        bool isUnlocked = Pref.GetBool(Pref.SKIN_PREF + shopItemId);
        if (isUnlocked)
        {
            if (shopItemId == Pref.CurId) return;

            Pref.CurId = shopItemId;
            UpdateUI();
        }
        else
        {
            if (Pref.Cost >= item.price)
            {
                Pref.Cost -= item.price;
                Pref.SetBool(Pref.SKIN_PREF + shopItemId, true);
                Pref.CurId = shopItemId;
                UpdateUI();
            }
        }
    }
    public void ButtonQuit()
    {
        SoundManager.Ins.SfxPlay(Constant.SOUND_BUTTON);
        CloseDirectly();
        UIManager.Ins.OpenUI<MainMenu>();
        GameManager.Ins.UIMainMenu();
    }
}
