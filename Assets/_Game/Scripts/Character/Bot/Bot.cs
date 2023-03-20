using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bot : Character
{
    private Transform target;
    private NavMeshAgent _agent;
    private float _timer;
    private bool _running = false;

    public float _wanderRadius;
    public float _wanderTimer;
    private float _time = 5;

    void Start()
    {
        StartCoroutine(Pause2s());
    }
    private IEnumerator Pause2s()
    {
        //pause
        yield return new WaitForSeconds(2f);
        StartCoroutine(Continute5s());

    }
    private IEnumerator Continute5s()
    {
        //continute
        yield return new WaitForSeconds(5f);

        StartCoroutine(Pause2s());
    }
    void OnEnable()
    {
        _agent = GetComponent<NavMeshAgent>();
        _timer = _wanderTimer;
    }
    private void Update()
    {
        
        if (!_running)
        {
            ChangAnim(Constant.EANIM_IDLE);
        }    
    }
    void FixedUpdate()
    {
        // BotMoving();
        //Invoke(nameof(BotMoving), 2f);
    }
/*    IEnumerator MoveForDuration()
    {
        _running = true;
        float _timeLapsed = 0;
        while(_timeLapsed < _time)
        {
            StopMove();
            _timeLapsed += Time.deltaTime;
            yield return null;
        }
        _running = false;
    }*/
    public void BotMoving()
    {
        _running = true;
        _timer += Time.deltaTime;
        if (_timer >= _wanderTimer)
        {
            Vector3 newPos = RandomNavSphere(transform.position, _wanderRadius, -1);
            _agent.SetDestination(newPos);
            ChangAnim(Constant.EANIM_RUN);
            _timer = 0;
        }
    }
    public void StopMove()
    {
        _running = false;

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