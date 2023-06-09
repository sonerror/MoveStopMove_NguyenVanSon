using System.Collections;
using System.Collections.Generic;
using TMPro;
using UIExample;
using UnityEngine;
using UnityEngine.UI;

public class NamePlayer : Singleton<NamePlayer>
{
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private Transform targetTransform;
    [SerializeField] private TextMeshProUGUI tmp;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float speed;
    private Vector3 targetPosition;
    public Text numberLevel;
    void LateUpdate()
    {
       
        targetPosition = Camera.main.WorldToScreenPoint(targetTransform.position + offset);
        rectTransform.position = Vector3.Lerp(rectTransform.position, targetPosition, speed * Time.deltaTime);
    }
    void FixedUpdate()
    {
        numberLevel.text = UIManager.Ins.numberLevel.ToString();
    }
    public void SetTargetTransform(Transform targetTF)
    {
        targetTransform = targetTF;
    }
    public void SetNamePlayer()
    {
        string namePlayer = PlayerPrefs.GetString(Pref.NAME_PLAYER_PREF);
        if(tmp.text == null)
        {
            tmp.text = Constant.PLAYER_NAME_DEFAULT;
        }
        else
        {
            tmp.text = namePlayer;
        }
    }
}
