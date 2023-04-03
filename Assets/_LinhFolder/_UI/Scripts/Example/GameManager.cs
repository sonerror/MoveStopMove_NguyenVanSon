using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Resources;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.CullingGroup;

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
        }
        public void WaitGame()
        {      
            UIManager.Ins.OpenUI<MainMenu>();
        }
        public void StartGame()
        {
            _cameraFollow.ChangeState(CameraFollow.State.GamePlay);
        }
    }

}