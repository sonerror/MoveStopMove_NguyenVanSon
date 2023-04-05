using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColorObject : MonoBehaviour
{
    [SerializeField] private Transform _tfZonePlayer;
    private Material _material;
    private Color _color;
    private Color _transparentColor;
    private void Start()
    {
        _material = GetComponent<Renderer>().material;
        _color = _material.color;
        _transparentColor = _material.color;
        _transparentColor.a = 0.2f;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.transform == _tfZonePlayer)
        {
            _material.color = Color.Lerp(_color, _transparentColor, 1f);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.transform == _tfZonePlayer)
        {
            ChangeColorOld();
        }
    }
    private void ChangeColorOld()
    {
        _material.color = Color.Lerp(_transparentColor, _color, 1f);
    }
}