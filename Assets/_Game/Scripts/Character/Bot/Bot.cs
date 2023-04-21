using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bot : Character
{
    private IState<Bot> currentState;
    private float _timer;
    public NavMeshAgent agent;
    public float range;
    public float _wanderRadius;
    public float _wanderTimer;
    Vector3 nextPoint;
    public bool _isCanMove;
    public GameObject botName;

    void Update()
    {
        if (currentState != null)
        {
            currentState.OnExecute(this);
        }
    }

    public override void OnInit()
    {
        base.OnInit();
        agent = GetComponent<NavMeshAgent>();
        ChangeState(new IdleState());
        ChangeWeapon(_indexWeapon);
        ChangePant();
    }
    public override void ChangeWeapon(int index)
    {
        base.ChangeWeapon(index);
        index = Random.Range(0, _weaponTypes.Length);
        _weaponType = _weaponTypes[index];
        OnEnableWeapon(_weaponType);
    }
    public override void ChangePant()
    {
        base.ChangePant();

    }
    public void ChangeState(IState<Bot> state)
    {
        if (currentState != null)
        {
            currentState.OnExit(this);
        }
        currentState = state;
        if (currentState != null)
        {
            currentState.OnEnter(this);
        }
    }
    public override void OnAttack()
    {
        base.OnAttack();
        SpawnWeapon();
    }
    public void Moving()
    {
        agent.enabled = true;
        _timer += Time.deltaTime;
        if (_timer >= _wanderTimer)
        {
            Vector3 newPos = RandomNavSphere(transform.position, _wanderRadius, -1);
            agent.SetDestination(newPos);
            ChangeAnim(Constant.ANIM_RUN);
            _timer = 0;
        }
        if (IsDestination())
        {
            ChangeState(new IdleState());
        }
    }
    public void OnMoveStop()
    {
        agent.enabled = false;
    }
    bool IsDestination() => Vector3.Distance(transform.position, nextPoint) - Mathf.Abs(transform.position.y - nextPoint.y) < 0.1f;
    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;
        randDirection += origin;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);
        return navHit.position;
    }

    public override void OnDead()
    {
        base.OnDead();
        OnMoveStop();
        SetMask(false);
        LevelManager.instance.RemoveTarget(this);
    }
}