using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIAlive : MonoBehaviour   
{
    public Text aliveNumber;

    // Update is called once per frame
    void Update()
    {
        aliveNumber.text =  LevelManager.instance.alive.ToString();
    }
}
