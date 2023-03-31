using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : GameUnit
{
    [SerializeField] public Animator _animator;
    [SerializeField] GameObject mask;
    [SerializeField] public List<Character> _listTarget = new List<Character>();
    [SerializeField] public WeaponType _WeaponType;
    [SerializeField] public Transform _weaponTransform;
    [SerializeField] private WeaponController _wreaponPrefab;
    private GameObject modelWeapon;

    public const float ATT_RANGE = 5f;
    public float _rangeAttack = 5f;


    string _currentAnim;
    //private Vector3 targetPoint;
    public bool _isDead { get; set; }


    public virtual void OnInit()
    {
        _isDead = false;
    }   
    public void OnEnableWeapon()
    {
        if (modelWeapon != null)
        {
            Destroy(modelWeapon);
        }
        if (_WeaponType._weapon != null)
        {
            modelWeapon = Instantiate(_WeaponType._weapon);
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
        if (this._listTarget.Count > 0)
        {
            Vector3 taget = GetClosestTarget();
            SimplePool.Spawn<WeaponController>(_wreaponPrefab, _weaponTransform.position, Quaternion.identity).Oninit(this, taget);
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
    public virtual void ChangeWeapon()
    {

    }
    public virtual void ChangdeSkin()
    {

    }
}