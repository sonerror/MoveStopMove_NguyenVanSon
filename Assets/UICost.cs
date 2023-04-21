using System.Collections;
using System.Collections.Generic;
using UIExample;
using UnityEngine;
using UnityEngine.UI;
public class UICost : Singleton<UICost>
{
    public Text costText;
    public void UpdateCost()
    {
        if (costText)
        {
            costText.text = "COINS : " + Pref.Cost;
        }
    }
}
