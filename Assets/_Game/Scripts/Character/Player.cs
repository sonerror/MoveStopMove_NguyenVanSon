using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _rotateSpeed;
    [SerializeField] private WeaponController _weaponPrefab;

    private float _timeRate = 1f;
    private float _time = 0f;

    public bool _isMove;
    public bool _isAttack;

    void Start()
    {
        OnEnableWeapon();
    }
    private void Update()
    {

        _time += Time.deltaTime;

        if (Input.GetMouseButtonUp(0))
        {
            _isMove = false;
        }
        else if (!_isMove && _listTarget.Count > 0)
        {
            if (_time >= _timeRate)
            {
                Attack();
                _time = 0f;
            }
            if (_listTarget.Count > 0)
            {
                Vector3 direction = GetDirectionTaget();
                transform.rotation = Quaternion.LookRotation(direction);
            }
        }
        else if (!_isMove)
        {
            ChangAnim(Constant.ANIM_IDLE);
        }
    }
    void FixedUpdate()
    {
        Move();
    }
    private void Move()
    {
        if (Input.GetMouseButton(0) && JoystickControl.direct != Vector3.zero)
        {
            _isMove = true;
            _rb.MovePosition(_rb.position + JoystickControl.direct * _moveSpeed * Time.fixedDeltaTime);
            ChangAnim(Constant.ANIM_RUN);
            Vector3 direction = Vector3.RotateTowards(transform.forward, JoystickControl.direct, _rotateSpeed * Time.deltaTime, 0.0f);
            transform.rotation = Quaternion.LookRotation(direction);
        }
    }
    private void StopMoving()
    {
        _moveSpeed = 0.1f;
    }
    public override void Attack()
    {
        _isAttack = true;
        base.Attack();
        StartCoroutine(SpawnWeapon());
    }
    IEnumerator SpawnWeapon()
    {

        float timeRate = 0.4f;
        float _time = 0;

        while (_time < timeRate)
        {
            _time += Time.deltaTime;
            yield return null;
            if (Input.GetMouseButton(0))
            {
                goto Lable;
            }
        }
        if (_listTarget.Count > 0)
        {
            Vector3 target = GetClosestTarget();
            SimplePool.Spawn<WeaponController>(_weaponPrefab, _weaponTransform.position, Quaternion.identity).Oninit(this, target);
        }
    Lable:
        yield return null;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constant.TAG_BULLET))
        {
            _isAttack = false;
            ChangAnim(Constant.ANIM_DEAD);
        }
    }
}