using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Resources;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.CullingGroup;
using UnityEngine.UI;
namespace UIExample
{
    public class GameManager : Singleton<GameManager>
    {
        protected void Awake()
        {
            Input.multiTouchEnabled = false;
            Application.targetFrameRate = 60;
            Screen.sleepTimeout = SleepTimeout.NeverSleep;

            int maxScreenHeight = 1280;
            float ratio = (float)Screen.currentResolution.width / (float)Screen.currentResolution.height;
            if (Screen.currentResolution.height > maxScreenHeight)
            {
                Screen.SetResolution(Mathf.RoundToInt(ratio * (float)maxScreenHeight), maxScreenHeight, true);
            }  
        }
        public void Start()
        {
            WaitGame();
                if (!PlayerPrefs.HasKey(Pref.COST_KEY))
                {
                    Pref.Cost = 0;
                }
            //Pref.Cost = 100;
            UICoins();
        }
        public void Update()
        {
            SkinActive();
        }
        public void WaitGame()
        {   

            UIManager.Ins.OpenUI<MainMenu>();
        }
        public void StartGame()
        {
            CameraFollow.Ins.ChangeState(CameraFollow.State.GamePlay);
        }
        public void UIShopWeapon()
        {
            CameraFollow.Ins.ChangeState(CameraFollow.State.Shop);
        }
        public void UIMainMenu()
        {
            CameraFollow.Ins.ChangeState(CameraFollow.State.MainMenu);
        }
        public void SkinActive()
        {
            int index = ShopManage.Ins.itemsShot[Pref.CurId]._index;
            int id = ShopManage.Ins.itemsBtnHeader[Pref.CurBtnId]._index;
            bool click = ShopManage.Ins.clickBtnHeader;
            if (id == 0 && click)
            {
                LevelManager.Ins.player.ChangePant(index);
            }
            if (id == 1 )
            {
                LevelManager.Ins.player.ChangeAccessory(index);
            }
            if (id == 2)
            {
                //LevelManager.Ins.player.ChangeSkin(index);
            }
            if (id == 3 && click)
            {
                
                LevelManager.Ins.player.ChangeHair(index);
            }
        }
        public void UICoins()
        {
            UIManager.Ins.OpenUI<UICoins>();
        }
    }
}