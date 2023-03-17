using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bot : Character
{
 
    private Transform target;
    private NavMeshAgent _agent;
    private float _timer;

    public float _wanderRadius;
    public float _wanderTimer;

    void OnEnable()
    {
        _agent = GetComponent<NavMeshAgent>();
        _timer = _wanderTimer;
    }
    void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= _wanderTimer)
        {
            Vector3 newPos = RandomNavSphere(transform.position, _wanderRadius, -1);
            _agent.SetDestination(newPos);
            ChangAnim(Constant.EANIM_RUN);
            _timer = 0;
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
