using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bot : Character
{
    [SerializeField] private WeaponController _weapon;


    private NavMeshAgent _agent;
    private float _timer;
    private IState<Bot> _currentState;
    private Vector3 nextPoint;

    public GameObject _lockTaget;
    public float _wanderRadius;
    public float _wanderTimer;
    private void Start()
    {
        OnEnableWeapon();
        ChangeState(new IdleState());
    }
    void OnEnable()
    {
        _agent = GetComponent<NavMeshAgent>();
        _timer = _wanderTimer;
    }
    private void Update()
    {
        if (_currentState != null)
        {
            _currentState.OnExecute(this);
        }
        if (_listTarget.Count > 0)
        {
            Vector3 direction = GetDirectionTaget();
            transform.rotation = Quaternion.LookRotation(direction);
        }
    }
    public override void OnInit()
    {
        base.OnInit();
    }
    public void Moving()
    {
        _agent.enabled = true;
        _timer += Time.deltaTime;
        if (_timer >= _wanderTimer)
        {
            Vector3 newPos = RandomNavSphere(transform.position, _wanderRadius, -1);
            _agent.SetDestination(newPos);
            ChangAnim(Constant.EANIM_RUN);
            _timer = 0;
        }
        if(IsDestination())
        {
            ChangeState(new IdleState());
        }
    }
    public void StopMoving()
    {
        _agent.enabled = false;
        ChangAnim(Constant.EANIM_IDLE);
    }
    public IEnumerator ActionAttack()
    {
        WeaponSpawnBot();
        float time = 0;
        float timer = 2f;
        while (time < timer)
        {
            time += Time.deltaTime;

            yield return null;
        }
        int numRandom = Random.Range(0, 20);
        if (numRandom > 10)
        {
            ChangeState(new IdleState());
        }
        else
        {
            ChangeState(new PatrolState());
        }
        yield return null;
    }
    public override void WeaponSpawnBot()
    {
        base.WeaponSpawnBot();
        if (_listTarget.Count > 0)
        {
            Vector3 target = GetClosestTarget();
            SimplePool.Spawn<WeaponController>(_weapon, _weaponTransform.position, Quaternion.identity).Oninit(this, target);
        }
    }
    public void ChangeState(IState<Bot> newState)
    {
        if (_currentState != null)
        {
            _currentState.OnExit(this);
        }
        _currentState = newState;

        if (_currentState != null)
        {
            _currentState.OnEnter(this);
        }
    }
    public void IsDead()
    {
        if (this._isDead) 
        {
            ChangeState(new DeadState());
        }
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
}
//https://stackoverflow.com/questions/61887165/how-to-make-navmesh-agent-stop-and-then-continue-his-movement
//https://www.youtube.com/watch?v=4mzbDk4Wsmk