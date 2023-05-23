using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRote : MonoBehaviour
{
    public float rotationSped;

    void LateUpdate()
    {
        transform.Rotate(0f,rotationSped * Time.deltaTime ,0f);
        
    }
}
