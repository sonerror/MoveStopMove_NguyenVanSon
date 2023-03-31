using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : GameUnit
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private Character _character;
    [SerializeField] Vector3 currentPostion;
    bool _isUpdatePosition = false;
    public void Oninit(Character character, Vector3 target)
    {
        this._character = character;
        TF.forward = (target - TF.position).normalized;
    }
    private void Update()
    {
        if (!_isUpdatePosition)
        {
            currentPostion = _character.TF.position;
            _isUpdatePosition = true;
        }
        if (Vector3.Distance(TF.position, currentPostion) < _character._rangeAttack)
        {
            TF.forward = new Vector3(TF.forward.x, 0, TF.forward.z);
            TF.Translate(TF.forward * moveSpeed * Time.deltaTime, Space.World);
        }
        else
        {
            _isUpdatePosition = false;
            OnDespawn();
        }
    }

    public void OnDespawn()
    {
        SimplePool.Despawn(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constant.TAG_CHARACTER) && other.GetComponent<Character>() != _character)
        {
            OnDespawn();
            other.GetComponent<Character>()._isDead = true;
            Debug.Log(other.gameObject);
            _character.RemoveTarget(other.GetComponent<Character>());
            _isUpdatePosition = false;
        }
    }
}