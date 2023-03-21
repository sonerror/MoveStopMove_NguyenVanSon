using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckBot : MonoBehaviour
{
    public Character character;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(Constant.TAG_BOT) || other.CompareTag(Constant.TAG_PLAYER))
        {
            character._target = other.gameObject;
            character._listTarget.Add(character._target);
        }
    }
    private void OnTriggerExit(Collider other )
    {
        if(other.CompareTag(Constant.TAG_BOT) || other.CompareTag(Constant.TAG_PLAYER))
        {
            character._listTarget.Remove(other.gameObject);
        }
    }
}