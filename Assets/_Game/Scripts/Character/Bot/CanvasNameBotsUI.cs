using System.Collections;
using System.Collections.Generic;
using TMPro;
using UIExample;
using UnityEngine;
using UnityEngine.UI;

public class CanvasNameBotsUI : GameUnit
{
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private Transform targetTransform;
    [SerializeField] private TextMeshProUGUI tmp;

    [SerializeField] private Vector3 offset;
    [SerializeField] private float speed;
    private Vector3 targetPosition;
    public string nameString;
    public Text numberLevel;

    private void Start()
    {
        Bot bot = targetTransform.GetComponent<Bot>();
        if (bot != null)
        {
            nameString = BotNamePool.instance.nameList[(int)Random.Range(0, BotNamePool.instance.nameList.Count)];
            tmp.text = nameString;
        }
    }
    private void FixedUpdate()
    {
        int levelBot = UIManager.Ins.numberLevel + 1;
        numberLevel.text = levelBot.ToString();
    }

    void LateUpdate()
    {
        targetPosition = Camera.main.WorldToScreenPoint(targetTransform.position + offset);
        rectTransform.position = Vector3.Lerp(rectTransform.position, targetPosition, speed * Time.deltaTime);
    }
    public void SetTargetTransform(Transform targetTF)
    {
        targetTransform = targetTF;
    }
}
