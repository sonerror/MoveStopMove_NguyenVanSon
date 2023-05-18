using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.AI;

public class Bot : Character
{
    [Header("Bot class:")]
    private IState<Bot> currentState;
    private float _timer;

    [SerializeField] public NavMeshAgent agent;
    public float range;
    public float _wanderRadius;
    public float _wanderTimer;
    private Vector3 nextPoint;
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
        ChangeState(new IdleState());
        ChangeWeapon(_indexWeapon);
        ChangePantFormIdex();
        ChangeHairFormIndex();
        ChangeAccessoryFormIndex();
    }
    public override void ChangeWeapon(int index)
    {
        base.ChangeWeapon(index);
        index = Random.Range(0, ShopManage.Ins._weaponTypes.Length);
        _weaponType = ShopManage.Ins._weaponTypes[index];
        OnEnableWeapon(_weaponType);
    }
    public override void ChangePant(int index)
    {
        base.ChangePant(index);
    }
    void ChangePantFormIdex()
    {
        int index = Random.Range(0, ShopManage.Ins.itemsShot.Length);
        ChangePant(index);
    }
    public override void ChangeHair(int index)
    {
        base.ChangeHair(index);
        
    }
    void ChangeHairFormIndex()
    {
        int index = Random.Range(0, ShopManage.Ins._hair.Length);
        ChangeHair(index);
    }


    public override void ChangeAccessory(int index)
    {
        base.ChangeAccessory(index);

    }
    void ChangeAccessoryFormIndex()
    {
        int index = Random.Range(0,ShopManage.Ins._accessory.Length);
        ChangeAccessory(index);
    }
    public IEnumerator DoAttack()
    {
        OnAttack();
        float time = 0;
        float timer = 1.11f;
        while (time < timer)
        {
            time += Time.deltaTime;

            yield return null;
        }
        int numRand = Random.Range(0,5);
        if (numRand == 3)
        {
            ChangeState(new IdleState());
        }
        else
        {
            ChangeState(new PatrolState());
        }
        yield return null;
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
    public override void OnDespawn()
    {
        base.OnDespawn();
        this._isDead = false;
        SimplePool.Despawn(this);
        CancelInvoke();
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
        LevelManager.Ins.RemoveTarget(this);
    }
}