using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bot : Character
{
    private NavMeshAgent _agent;
    public float _wanderRadius;
    public float _wanderTimer;
    private float _timer;
    private IState _currentState;
    private bool _isMove;
    [SerializeField] private WeaponController _weapon;

    private float _timeRate = 1f;
    private float _time = 0f;
    private void Awake()
    {
        ChangeState(new PatrolState());
    }
    private void Start()
    {
        OnEnableWeapon();
    }
    void OnEnable()
    {
        _agent = GetComponent<NavMeshAgent>();
        _timer = _wanderTimer;
    }
    private void Update()
    {
        _time += Time.deltaTime;

       if (!_isMove && _listTarget.Count > 0)
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
            else if (_listTarget.Count < 0)
            {
                _isMove= true;
                ChangeState(new PatrolState());
            }
        }
        if (_currentState != null)
        {
            _currentState.OnExecute(this);
        }
    }
    public override void OnInit()
    {
        base.OnInit();
    }
    public void Moving()
    {
        _agent.enabled= true;
        _timer += Time.deltaTime;
        if (_timer >= _wanderTimer)
        {
            Vector3 newPos = RandomNavSphere(transform.position, _wanderRadius, -1);
            _agent.SetDestination(newPos);
            _isMove = true;
            ChangAnim(Constant.EANIM_RUN);
            _timer = 0;
        }
    }
    public void StopMoving()
    {
        _isMove = false;
        _agent.enabled= false;
        ChangAnim(Constant.EANIM_IDLE);
    }

    public override void Attack()
    {
        ChangAnim(Constant.EANIM_ATTACK);
        StartCoroutine(SpawnWeaponBot());
    }
    IEnumerator SpawnWeaponBot()
    {
        float timeRate = 0.4f;
        float _time = 0;
        while (_time < timeRate)
        {
            _time += Time.deltaTime;
            yield return null;
            if (_isMove)
            {
                goto Lable;
            }
        }
        if (_listTarget.Count > 0)
        {
            Vector3 target = GetClosestTarget();
            SimplePool.Spawn<WeaponController>(_weapon, _weaponTransform.position, Quaternion.identity).Oninit(this, target);
        }
    Lable:
        yield return null;
        Debug.Log("check weapon");
    }
    public void ChangeState(IState newState)
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