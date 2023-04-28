﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : GameUnit
{
    [SerializeField] public Animator _animator;
    [SerializeField] GameObject mask;
    [SerializeField] public List<Character> _listTarget = new List<Character>();


    [SerializeField] public WeaponType _weaponType;
    [SerializeField] public Transform _weaponTransform;
    [SerializeField] public int _indexWeapon = 0;
    private GameObject modelWeapon;
    public GameObject _modelPant;

    private GameObject _hatType;
    public Transform _hatTransform;

    public float _rangeAttack = 5f;
    public const float ATT_RANGE = 5f;

    string _currentAnim;
    private float _multiplier = 1.0f;
    private float _sizeDelta = 0.025f;
    private int _charLevel = 1;

    public bool _isDead { get; set; }


    public virtual void OnInit()
    {
        _isDead = false;
    }
    public void OnEnableWeapon(WeaponType weaponType)
    {
        if (modelWeapon != null)
        {
            Destroy(modelWeapon);
        }
        if (weaponType._weapon != null)
        {
            modelWeapon = Instantiate(weaponType._weapon);
            modelWeapon.transform.SetParent(_weaponTransform, false);
        }

    }

    public void SetActiveWeapon()
    {
        modelWeapon.SetActive(false);
        Invoke(nameof(IsHaveWeapon), 1f);
    }

    public void IsHaveWeapon()
    {
        modelWeapon.SetActive(true);
    }
    public Vector3 GetDirectionTaget()
    {
        Vector3 closestTarget = _listTarget[0].transform.position;
        float closestDistance = Vector3.Distance(TF.position, closestTarget);
        for (int i = 0; i < _listTarget.Count; i++)
        {
            float distance = Vector3.Distance(TF.position, _listTarget[i].transform.position);
            if (distance < closestDistance)
            {
                closestTarget = _listTarget[i].transform.position;
                closestDistance = distance;
            }
        }
        Vector3 directionToTarget = closestTarget - TF.position;
        Vector3 normalizedDirection = directionToTarget.normalized;
        return normalizedDirection;
    }
    public Vector3 GetClosestTarget()
    {
        Vector3 closestTarget = _listTarget[0].TF.position;
        float closestDistance = Vector3.Distance(TF.position, closestTarget);
        for (int i = 0; i < _listTarget.Count; i++)
        {
            float distance = Vector3.Distance(TF.position, _listTarget[i].TF.position);
            if (distance < closestDistance)
            {
                closestTarget = _listTarget[i].TF.position;
                closestDistance = distance;
            }
        }
        return closestTarget;
    }

    public virtual void OnAttack()
    {
        LookBot();
        ChangeAnim(Constant.ANIM_ATTACK);
        SetActiveWeapon();
    }

    public void SetMask(bool active)
    {
        mask.SetActive(active);
    }

    public virtual void AddTarget(Character character)
    {
        this._listTarget.Add(character);

    }
    public virtual void RemoveTarget(Character character)
    {
        this._listTarget.Remove(character);
    }
    public void ChangeAnim(string animName)
    {
        if (_currentAnim != animName)
        {
            _animator.ResetTrigger(animName);
            _currentAnim = animName;
            _animator.SetTrigger(_currentAnim);
        }
    }
    public void LookBot()
    {
        if (_listTarget.Count > 0)
        {
            Vector3 direction = GetDirectionTaget();
            direction.y = 0f;
            TF.rotation = Quaternion.LookRotation(direction);
        }
    }
    public virtual void SpawnWeapon()
    {
        Vector3 target = GetClosestTarget();
        if (this._listTarget.Count > 0)
        {
            if (_weaponType._typeSpawnWeapon == TypeSpawnWeapon.Boomerang)
            {
                WeaponController weapon = SimplePool.Spawn<WeaponBoommerang>(_weaponType._weaponPrefab, _weaponTransform.position, Quaternion.identity);
                weapon.WeaponInit(this, target);
            }
            else if (_weaponType._typeSpawnWeapon == TypeSpawnWeapon.FowardWeapon || _weaponType._typeSpawnWeapon == TypeSpawnWeapon.RotateWeapon)
            {
                WeaponController weapon = SimplePool.Spawn<WeaponForward>(_weaponType._weaponPrefab, _weaponTransform.position, Quaternion.identity);
                weapon.WeaponInit(this, target);
            }
        }
    }
    public virtual void OnDead()
    {
        ChangeAnim(Constant.ANIM_DEAD);
    }
    public void ResetAnim()
    {
        ChangeAnim("");
    }
    public float GetMultiplier()
    {
        return _multiplier;
    }
    public virtual void ChangeAccessory(int index)
    {
        if (_hatType != null)
        {
            Destroy(_hatType);
        }
        _hatType = Instantiate(ShopManage.Ins._hair[index],Vector3.zero,Quaternion.identity);
        _hatType.transform.SetParent(_hatTransform, false);
    }

    public virtual void ChangePant(int index)
    {
        _modelPant.transform.GetComponent<Renderer>().material = ShopManage.Ins._pantTypes[index];
    }
    private void ChangeSize()
    {
        _multiplier = 1.0f + _sizeDelta * (_charLevel - 1);
        transform.localScale = Vector3.one * _multiplier;
    }
    public virtual void ChangeWeapon(int index)
    {

    }
    public virtual void ChangeSkin()
    {

    }
}