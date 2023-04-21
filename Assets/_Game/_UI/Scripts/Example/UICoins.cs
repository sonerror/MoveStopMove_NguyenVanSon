using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UICoins : UICanvas
{
    public Text coins;

    private void Update()
    {
        coins.text = Pref.Cost.ToString();
    }
}
