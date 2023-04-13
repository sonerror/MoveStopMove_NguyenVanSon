using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBoommerang : WeaponController
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
        this.target = (target - character.TF.position).normalized * (Character.ATT_RANGE + 1) + character.TF.position + Vector3.up;
        state = State.Forward;
    }


    public void Boomearang()
    {
        switch (state)
        {
            case State.Forward:
                TF.position = Vector3.MoveTowards(TF.position, this.target, moveSpeed * Time.deltaTime);
                transform.RotateAround(pivotPoint.position, Vector3.up, rotationSpeed * Time.deltaTime);
                if (Vector3.Distance(TF.position, target) < 0.1f)
                {
                    state = State.Backward;
                }
                break;

            case State.Backward:
                TF.position = Vector3.MoveTowards(TF.position, this._character.TF.position, moveSpeed * Time.deltaTime);
                transform.RotateAround(pivotPoint.position, Vector3.up, rotationSpeed * Time.deltaTime);
                if (_character._isDead || Vector3.Distance(TF.position, this._character.TF.position) < 0.1f) // 
                {
                    OnDespawn();
                }
                break;
        }

    }

}
