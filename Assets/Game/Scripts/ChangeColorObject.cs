using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColorObject : MonoBehaviour
{
    private Color originalColor;
    public Color highlightColor;

    private void Start()
    {
        originalColor = GetComponent<Renderer>().material.color;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(Constant.TAG_CHARACTER))
        {
            GetComponent<Renderer>().material.color = highlightColor;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag(Constant.TAG_CHARACTER))
        {
            GetComponent<Renderer>().material.color = originalColor;
        }
    }
}