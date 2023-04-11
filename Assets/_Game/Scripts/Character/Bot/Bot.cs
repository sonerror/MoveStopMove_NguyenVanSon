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
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        ChangeState(new IdleState());
        OnEnableWeapon();
        OnInit();
    }

    // Update is called once per frame
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

    public IEnumerator DoAttack()
    {
        OnAttack();
        float time = 0;
        float timer = 1.1f;
        while (time < timer)
        {
            time += Time.deltaTime;

            yield return null;
        }
        int numRand = Random.Range(0, 100);
        if (numRand > 50)
        {
            ChangeState(new IdleState());
        }
        else
        {
            ChangeState(new PatrolState());
        }
        yield return null;
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