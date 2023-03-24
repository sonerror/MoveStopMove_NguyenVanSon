using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RandomPosBot : MonoBehaviour
{
    [SerializeField] private Transform _transformPlayer;
    [SerializeField] private Transform _transformBot;
    public float _wanderRadius;

    private void Start()
    {
        RandomPositonBot();
    }
    void RandomPositonBot()
    {
        while(Vector3.Distance(_transformPlayer.position,_transformBot.position) < 5f)
        {
            _transformBot.position = RandomNavSphere(transform.position, _wanderRadius, -1);
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