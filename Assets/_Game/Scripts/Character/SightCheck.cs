using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SightCheck : MonoBehaviour
{
    public Character character;
    public Bot _bot;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constant.TAG_BOT) || other.CompareTag(Constant.TAG_PLAYER))
        {
            character._target = other.gameObject;
            character._listTarget.Add(character._target);
        }
        if (other.CompareTag(Constant.TAG_PLAYER))
        {
            _bot._lockTaget.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other )
    {
        if(other.CompareTag(Constant.TAG_BOT) || other.CompareTag(Constant.TAG_PLAYER))
        {
            character._listTarget.Remove(other.gameObject);
        }
        if (other.CompareTag(Constant.TAG_PLAYER))
        {
            _bot._lockTaget.SetActive(false);
        }
    }
}