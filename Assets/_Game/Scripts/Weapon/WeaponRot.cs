using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRot : WeaponController
{
    private Vector3 target;
    public Transform pivotPoint;
    public float rotationSpeed = 700f;

    public enum State { Forward, Backward, Stop }

    private State state;

    private void Start()
    {
        pivotPoint = transform;
    }
    private void Update()
    {
        Boomearang();
    }
    public override void WeaponInit(Character character, Vector3 target)
    {
        base.WeaponInit(character, target);
        this.target = (target - character.TF.position).normalized * (character.attRange + 1) + character.TF.position + Vector3.up;
        state = State.Forward;
    }


    public void Boomearang()
    {
        // Lấy giá trị góc xoay hiện tại của đối tượng
        Vector3 currentRotation = pivotPoint.rotation.eulerAngles;

        // Tính toán giá trị góc xoay mới
        float newRotation = currentRotation.x + rotationSpeed * Time.deltaTime;

        // Xoay đối tượng quanh trục Z với góc xoay mới
        //targetObject.RotateAround(targetObject.position, Vector3.forward, newRotation - currentRotation.z);
        switch (state)
        {
            case State.Forward:
                TF.position = Vector3.MoveTowards(TF.position, this.target, weaponMoveSpeed * Time.deltaTime);
                transform.RotateAround(pivotPoint.position, Vector3.forward, newRotation - currentRotation.z);
                if (Vector3.Distance(TF.position, target) < 0.1f)
                {
                    state = State.Backward;
                }
                break;

            case State.Backward:
                TF.position = Vector3.MoveTowards(TF.position, this._character.TF.position, weaponMoveSpeed * Time.deltaTime);
                transform.RotateAround(pivotPoint.position, Vector3.forward, newRotation - currentRotation.z);
                if (_character._isDead || Vector3.Distance(TF.position, this._character.TF.position) < 0.1f) // 
                {
                    OnDespawn();
                }
                break;
        }

    }
}

