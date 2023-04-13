using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponForward : WeaponController
{
    [SerializeField] protected Vector3 currentPostion;
    void Start()
    {
        currentPostion = _character.transform.position;
    }
    private void Update()
    {
        MoveForward();
    }

    public void MoveForward()
    {
        if (!_isUpdatePosition)
        {
            currentPostion = _character.transform.position;
            _isUpdatePosition = true;
        }

        if (Vector3.Distance(transform.position, currentPostion) < _character._rangeAttack)
        {
            transform.forward = new Vector3(transform.forward.x, 0, transform.forward.z);
            transform.Translate(transform.forward * moveSpeed * Time.deltaTime, Space.World);
        }
        else
        {
            _isUpdatePosition = false;
            OnDespawn();
        }
    }
}
