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
        [SerializeField] private CameraFollow _cameraFollow;
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
           /* if(!PlayerPrefs.HasKey(Constant.COST_KEY))
            {
                Pref.Cost = 100;
            }*/
            Pref.Cost = 100;
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
            _cameraFollow.ChangeState(CameraFollow.State.GamePlay);
        }
        public void UIShopWeapon()
        {
            _cameraFollow.ChangeState(CameraFollow.State.Shop);
        }
        public void UIMainMenu()
        {
            _cameraFollow.ChangeState(CameraFollow.State.MainMenu);
        }
        public void SkinActive()
        {
            int newSkinPb = ShopManage.Ins.itemsShot[Pref.CurSkinId]._index;
            LevelManager.instance.player.ChangePantFormShop(newSkinPb);
        }
        public void UICoins()
        {
            UIManager.Ins.OpenUI<UICoins>();
        }
    }

}