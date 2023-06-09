﻿using System;
using System.Collections;
using System.Collections.Generic;
using UIExample;
using UnityEngine;

public class Player : Character
{
    [Header("=======Player class=======")]
    [SerializeField] private Rigidbody rb;
    [SerializeField] public float moveSpeed = 5f;
    [SerializeField] private float rotateSpeed;

    private float timeRate = 1f;
    private float time = 0f;

    public static Player Instance { get; private set; }
    public bool isMove;
    public bool move = true;
    
    void Start()
    {
        OnEnableWeapon(weaponType);
        isDead = false;
        ChangeAnim(Constant.ANIM_IDLE);
        LevelManager.Ins.RemoveTarget(this);
    }

    private void Update()
    {
        if (this.isDead)
        {
            this.OnDead();
        }
        time += Time.deltaTime;
        if (!this.isDead)
        {
            if (LevelManager.Ins.alive == 1)
            {
                ChangeAnim(Constant.ANIM_VICTORY);
                return;
            }
            if (Input.GetMouseButtonUp(0))
            {
                isMove = false;
                time = 1.1f;
            }
            else if (!isMove && listTarget.Count > 0)
            {
                if (time >= timeRate)
                {
                    OnAttack();
                    time = 0f;
                }
            }
            else if (!isMove)
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
                isMove = true;
                rb.MovePosition(rb.position + JoystickControl.direct * moveSpeed * Time.fixedDeltaTime);
                ChangeAnim(Constant.ANIM_RUN);
                Vector3 direction = Vector3.RotateTowards(transform.forward, JoystickControl.direct, rotateSpeed * Time.deltaTime, 0.0f);
                transform.rotation = Quaternion.LookRotation(direction);
            }
        }
    }
    public void StopMoving()
    {
        moveSpeed = 0f;
        isMove = false;
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
           ThrowWeapon();
        }
    }
    private void ResetPosition()
    {
        TF.position = Vector3.zero;
        TF.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
    }
    public override void ThrowWeapon()
    {
        base.ThrowWeapon();
        SoundManager.Ins.SfxPlay(Constant.SOUND_THROW);
    }
    public override void ChangeWeapon(int index)
    {
        base.ChangeWeapon(index);
        weaponType = ShopManage.Ins._weaponTypes[index];
        OnEnableWeapon(weaponType);
    }
    public override void ChangePant(int index)
    {
        base.ChangePant(index);
    }
    public override void ChangeHair(int index)
    {
        base.ChangeHair(index);

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
        isDead = false;
        attRange = 5f;
        moveSpeed = 5f;
    }
}