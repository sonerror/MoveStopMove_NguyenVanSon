using System.Collections;
using System.Collections.Generic;
using Unity;
using UnityEngine;

public class DeadState : IState<Bot>
{
    float timer = 0f;
    float time;
    public void OnEnter(Bot t)
    {
        timer = 2f;
        t.OnDead();
    }

    public void OnExecute(Bot t)
    {
        if (time > timer)
        {

            t._isDead = false;

            SimplePool.Despawn(t);
            time = 0;
        }
        time += Time.deltaTime;

    }

    public void OnExit(Bot t)
    {

    }

}