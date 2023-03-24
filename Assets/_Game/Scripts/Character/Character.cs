using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : GameUnit
{
    [SerializeField] public Animator _animator;
    [SerializeField] public GameObject _target;
    [SerializeField] public List<GameObject> _listTarget = new List<GameObject>();
    [SerializeField] public Transform _trasformPlayer;
    [SerializeField] public WeaponType _weaponType;
    [SerializeField] public Transform _weaponTransform;
    private GameObject modelWeapon;
    public float _rangeWeapon;
    string _currentAnim;
    public bool _isDead;

    public virtual void OnInit()
    {

    }
    private void Start()
    {
        OnInit();
    }
    public Vector3 GetDirectionTaget()//lấy hướng của target
    {
        Vector3 closestTarget = _listTarget[0].transform.position;
        float closestDistance = Vector3.Distance(_trasformPlayer.position , closestTarget);
        for (int i = 0; i < _listTarget.Count; i++)
        {
            float distance = Vector3.Distance(_trasformPlayer.position, _listTarget[i].transform.position);
            if (distance < closestDistance)
            {
                closestTarget = _listTarget[i].transform.position;
                closestDistance = distance;
            }
        }
        Vector3 directionToTarget = closestTarget - _trasformPlayer.position;
        Vector3 normalizedDirection = directionToTarget.normalized;
        return normalizedDirection;
    }
    public Vector3 GetClosestTarget()//lấy target gần nhất
    {
        Vector3 closestTarget = _listTarget[0].transform.position;
        float closestDistance = Vector3.Distance(_trasformPlayer.position, closestTarget);// Distance Khoảng cách của 2 điểm vector
        for (int i = 0; i < _listTarget.Count; i++)
        {
            float distance = Vector3.Distance(_trasformPlayer.position, _listTarget[i].transform.position);
            if (distance < closestDistance)
            {
                closestTarget = _listTarget[i].transform.position;
                closestDistance = distance;
            }
        }
        return closestTarget;
    }
    public void OnEnableWeapon()//Tạo ra vũ khí trên tay
    {
        if (modelWeapon != null)
        {
            Destroy(modelWeapon);
        }
        if (_weaponType._weapon != null)
        {
            modelWeapon = Instantiate(_weaponType._weapon);
            modelWeapon.transform.SetParent(_weaponTransform, false);
        }
    }
    public void SetActiveWeapon() 
    {
        modelWeapon.SetActive(false);// ẩn vũ khí khi nhém
        Invoke(nameof(IsHaveWeapon), 1f);//chờ 1s sau thì hiện lại
    }

    public void IsHaveWeapon()
    {
        modelWeapon.SetActive(true); //hiện lại vũ khí
    }
    public void ChangAnim(string animName) // chuyển Animation 
    {
        if (_currentAnim != animName)
        {
            _animator.ResetTrigger(animName);
            _currentAnim = animName;
            _animator.SetTrigger(_currentAnim);
        }
    }
    public void ResetAnim()
    {
        ChangAnim("");
    }
    public virtual void ChangeWeapon()
    {

    }
    public virtual void ChangdeSkin()
    {
    }
    public virtual void ChangeAccessory()
    {
    }
    public void OnDead()
    {
        _isDead = true;
    }

    public virtual void Attack()
    {
        ChangAnim(Constant.ANIM_ATTACK);
        SetActiveWeapon();
    }
    public virtual void WeaponSpawnBot()
    {
        ChangAnim(Constant.EANIM_ATTACK);
        SetActiveWeapon();
    }
    public virtual void Death()
    {
        ChangAnim(Constant.ANIM_DEAD);
    }
}