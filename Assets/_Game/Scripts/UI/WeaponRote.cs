using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRote : MonoBehaviour
{
    public float rotationSped;

    void Update()
    {
        transform.Rotate(0f,rotationSped * Time.deltaTime ,0f);
        
    }
}
