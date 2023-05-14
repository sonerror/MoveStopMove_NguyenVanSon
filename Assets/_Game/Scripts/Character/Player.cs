using System;
using System.Collections;
using System.Collections.Generic;
using UIExample;
using UnityEngine;

public class Player : Character
{
    [Header("=======Player class=======")]
    [SerializeField] private Rigidbody _rb;
    [SerializeField] public float _moveSpeed = 5f;
    [SerializeField] private float _rotateSpeed;

    private float _timeRate = 1f;
    private float _time = 0f;

    public static Player Instance { get; private set; }
    public bool _isMove;
    public bool move = true;

    void Start()
    {
        OnEnableWeapon(_weaponType);
        _isDead = false;
        ChangeAnim(Constant.ANIM_IDLE);
        LevelManager.Ins.RemoveTarget(this);
    }

    private void Update()
    {
        if (this._isDead)
        {
            this.OnDead();
        }
        _time += Time.deltaTime;
        if (!this._isDead)
        {
            if (LevelManager.Ins.alive == 1)
            {
                ChangeAnim(Constant.ANIM_VICTORY);
                return;
            }
            if (Input.GetMouseButtonUp(0))
            {
                _isMove = false;
                _time = 1.1f;
            }
            else if (!_isMove && _listTarget.Count > 0)
            {
                if (_time >= _timeRate)
                {
                    OnAttack();
                    _time = 0f;
                }

            }
            else if (!_isMove)
            {
                ChangeAnim(Constant.ANIM_IDLE);
            }
        }
    }

    void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        if (move)
        {
            if (Input.GetMouseButton(0) && JoystickControl.direct.sqrMagnitude > 0.001f)
            {
                _isMove = true;
                _rb.MovePosition(_rb.position + JoystickControl.direct * _moveSpeed * Time.fixedDeltaTime);
                ChangeAnim(Constant.ANIM_RUN);
                Vector3 direction = Vector3.RotateTowards(transform.forward, JoystickControl.direct, _rotateSpeed * Time.deltaTime, 0.0f);
                transform.rotation = Quaternion.LookRotation(direction);
            }
        }
    }
    public void StopMoving()
    {
        _moveSpeed = 0f;
        _isMove = false;
    }

    public override void OnInit()
    {
        base.OnInit();
    }
    public override void OnAttack()
    {
        base.OnAttack();
        StartCoroutine(DoSpawnWeapon());
    }
    public override void AddTarget(Character character)
    {
        base.AddTarget(character);
        character.SetMask(true);
    }
    public override void RemoveTarget(Character character)
    {
        base.RemoveTarget(character);
        character.SetMask(false);
    }
    IEnumerator DoSpawnWeapon()
    {
        float timerate = 0.4f;
        float time = 0;
        bool mouseClicked = false;
        while (time < timerate && !mouseClicked)
        {
            time += Time.deltaTime;
            yield return null;
            if (Input.GetMouseButton(0))
            {
                mouseClicked = true;
            }
        }
        if (mouseClicked)
        {
            yield return null;
        }
        else
        {
            SpawnWeapon();
        }
    }
    private void ResetPosition()
    {
        TF.position = Vector3.zero;
        TF.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
    }
    public override void SpawnWeapon()
    {
        base.SpawnWeapon();
        SoundManager.Ins.SfxPlay(Constant.SOUND_THROW);
    }
    public override void ChangeWeapon(int index)
    {
        base.ChangeWeapon(index);
        _weaponType = ShopManage.Ins._weaponTypes[index];
        OnEnableWeapon(_weaponType);
    }
    public override void ChangePant(int index)
    {
        base.ChangePant(index);
    }
    public override void ChangeAccessory(int index)
    {
        base.ChangeAccessory(index);

    }
    public override void OnDead()
    {
        base.OnDead();
        move = false;
        LevelManager.Ins.RemoveTarget(this);
        UIManager.Ins.OpenUI<UIGamePlay>().CloseDirectly();
        UIManager.Ins.OpenUI<UILose>();
    }
    public override void ChangeSize(float size)
    {
        base.ChangeSize(size);
        this.attRange += 0.2f;
    }
    public void OnRevive()
    {
        base.OnInit();
        ResetPosition();
        this.ChangeSize(1);
        _isDead = false;
        attRange = 5f;
        _moveSpeed = 5f;
    }
}